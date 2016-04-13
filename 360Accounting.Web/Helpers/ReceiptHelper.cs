using _360Accounting.Core;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web
{
    public static class ReceiptHelper
    {
        private static IReceiptService service;
        
        static ReceiptHelper()
        {
            service = IoC.Resolve<IReceiptService>("ReceiptService");
        }

        public static IEnumerable<ReceiptViewModel> GetReceipts(long bankId,
            long bankAccountId, DateTime? receiptDate = null)
        {
            return service.GetReceipts(SessionHelper.SOBId, bankId, bankAccountId, receiptDate)
                .Select(x => new ReceiptViewModel(x)).ToList();
        }

        public static IList<SelectListItem> GetReceiptList(long bankId, long bankAccountId,
            DateTime? receiptDate = null)
        {
            IList<SelectListItem> receiptList = GetReceipts(bankId, bankAccountId, receiptDate)
                .Select(x => new SelectListItem
                {
                    Text = "RECEIPT # " + x.ReceiptNumber + " of CUSTOMER " + x.CustomerName + " Amounting : " + x.ReceiptAmount,
                    Value = x.Id.ToString()
                }).ToList();

            return receiptList;
            
        }

        public static string GetDocNo(long customerId, long periodId, long sobId, long currencyId)
        {
            var currentDocument = service.GetSingle(AuthenticationHelper.CompanyId.Value, sobId, periodId, currencyId, customerId);
            string newDocNo = "";
            if (currentDocument != null)
            {
                int outVal;
                bool isNumeric = int.TryParse(currentDocument.ReceiptNumber, out outVal);
                if (isNumeric && currentDocument.ReceiptNumber.Length == 8)
                {
                    newDocNo = (int.Parse(currentDocument.ReceiptNumber) + 1).ToString();
                    return newDocNo;
                }
            }

            //Create New DocNum..
            string yearDigit = SessionHelper.JV.GLDate.ToString("yy");
            string monthDigit = SessionHelper.JV.GLDate.ToString("MM");
            string docNo = int.Parse("1").ToString().PadLeft(4, '0');

            return yearDigit + monthDigit + docNo;
        }
    }
}
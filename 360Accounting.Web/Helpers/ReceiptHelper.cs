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

        public static IEnumerable<SelectListItem> GetReceiptList(long bankId, long bankAccountId,
            DateTime? receiptDate = null)
        {
            IEnumerable<SelectListItem> receiptList = GetReceipts(bankId, bankAccountId, receiptDate)
                .Select(x => new SelectListItem
                {
                    Text = "RECEIPT # " + x.ReceiptNumber + " of CUSTOMER " + x.CustomerName + " Amounting : " + x.ReceiptAmount,
                    Value = x.Id.ToString()
                }).ToList();

            return receiptList;
        }
    }
}
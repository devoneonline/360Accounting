using _360Accounting.Core;
using _360Accounting.Core.Entities;
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

        #region Private Methods
        private static Receipt getEntityByModel(ReceiptViewModel model)
        {
            if (model == null) return null;

            Receipt entity = new Receipt();

            if (model.Id == 0)
            {
                entity.CreateBy = AuthenticationHelper.UserId;
                entity.CreateDate = DateTime.Now;
                entity.CompanyId = AuthenticationHelper.CompanyId.Value;
            }
            else
            {
                entity.CreateBy = model.CreateBy;
                entity.CreateDate = model.CreateDate;
                entity.CompanyId = model.CompanyId;
            }

            entity.BankAccountId = model.BankAccountId;
            entity.BankId = model.BankId;
            entity.ConversionRate = model.ConversionRate;
            entity.CurrencyId = model.CurrencyId;
            entity.CustomerId = model.CustomerId;
            entity.CustomerSiteId = model.CustomerSiteId;
            entity.Id = model.Id;
            entity.PeriodId = model.PeriodId;
            entity.ReceiptAmount = model.ReceiptAmount;
            entity.ReceiptDate = model.ReceiptDate;
            entity.ReceiptNumber = model.ReceiptNumber;
            entity.Remarks = model.Remarks;
            entity.SOBId = model.SOBId;
            entity.Status = model.Status;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            return entity;
        }
        #endregion

        public static string SaveReceipt(ReceiptViewModel model)
        {
            if (model.Id > 0)
            {
                return service.Update(getEntityByModel(model));
            }
            else
            {
                return service.Insert(getEntityByModel(model));
            }
        }

        public static List<ReceiptViewModel> GetReceipts(long sobId, long periodId, long customerId, long currencyId)
        {
            List<ReceiptViewModel> receipts = service.GetReceipts(sobId, periodId, customerId, currencyId, AuthenticationHelper.CompanyId.Value).
                Select(x => new ReceiptViewModel(x)).ToList();
            return receipts;
        }

        public static ReceiptViewModel GetReceipt(string id)
        {
            ReceiptViewModel receipt = new ReceiptViewModel(service.GetSingle(id, AuthenticationHelper.CompanyId.Value));
            return receipt;
        }

        public static void DeleteReceipt(string id)
        {
            service.Delete(id, AuthenticationHelper.CompanyId.Value);
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
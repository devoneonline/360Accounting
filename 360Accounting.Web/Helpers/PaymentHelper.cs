using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Web.Models;
using _360Accounting.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using _360Accounting.Core.IService;

namespace _360Accounting.Web
{
    public static class PaymentHelper
    {
        private static IPaymentService service;

        static PaymentHelper()
        {
            service = IoC.Resolve<IPaymentService>("PaymentService");
        }

        public static PaymentViewModel GetPayment(string id)
        {
            PaymentModel payment = new PaymentModel(service.GetSingle(id, AuthenticationHelper.User.CompanyId));

            PaymentViewModel paymentView = new PaymentViewModel
            {
                Amount = payment.Amount,
                BankAccountId = payment.BankAccountId,
                BankAccountName = "",
                BankId = payment.BankId,
                CreateBy = payment.CreateBy,
                CreateDate = payment.CreateDate,
                Id = payment.Id,
                PaymentDate = payment.PaymentDate,
                PaymentNo = payment.PaymentNo,
                PeriodId = payment.PeriodId,
                SOBId = payment.SOBId,
                Status = payment.Status,
                UpdateBy = payment.UpdateBy,
                UpdateDate = payment.UpdateDate,
                VendorId = payment.VendorId,
                VendorSiteId = payment.VendorSiteId,
                VendorSiteName = ""
            };

            return paymentView;
        }

        public static PaymentListViewModel GetPayments(long sobId, long bankId, long vendorId, long periodId)
        {
            PaymentListViewModel model = new PaymentListViewModel();
            model.Payments = service.GetAll(AuthenticationHelper.User.CompanyId, vendorId, bankId, sobId, periodId).ToList()
                .Select(x => new PaymentViewModel(x)).ToList();

            return model;
        }

        public static IList<PaymentInvoiceLinesModel> GetPaymentLines([Optional]string headerId)
        {
            if (headerId == null)
                return getPaymentLines();
            else
                return getpaymentLinesbyPaymentId(headerId);
        }

        public static string GetPaymentNo(long companyId, long vendorId, long sobId, long bankId, long periodId)
        {
            var currentDocument = service.GetSingle(companyId, vendorId, bankId, sobId, periodId);
            string newDocNo = "";
            if (currentDocument != null)
            {
                int outVal;
                bool isNumeric = int.TryParse(currentDocument.PaymentNo, out outVal);
                if (isNumeric && currentDocument.PaymentNo.Length == 8)
                {
                    newDocNo = (int.Parse(currentDocument.PaymentNo) + 1).ToString();
                    return newDocNo;
                }
            }

            //Create New DocNum..
            string yearDigit = DateTime.Now.ToString("yy");
            string monthDigit = DateTime.Now.ToString("MM");
            string docNo = int.Parse("1").ToString().PadLeft(4, '0');

            return yearDigit + monthDigit + docNo;
        }

        public static void Update(PaymentViewModel payment)
        {
            PaymentHeader entity = Mappers.GetEntityByModel(payment);

            string result = string.Empty;
            if (entity.IsValid())
            {
                if (payment.Id > 0)
                    result = service.Update(entity);
                else
                    result = service.Insert(entity);

                if (!string.IsNullOrEmpty(result))
                {
                    var savedLines = getpaymentLinesbyPaymentId(result);
                    if (savedLines.Count() > payment.PaymentInvoiceLines.Count())
                    {
                        var tobeDeleted = savedLines.Take(savedLines.Count() - payment.PaymentInvoiceLines.Count());
                        foreach (var item in tobeDeleted)
                        {
                            service.DeleteLine(item.Id, AuthenticationHelper.User.CompanyId);
                        }
                        savedLines = getpaymentLinesbyPaymentId(result);
                    }

                    foreach (var line in payment.PaymentInvoiceLines)
                    {
                        PaymentInvoiceLines lineEntity = Mappers.GetEntityByModel(line);
                        if (lineEntity.IsValid())
                        {
                            lineEntity.PaymentId = Convert.ToInt64(result);
                            if (savedLines.Count() > 0)
                            {
                                lineEntity.Id = savedLines.FirstOrDefault().Id;
                                savedLines.Remove(savedLines.FirstOrDefault(rec => rec.Id == lineEntity.Id));
                                service.Update(lineEntity);
                            }
                            else
                                service.Insert(lineEntity);
                        }
                    }
                }
            }
        }

        public static void UpdatePaymentLine(PaymentInvoiceLinesModel model)
        {
            PaymentViewModel header = SessionHelper.Payment;
            header.PaymentInvoiceLines.FirstOrDefault(x => x.Id == model.Id).Amount = model.Amount;
            header.PaymentInvoiceLines.FirstOrDefault(x => x.Id == model.Id).InvoiceId = model.InvoiceId;
        }

        public static void DeletePaymentLine(PaymentInvoiceLinesModel model)
        {
            PaymentViewModel header = SessionHelper.Payment;
            PaymentInvoiceLinesModel paymentLine = header.PaymentInvoiceLines.FirstOrDefault(x => x.Id == model.Id);
            header.PaymentInvoiceLines.Remove(paymentLine);
        }

        public static void Insert(PaymentInvoiceLinesModel model)
        {
            PaymentViewModel header = SessionHelper.Payment;
            header.PaymentInvoiceLines.Add(model);
        }

        public static void Delete(string id)
        {
            service.Delete(id, AuthenticationHelper.User.CompanyId);
        }

        #region Private Methods
        private static IList<PaymentInvoiceLinesModel> getPaymentLines()
        {
            return SessionHelper.Payment.PaymentInvoiceLines.ToList();
        }

        private static IList<PaymentInvoiceLinesModel> getpaymentLinesbyPaymentId(string headerId)
        {
            List<PaymentInvoiceLinesModel> modelList = service.GetAllLines(Convert.ToInt64(headerId), AuthenticationHelper.User.CompanyId).
                Select(x => new PaymentInvoiceLinesModel(x)).ToList();
            return modelList;
        }
        #endregion
    }
}
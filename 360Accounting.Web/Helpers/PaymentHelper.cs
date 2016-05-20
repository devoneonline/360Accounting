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
            PaymentModel payment = new PaymentModel(service.GetSingle(id, AuthenticationHelper.CompanyId.Value));

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
            model.Payments = service.GetAll(AuthenticationHelper.CompanyId.Value, vendorId, bankId, sobId, periodId).ToList()
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
            PaymentHeader entity = getEntityByModel(payment);

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
                            service.DeleteLine(item.Id, AuthenticationHelper.CompanyId.Value);
                        }
                        savedLines = getpaymentLinesbyPaymentId(result);
                    }

                    foreach (var line in payment.PaymentInvoiceLines)
                    {
                        PaymentInvoiceLines lineEntity = getEntityByModel(line);
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
            service.Delete(id, AuthenticationHelper.CompanyId.Value);
        }

        #region Private Methods
        private static PaymentHeader getEntityByModel(PaymentViewModel model)
        {
            if (model == null) return null;

            PaymentHeader entity = new PaymentHeader();

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
                entity.CompanyId = AuthenticationHelper.CompanyId.Value; //Not exist.. have to do this..
            }

            entity.Amount = model.Amount;
            entity.BankId = model.BankId;
            entity.Id = model.Id;
            entity.PaymentDate = model.PaymentDate;
            entity.PaymentNo = model.PaymentNo;
            entity.Status = model.Status;
            entity.SOBId = model.SOBId;
            entity.BankAccountId = model.BankAccountId;
            entity.VendorId = model.VendorId;
            entity.PeriodId = model.PeriodId;
            entity.VendorSiteId = model.VendorSiteId;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            return entity;
        }

        private static PaymentInvoiceLines getEntityByModel(PaymentInvoiceLinesModel model)
        {
            if (model == null) return null;

            PaymentInvoiceLines entity = new PaymentInvoiceLines
            {
                Amount = model.Amount,
                PaymentId = model.PaymentId,
                InvoiceId = model.InvoiceId,
                Id = model.Id
            };
            if (model.Id == 0)
            {
                entity.CreateBy = AuthenticationHelper.UserId;
                entity.CreateDate = DateTime.Now;
            }
            else
            {
                entity.CreateBy = model.CreateBy;
                entity.CreateDate = model.CreateDate;
            }
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            return entity;
        }

        private static IList<PaymentInvoiceLinesModel> getPaymentLines()
        {
            return SessionHelper.Payment.PaymentInvoiceLines.ToList();
        }

        private static IList<PaymentInvoiceLinesModel> getpaymentLinesbyPaymentId(string headerId)
        {
            List<PaymentInvoiceLinesModel> modelList = service.GetAllLines(Convert.ToInt64(headerId), AuthenticationHelper.CompanyId.Value).
                Select(x => new PaymentInvoiceLinesModel(x)).ToList();
            return modelList;
        }
        #endregion
    }
}
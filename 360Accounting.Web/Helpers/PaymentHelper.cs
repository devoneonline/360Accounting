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
        private static IPaymentHeaderService service;
        private static IPaymentInvoiceLineService lineService;


        static PaymentHelper()
        {
            service = IoC.Resolve<IPaymentHeaderService>("PaymentHeaderService");
            lineService = IoC.Resolve<IPaymentInvoiceLineService>("PaymentInvoiceLineService");
        }

        public static PaymentHeaderModel GetPayment(string id)
        {
            PaymentHeaderModel paymentHeader = new PaymentHeaderModel(service.GetSingle(id, AuthenticationHelper.User.CompanyId));
            paymentHeader.PaymentInvoiceLines = getPaymentLinesByHeaderId(id);

            return paymentHeader;
        }

        public static PaymentHeaderModel GetPaymentHeader(string id)
        {
            return new PaymentHeaderModel(service.GetSingle(id, AuthenticationHelper.User.CompanyId));
        }

        //to be implemented after controller and view creation..
        public static IList<PaymentHeaderModel> GetPaymentHeaders(long sobId, long periodId, long currencyId)
        {
            IList<PaymentHeaderModel> modelList = service
                .GetAll(AuthenticationHelper.User.CompanyId).ToList()
                .Select(x => new PaymentHeaderModel(x)).ToList();
            return modelList;
        }

        public static IList<PaymentInvoiceLinesModel> GetPaymentLines([Optional]string headerId)
        {
            if (headerId == null)
                return getPaymentLines();
            else
                return getPaymentLinesByHeaderId(headerId);
        }

        //Logic of Num generation
        public static string GetDocNo(long companyId, long periodId, long sobId, long currencyId)
        {
            //var currentDocument = service.GetSingle(companyId, periodId, sobId, currencyId);
            //string newDocNo = "";
            //if (currentDocument != null)
            //{
            //    int outVal;
            //    bool isNumeric = int.TryParse(currentDocument.DocumentNo, out outVal);
            //    if (isNumeric && currentDocument.DocumentNo.Length == 8)
            //    {
            //        newDocNo = (int.Parse(currentDocument.DocumentNo) + 1).ToString();
            //        return newDocNo;
            //    }
            //}

            ////Create New DocNum..
            //string yearDigit = DateTime.Now.ToString("yy");
            //string monthDigit = DateTime.Now.ToString("MM");
            //string docNo = int.Parse("1").ToString().PadLeft(4, '0');

            //return yearDigit + monthDigit + docNo;

            return "";
        }

        public static void Update(PaymentHeaderModel payment)
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
                    var savedLines = getPaymentLinesByHeaderId(result);
                    if (savedLines.Count() > payment.PaymentInvoiceLines.Count())
                    {
                        var tobeDeleted = savedLines.Take(savedLines.Count() - payment.PaymentInvoiceLines.Count());
                        foreach (var item in tobeDeleted)
                        {
                            lineService.Delete(item.Id.ToString(), AuthenticationHelper.User.CompanyId);
                        }
                        savedLines = getPaymentLinesByHeaderId(result);
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
                                lineService.Update(lineEntity);
                            }
                            else
                                lineService.Insert(lineEntity);
                        }
                    }
                }
            }
        }

        public static void UpdatePaymentInvoiceLine(PaymentInvoiceLinesModel model)
        {
            PaymentHeaderModel header = SessionHelper.Payment;
            header.PaymentInvoiceLines.FirstOrDefault(x => x.Id == model.Id).Amount = model.Amount;
            header.PaymentInvoiceLines.FirstOrDefault(x => x.Id == model.Id).Id = model.Id;
            header.PaymentInvoiceLines.FirstOrDefault(x => x.Id == model.Id).InvoiceId = model.InvoiceId;
            header.PaymentInvoiceLines.FirstOrDefault(x => x.Id == model.Id).PaymentId = model.PaymentId;
        }

        public static void DeletePaymentLine(PaymentInvoiceLinesModel model)
        {
            PaymentHeaderModel header = SessionHelper.Payment;
            PaymentInvoiceLinesModel paymentLine = header.PaymentInvoiceLines.FirstOrDefault(x => x.Id == model.Id);
            header.PaymentInvoiceLines.Remove(paymentLine);
        }

        public static void Insert(PaymentInvoiceLinesModel model)
        {
            PaymentHeaderModel header = SessionHelper.Payment;
            header.PaymentInvoiceLines.Add(model);
        }

        public static void Delete(string id)
        {
            service.Delete(id, AuthenticationHelper.User.CompanyId);
        }

        #region Private Methods
        private static IList<PaymentInvoiceLinesModel> getPaymentLines()
        {
            return SessionHelper.Payment.PaymentInvoiceLines;
        }

        private static IList<PaymentInvoiceLinesModel> getPaymentLinesByHeaderId(string headerId)
        {
            IList<PaymentInvoiceLinesModel> modelList = lineService.GetAll
                (AuthenticationHelper.User.CompanyId, Convert.ToInt32(headerId)).
                Select(x => new PaymentInvoiceLinesModel(x)).ToList();
            return modelList;
        }
        #endregion
    }
}
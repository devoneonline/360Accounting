using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace _360Accounting.Web
{
    public static class PayableInvoiceHelper
    {
        private static IPayableInvoiceService service;
        private static IPayableInvoiceDetailService detailService;

        static PayableInvoiceHelper()
        {
            service = IoC.Resolve<IPayableInvoiceService>("PayableInvoiceService");
            detailService = IoC.Resolve<IPayableInvoiceDetailService>("PayableInvoiceDetailService");
        }

        #region Private Methods
        private static IList<PayableInvoiceDetailModel> getInvoiceDetailByInvoiceId(string invoiceId)
        {
            IList<PayableInvoiceDetailModel> modelList = detailService
                .GetAll(AuthenticationHelper.User.CompanyId, Convert.ToInt32(invoiceId))
                .Select(x => new PayableInvoiceDetailModel(x)).ToList();
            return modelList;
        }

        private static IList<PayableInvoiceDetailModel> getInvoiceDetail()
        {
            return SessionHelper.PayableInvoice.InvoiceDetail;
        }
        #endregion

        public static IList<PayableInvoiceModel> GetInvoices(long sobId, long periodId)
        {
            IList<PayableInvoiceModel> modelList = service.GetAll(AuthenticationHelper.User.CompanyId, sobId, periodId).Select(x => new PayableInvoiceModel(x)).ToList();
            return modelList;
        }

        public static IList<PayableInvoiceDetailModel> GetInvoiceDetail([Optional]string invoiceId)
        {
            if (invoiceId == null)
                return getInvoiceDetail();
            else
                return getInvoiceDetailByInvoiceId(invoiceId);
        }

        public static void Insert(PayableInvoiceDetailModel model)
        {
            PayableInvoiceModel invoice = SessionHelper.PayableInvoice;
            invoice.InvoiceDetail.Add(model);
        }

        public static void UpdateInvoiceDetail(PayableInvoiceDetailModel model)
        {
            PayableInvoiceModel invoice = SessionHelper.PayableInvoice;

            invoice.InvoiceDetail.FirstOrDefault(x => x.Id == model.Id).CodeCombinationId = model.CodeCombinationId;
            invoice.InvoiceDetail.FirstOrDefault(x => x.Id == model.Id).Amount = model.Amount;
            invoice.InvoiceDetail.FirstOrDefault(x => x.Id == model.Id).Description = model.Description;
        }

        public static void DeleteInvoiceDetail(PayableInvoiceDetailModel model)
        {
            PayableInvoiceModel invoice = SessionHelper.PayableInvoice;
            PayableInvoiceDetailModel invoiceDetail = invoice.InvoiceDetail.FirstOrDefault(x => x.Id == model.Id);
            invoice.InvoiceDetail.Remove(invoiceDetail);
        }

        public static string GetInvoiceNo(long companyId, long sobId, long periodId)
        {
            ///TODO: plz audit this code
            var previousInvoice = service.GetSingle(companyId, sobId, periodId);
            string newInvoiceNo = "";
            if (previousInvoice != null)
            {
                int outVal;
                bool isNumeric = int.TryParse(previousInvoice.InvoiceNo, out outVal);
                if (isNumeric && previousInvoice.InvoiceNo.Length == 8)
                {
                    newInvoiceNo = (int.Parse(previousInvoice.InvoiceNo) + 1).ToString();
                    return newInvoiceNo;
                }
            }

            //Create New Invoice #...
            string yearDigit = SessionHelper.PayableInvoice.InvoiceDate.ToString("yy");
            string monthDigit = SessionHelper.PayableInvoice.InvoiceDate.ToString("MM");
            string invoiceNo = int.Parse("1").ToString().PadLeft(4, '0');

            return yearDigit + monthDigit + invoiceNo;
        }

        public static void Update(PayableInvoiceModel payableInvoiceModel)
        {
            PayableInvoice entity = Mappers.GetEntityByModel(payableInvoiceModel);

            string result = string.Empty;
            if (entity.IsValid())
            {
                if (payableInvoiceModel.Id > 0)
                    result = service.Update(entity);
                else
                    result = service.Insert(entity);

                if (!string.IsNullOrEmpty(result))
                {
                    var savedDetail = getInvoiceDetailByInvoiceId(result);
                    if (savedDetail.Count() > payableInvoiceModel.InvoiceDetail.Count())
                    {
                        var tobeDeleted = savedDetail.Take(savedDetail.Count() - payableInvoiceModel.InvoiceDetail.Count());
                        foreach (var item in tobeDeleted)
                        {
                            detailService.Delete(item.Id.ToString(), AuthenticationHelper.User.CompanyId);
                        }
                        savedDetail = getInvoiceDetailByInvoiceId(result);
                    }

                    foreach (var detail in payableInvoiceModel.InvoiceDetail)
                    {
                        PayableInvoiceDetail detailEntity = Mappers.GetEntityByModel(detail, savedDetail.Count());
                        if (detailEntity.IsValid())
                        {
                            detailEntity.InvoiceId = Convert.ToInt64(result);
                            if (savedDetail.Count() > 0)
                            {
                                detailEntity.Id = savedDetail.FirstOrDefault().Id;
                                savedDetail.Remove(savedDetail.FirstOrDefault(rec => rec.Id == detailEntity.Id));
                                detailService.Update(detailEntity);
                            }
                            else
                                detailService.Insert(detailEntity);
                        }
                    }
                }
            }
        }

        public static PayableInvoiceModel GetInvoice(string id)
        {
            return new PayableInvoiceModel(service.GetSingle(id, AuthenticationHelper.User.CompanyId));
        }
        
        public static void Delete(string id)
        {
            service.Delete(id, AuthenticationHelper.User.CompanyId);
        }

        

        

        
    }
}
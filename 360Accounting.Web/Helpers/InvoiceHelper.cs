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
    public static class InvoiceHelper
    {
        private static IInvoiceService service;
        private static IInvoiceDetailService detailService;

        static InvoiceHelper()
        {
            service = IoC.Resolve<IInvoiceService>("InvoiceService");
            detailService = IoC.Resolve<IInvoiceDetailService>("InvoiceDetailService");
        }

        #region Private Methods
        private static IList<InvoiceDetailModel> getInvoiceDetailByInvoiceId(string InvoiceId)
        {
            
            IList<InvoiceDetailModel> modelList = detailService
                .GetAll(AuthenticationHelper.User.CompanyId, Convert.ToInt32(InvoiceId))
                .Select(x => new InvoiceDetailModel(x)).ToList();
            return modelList;
        }

        private static IList<InvoiceDetailModel> getInvoiceDetail()
        {
            return SessionHelper.Invoice.InvoiceDetail;
        }
        #endregion

        public static IList<InvoiceModel> GetInvoices(long sobId, long periodId, long currencyId)
        {
            IList<InvoiceModel> modelList = service
                .GetAll(AuthenticationHelper.User.CompanyId, sobId, periodId, currencyId).ToList()
                .Select(x => new InvoiceModel(x)).ToList();
            return modelList;
        }

        public static IList<InvoiceDetailModel> GetInvoiceDetail([Optional]string invoiceId)
        {
            if (invoiceId == null)
                return getInvoiceDetail();
            else
                return getInvoiceDetailByInvoiceId(invoiceId);
        }

        public static void Insert(InvoiceDetailModel model)
        {
            InvoiceModel invoice = SessionHelper.Invoice;
            invoice.InvoiceDetail.Add(model);
        }

        public static void UpdateInvoiceDetail(InvoiceDetailModel model)
        {
            InvoiceModel invoice = SessionHelper.Invoice;

            invoice.InvoiceDetail.FirstOrDefault(x => x.Id == model.Id).InvoiceSourceId = model.InvoiceSourceId;
            invoice.InvoiceDetail.FirstOrDefault(x => x.Id == model.Id).Quantity = model.Quantity;
            invoice.InvoiceDetail.FirstOrDefault(x => x.Id == model.Id).Rate = model.Rate;
            invoice.InvoiceDetail.FirstOrDefault(x => x.Id == model.Id).TaxId = model.TaxId;
        }

        public static void DeleteInvoiceDetail(InvoiceDetailModel model)
        {
            InvoiceModel invoice = SessionHelper.Invoice;
            InvoiceDetailModel invoiceDetail = invoice.InvoiceDetail.FirstOrDefault(x => x.Id == model.Id);
            invoice.InvoiceDetail.Remove(invoiceDetail);
        }

        public static string GetInvoiceNo(long companyId, long sobId, long periodId, long currencyId)
        {
            ///TODO: plz audit this code
            var previousInvoice = service.GetSingle(companyId, sobId, periodId, currencyId);
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
            string yearDigit = DateTime.Now.ToString("yy");
            string monthDigit = DateTime.Now.ToString("MM");
            string invoiceNo = int.Parse("1").ToString().PadLeft(4, '0');

            return yearDigit + monthDigit + invoiceNo;
        }

        public static void Update(InvoiceModel invoiceModel)
        {
            Invoice entity = Mappers.GetEntityByModel(invoiceModel);

            string result = string.Empty;
            if (entity.IsValid())
            {
                if (invoiceModel.Id > 0)
                    result = service.Update(entity);
                else
                    result = service.Insert(entity);

                if (!string.IsNullOrEmpty(result))
                {
                    var savedDetail = getInvoiceDetailByInvoiceId(result);
                    if (savedDetail.Count() > invoiceModel.InvoiceDetail.Count())
                    {
                        var tobeDeleted = savedDetail.Take(savedDetail.Count() - invoiceModel.InvoiceDetail.Count());
                        foreach (var item in tobeDeleted)
                        {
                            detailService.Delete(item.Id.ToString(), AuthenticationHelper.User.CompanyId);
                        }
                        savedDetail = getInvoiceDetailByInvoiceId(result);
                    }

                    foreach (var detail in invoiceModel.InvoiceDetail)
                    {
                        InvoiceDetail detailEntity = Mappers.GetEntityByModel(detail);
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

        public static InvoiceModel GetInvoice(string id)
        {
            return new InvoiceModel(service.GetSingle
                (id, AuthenticationHelper.User.CompanyId));
        }

        public static void Delete(string id)
        {
            service.Delete(id, AuthenticationHelper.User.CompanyId);
        }
    }
}
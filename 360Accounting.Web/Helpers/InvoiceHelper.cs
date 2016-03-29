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
        private static InvoiceDetail getEntityByModel(InvoiceDetailModel model)
        {
            if (model == null) return null;
            InvoiceDetail entity = new InvoiceDetail();

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

            entity.Id = model.Id;
            entity.InvoiceId = model.InvoiceId;
            entity.InvoiceSourceId = model.InvoiceSourceId;
            entity.Quantity = model.Quantity;
            entity.Rate = model.Rate;
            entity.TaxId = model.TaxId;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            return entity;
        }

        private static Invoice getEntityByModel(InvoiceModel model)
        {
            if (model == null) return null;
            Invoice entity = new Invoice();

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
            entity.ConversionRate = model.ConversionRate;
            entity.CurrencyId = model.CurrencyId;
            entity.CustomerId = model.CustomerId;
            entity.CustomerSiteId = model.CustomerSiteId;
            entity.Id = model.Id;
            entity.InvoiceDate = model.InvoiceDate;
            entity.InvoiceNo = model.InvoiceNo;
            entity.InvoiceType = model.InvoiceType;
            entity.PeriodId = model.PeriodId;
            entity.Remarks = model.Remarks;
            entity.SOBId = model.SOBId;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;

            return entity;
        }

        private static IList<InvoiceDetailModel> getInvoiceDetailByInvoiceId(string invoiceId)
        {
            IList<InvoiceDetailModel> modelList = detailService
                .GetAll(AuthenticationHelper.User.CompanyId, Convert.ToInt32(invoiceId))
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
                .GetAll(AuthenticationHelper.User.CompanyId, sobId, periodId, currencyId)
                .Select(x => new InvoiceModel(x)).ToList();
            return modelList;
        }

        public static List<SelectListItem> GetInvoices(long sobId, long periodId)
        {
            List<SelectListItem> modelList = service
                .GetInvoices(AuthenticationHelper.User.CompanyId, sobId, periodId)
                .Select(x => new SelectListItem { Text = x.InvoiceNo, Value = x.Id.ToString() }).ToList();
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
            string yearDigit = SessionHelper.Invoice.InvoiceDate.ToString("yy");
            string monthDigit = SessionHelper.Invoice.InvoiceDate.ToString("MM");
            string invoiceNo = int.Parse("1").ToString().PadLeft(4, '0');

            return yearDigit + monthDigit + invoiceNo;
        }

        public static void Update(InvoiceModel invoiceModel)
        {
            Invoice entity = getEntityByModel(invoiceModel);

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
                        InvoiceDetail detailEntity = getEntityByModel(detail);
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
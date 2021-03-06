﻿using _360Accounting.Core;
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

            entity.Amount = model.Amount;
            entity.Id = model.Id;
            entity.InvoiceId = model.InvoiceId;
            entity.InvoiceSourceId = model.InvoiceSourceId;
            entity.ItemId = model.ItemId;
            entity.Quantity = model.Quantity;
            entity.Rate = model.Rate;
            entity.TaxAmount = model.TaxAmount;
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
                .GetAll(AuthenticationHelper.CompanyId.Value, Convert.ToInt32(invoiceId))
                .Select(x => new InvoiceDetailModel(x)).ToList();
            return modelList;
        }

        private static IList<InvoiceDetailModel> getInvoiceDetail()
        {
            return SessionHelper.Invoice.InvoiceDetail;
        }
        #endregion

        public static IList<InvoiceModel> GetInvoices(long sobId)
        {
            IList<InvoiceModel> modelList = service
                .GetAll(AuthenticationHelper.CompanyId.Value, sobId)
                .Select(x => new InvoiceModel
                {
                    CompanyId = x.CompanyId,
                    PeriodId = x.PeriodId,
                    Id = x.Id,
                    CustomerSiteId = x.CustomerSiteId,
                    InvoiceDate = x.InvoiceDate,
                    ConversionRate = x.ConversionRate,
                    CurrencyId = x.CurrencyId,
                    CreateBy = x.CreateBy,
                    CreateDate = x.CreateDate,
                    InvoiceNo = x.InvoiceNo,
                    InvoiceType = x.InvoiceType,
                    PeriodName = x.PeriodName,
                    Remarks = x.Remarks,
                    SOBId = x.SOBId,
                    CurrencyName = x.CurrencyName,
                    CustomerId = x.CustomerId,
                    UpdateBy = x.UpdateBy,
                    UpdateDate = x.UpdateDate
                }).ToList();
            return modelList;
        }

        public static List<SelectListItem> GetInvoices(long sobId, long periodId)
        {
            List<SelectListItem> modelList = service
                .GetInvoices(AuthenticationHelper.CompanyId.Value, sobId, periodId)
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
            model.Amount = model.Quantity * model.Rate;

            TaxDetailModel taxDetail = TaxHelper.GetTaxDetail(model.TaxId.ToString()).FirstOrDefault(x => x.StartDate <= SessionHelper.Invoice.InvoiceDate && x.EndDate >= SessionHelper.Invoice.InvoiceDate);

            if (taxDetail != null)
                model.TaxAmount = model.Amount * taxDetail.Rate / 100;
            else
                model.TaxAmount = 0;

            InvoiceModel invoice = SessionHelper.Invoice;
            invoice.InvoiceDetail.Add(model);
        }

        public static IEnumerable<Invoice> GetByCurrencyId(long currencyId)
        {
            return service.GetByCurrencyId(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, currencyId);
        }

        public static void UpdateInvoiceDetail(InvoiceDetailModel model)
        {
            InvoiceModel invoice = SessionHelper.Invoice;

            invoice.InvoiceDetail.FirstOrDefault(x => x.Id == model.Id).InvoiceSourceId = model.InvoiceSourceId;
            invoice.InvoiceDetail.FirstOrDefault(x => x.Id == model.Id).ItemId = model.ItemId;
            invoice.InvoiceDetail.FirstOrDefault(x => x.Id == model.Id).Quantity = model.Quantity;
            invoice.InvoiceDetail.FirstOrDefault(x => x.Id == model.Id).Rate = model.Rate;
            invoice.InvoiceDetail.FirstOrDefault(x => x.Id == model.Id).TaxId = model.TaxId;
            invoice.InvoiceDetail.FirstOrDefault(x => x.Id == model.Id).Amount = model.Quantity * model.Rate;

            if (model.TaxId != null)
            {
                TaxDetailModel taxDetail = TaxHelper.GetTaxDetail(invoice.InvoiceDetail.FirstOrDefault(x => x.Id == model.Id).TaxId.ToString()).FirstOrDefault(x => x.StartDate <= SessionHelper.Invoice.InvoiceDate && x.EndDate >= SessionHelper.Invoice.InvoiceDate);

                if (taxDetail != null)
                    invoice.InvoiceDetail.FirstOrDefault(x => x.Id == model.Id).TaxAmount = invoice.InvoiceDetail.FirstOrDefault(x => x.Id == model.Id).Amount * taxDetail.Rate / 100;
                else
                    invoice.InvoiceDetail.FirstOrDefault(x => x.Id == model.Id).TaxAmount = 0;
            }
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
                            detailService.Delete(item.Id.ToString(), AuthenticationHelper.CompanyId.Value);
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
                (id, AuthenticationHelper.CompanyId.Value));
        }

        public static void Delete(string id)
        {
            service.Delete(id, AuthenticationHelper.CompanyId.Value);
        }
    }
}
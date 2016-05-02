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
    public static class TaxHelper
    {
        private static ITaxService service;
        private static ITaxDetailService detailService;

        static TaxHelper()
        {
            service = IoC.Resolve<ITaxService>("TaxService");
            detailService = IoC.Resolve<ITaxDetailService>("TaxDetailService");
        }

        #region Private Methods
        private static TaxDetail getEntityByModel(TaxDetailModel model)
        {
            if (model == null) return null;
            TaxDetail entity = new TaxDetail();

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
            entity.CodeCombinationId = model.CodeCombinationId;
            entity.EndDate = model.EndDate;
            entity.Id = model.Id;
            entity.Rate = model.Rate;
            entity.StartDate = model.StartDate;
            entity.TaxId = model.TaxId;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;

            return entity;
        }

        private static Tax getEntityByModel(TaxModel model)
        {
            if (model == null) return null;
            Tax entity = new Tax();

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

            entity.EndDate = model.EndDate;
            entity.Id = model.Id;
            entity.SOBId = model.SOBId;
            entity.StartDate = model.StartDate;
            entity.TaxName = model.TaxName;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;

            return entity;
        }

        private static IList<TaxDetailModel> getTaxDetail()
        {
            return SessionHelper.Tax.TaxDetails;
        }

        private static IList<TaxDetailModel> getTaxDetailByTaxId(string taxId)
        {
            IList<TaxDetailModel> modelList = detailService.GetAll
                    (AuthenticationHelper.CompanyId.Value, 
                    Convert.ToInt32(taxId))
                    .Select(x => new TaxDetailModel(x)).ToList();
            return modelList;
        }
        #endregion

        public static List<SelectListItem> GetTaxes(long sobId, DateTime startDate, DateTime endDate)
        {
            return service.GetAll(AuthenticationHelper.CompanyId.Value, sobId)
                .Where(a => a.StartDate <= startDate && a.EndDate >= endDate)
                .Select(x => new SelectListItem
                {
                    Text = x.TaxName,
                    Value = x.Id.ToString()
                }).ToList();
        }
        
        public static void Delete(string id)
        {
            service.Delete(id, AuthenticationHelper.CompanyId.Value);
        }

        public static TaxModel GetTax(string id)
        {
            return new TaxModel(service.GetSingle
                (id, AuthenticationHelper.CompanyId.Value));
        }

        public static IList<TaxModel> GetTaxes(long sobId)
        {
            IList<TaxModel> modelList = service.GetAll(AuthenticationHelper.CompanyId.Value, sobId)
                .Select(x => new TaxModel(x)).ToList();
            return modelList;
        }

        public static void Update(TaxModel model)
        {
            //tax ko entity mai lia
            Tax entity = getEntityByModel(model);

            string result = string.Empty;
            if (entity.IsValid())
            {
                //edit mode ya new mode?
                if (model.Id > 0)
                    result = service.Update(entity);
                else
                    result = service.Insert(entity);

                if (!string.IsNullOrEmpty(result))
                {
                    //database se tax ki detail uthai
                    IList<TaxDetailModel> taxDetail = getTaxDetailByTaxId(result);

                    //koi row delete tou nai ki detail mai
                    //database se utha kar check kia
                    //new detail aur db detail 
                    //k count ko match kar k
                    if (taxDetail.Count() > model.TaxDetails.Count())
                    {
                        //agar detail delete ki hai tou
                        //unko uthaya jo delete ki hain
                        var toBeDeleted = taxDetail.Take(taxDetail.Count() - model.TaxDetails.Count());
                        foreach (var item in toBeDeleted)
                        {
                            //ek ek kar k delete kia detail ko
                            detailService.Delete(item.Id.ToString(), AuthenticationHelper.CompanyId.Value);
                        }

                        //delete karne k bd db se detail utha li.
                        taxDetail = getTaxDetailByTaxId(result);
                    }

                    //tax detail ki loop chalai
                    foreach (var detailModel in model.TaxDetails)
                    {
                        //detail ki entity li
                        TaxDetail detailEntity = getEntityByModel(detailModel);
                        if (detailEntity.IsValid())
                        {
                            //tax id dali
                            detailEntity.TaxId = Convert.ToInt64(result);

                            //detail mai koi data para hua tou nai hai
                            //agar hai tou usko update kar do
                            if (taxDetail.Count() > 0)
                            {
                                //entity ki id banai
                                detailEntity.Id = taxDetail.FirstOrDefault().Id;

                                //detail se remove ki wo id.
                                //magar aisa q kia ye samajh nai aya???
                                taxDetail.Remove(taxDetail.FirstOrDefault(rec => rec.Id == detailEntity.Id));

                                //entity update kar di.
                                detailService.Update(detailEntity);
                            }
                            else
                                //nai tou insert
                                detailService.Insert(detailEntity);
                        }
                    }
                }
            }
        }

        public static void DeleteTaxDetail(TaxDetailModel model)
        {
            TaxModel tax = SessionHelper.Tax;
            TaxDetailModel taxDetail = tax.TaxDetails.FirstOrDefault(x => x.Id == model.Id);
            tax.TaxDetails.Remove(taxDetail);
        }

        public static void UpdateTaxDetail(TaxDetailModel model)
        {
            TaxModel tax = SessionHelper.Tax;
            tax.TaxDetails.FirstOrDefault(x => x.Id == model.Id).CodeCombinationId = model.CodeCombinationId;
            tax.TaxDetails.FirstOrDefault(x => x.Id == model.Id).StartDate = model.StartDate;
            tax.TaxDetails.FirstOrDefault(x => x.Id == model.Id).EndDate = model.EndDate;
            tax.TaxDetails.FirstOrDefault(x => x.Id == model.Id).Rate = model.Rate;
        }

        public static void Insert(TaxDetailModel model)
        {
            TaxModel tax = SessionHelper.Tax;
            tax.TaxDetails.Add(model);
        }

        public static IList<TaxDetailModel> GetTaxDetail([Optional]string taxId)
        {
            if (taxId != null)
                return getTaxDetailByTaxId(taxId);
            else
                return getTaxDetail();
        }

        public static IEnumerable<TaxDetail> GetByCodeCombinitionId(long codeCombinitionId)
        {
            return detailService.GetByCodeCombinitionId(codeCombinitionId);
        }
    }
}
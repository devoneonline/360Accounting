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
        private static IList<TaxDetailModel> getTaxDetailByTaxId([Optional]string taxId)
        {
            TaxModel tax = SessionHelper.Tax;
            IList<TaxDetailModel> modelList;
            if (tax == null)
                modelList = detailService.GetAll
                    (AuthenticationHelper.User.CompanyId, 
                    Convert.ToInt32(taxId))
                    .Select(x => new TaxDetailModel(x)).ToList();
            else
                modelList = tax.TaxDetails;

            return modelList;
        }
        #endregion

        public static List<SelectListItem> GetTaxes(long sobId, DateTime startDate, DateTime endDate)
        {
            return service.GetAll(AuthenticationHelper.User.CompanyId, sobId)
                .Where(a => a.StartDate <= startDate && a.EndDate >= endDate)
                .Select(x => new SelectListItem
                {
                    Text = x.TaxName,
                    Value = x.Id.ToString()
                }).ToList();
        }
        
        public static void Delete(string id)
        {
            service.Delete(id, AuthenticationHelper.User.CompanyId);
        }

        public static TaxModel GetTax(string id)
        {
            return new TaxModel(service.GetSingle
                (id, AuthenticationHelper.User.CompanyId));
        }

        public static IList<TaxModel> GetTaxes(long sobId)
        {
            IList<TaxModel> modelList = service.GetAll(AuthenticationHelper.User.CompanyId, sobId)
                .Select(x => new TaxModel(x)).ToList();
            return modelList;
        }

        public static void Update(TaxModel model)
        {
            //tax ko entity mai lia
            Tax entity = Mappers.GetEntityByModel(model);

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
                            detailService.Delete(item.Id.ToString(), AuthenticationHelper.User.CompanyId);
                        }

                        //delete karne k bd db se detail utha li.
                        taxDetail = getTaxDetailByTaxId(result);
                    }

                    //tax detail ki loop chalai
                    foreach (var detailModel in model.TaxDetails)
                    {
                        //detail ki entity li
                        TaxDetail detailEntity = Mappers.GetEntityByModel(detailModel);
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
            return getTaxDetailByTaxId(taxId);
        }
    }
}
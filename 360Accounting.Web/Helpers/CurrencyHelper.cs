using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web
{
    public static class CurrencyHelper
    {
        private static ICurrencyService service;

        static CurrencyHelper()
        {
            service = IoC.Resolve<ICurrencyService>("CurrencyService");
        }

        private static Currency getEntityByModel(CurrencyViewModel model)
        {
            if (model == null) return null;

            Currency entity = new Currency();

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

            entity.CurrencyCode = model.CurrencyCode;
            entity.Id = model.Id;
            entity.Name = model.Name;
            entity.Precision = model.Precision;
            entity.SOBId = model.SOBId;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            return entity;
        }

        public static string SaveCurrency(CurrencyViewModel model)
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

        public static CurrencyViewModel GetCurrency(string id)
        {
            return new CurrencyViewModel(service.GetSingle(id, AuthenticationHelper.User.CompanyId));
        }

        public static void DeleteCurrency(string id)
        {
            service.Delete(id, AuthenticationHelper.User.CompanyId);
        }

        public static List<CurrencyViewModel> GetCurrencies(long sobId)
        {
            return service.GetAll(AuthenticationHelper.User.CompanyId, sobId)
                .Select(x => new CurrencyViewModel(x)).ToList();
        }

        public static List<SelectListItem> GetCurrencyList(long sobId)
        {
            List<SelectListItem> currencyList = GetCurrencies(sobId).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            return currencyList;
        }
    }
}
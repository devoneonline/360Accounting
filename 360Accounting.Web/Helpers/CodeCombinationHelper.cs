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
    public static class CodeCombinationHelper
    {
        private static ICodeCombinitionService service;

        static CodeCombinationHelper()
        {
            service = IoC.Resolve<ICodeCombinitionService>("CodeCombinitionService");
        }

        public static CodeCombinitionViewModel GetSingle(long id)
        {
            return new CodeCombinitionViewModel(service.GetSingle(id, AuthenticationHelper.CompanyId.Value));
        }

        public static IList<SelectListItem> GetAccounts(long sobId, DateTime startDate, DateTime endDate)
        {
            //logic editted by uzair, previous one was incorrect.
            //plz check this one also.
            return service.GetAll(AuthenticationHelper.User.CompanyId, sobId, "", false, null, "", "")
                .Where(a => a.StartDate <= startDate && a.EndDate >= endDate)
                .Select(x => new SelectListItem
                {
                    Text = x.CodeCombinitionCode,
                    Value = x.Id.ToString()
                }).ToList();

            //if (SessionHelper.Calendar != null)
            //{
            //    return service.GetAll(AuthenticationHelper.User.CompanyId, sobId, "", false, null, "", "")
            //        .Where(rec => rec.StartDate >= startDate && rec.EndDate <= endDate)
            //            .Select(x => new SelectListItem
            //            {
            //                Text = x.CodeCombinitionCode,
            //                Value = x.Id.ToString()
            //            }).ToList();
            //}
            //else
            //{
            //    return service.GetAll(AuthenticationHelper.User.CompanyId, sobId, "", false, null, "", "")
            //            .Select(x => new SelectListItem
            //            {
            //                Text = x.CodeCombinitionCode,
            //                Value = x.Id.ToString()
            //            }).ToList();
            //}
        }

        public static string SaveCodeCombination(CodeCombinitionCreateViewModel model)
        {
            if (model.Id > 0)
            {
                return service.Update(Mappers.GetEntityByModel(model));
            }
            else
            {
                return service.Insert(Mappers.GetEntityByModel(model));
            }
        }

        public static List<CodeCombinitionViewModel> 
            GetCodeCombinations(CodeCombinitionListModel model)
        {
            return service.GetAll(AuthenticationHelper.User.CompanyId, 
                model.SOBId, model.SearchText, true, model.Page, 
                model.SortColumn, model.SortDirection)
                .Select(x => new CodeCombinitionViewModel(x)).ToList();
        }

        //For Select List Combos..
        public static List<CodeCombinition> GetCodeCombinations(long sobId, long companyId)
        {
            return service.GetAll(companyId, sobId).ToList();
        }

        public static CodeCombinitionCreateViewModel GetCodeCombination(string id)
        {
            return new CodeCombinitionCreateViewModel(service.GetSingle(id, AuthenticationHelper.User.CompanyId));
        }

        public static void Delete(string id)
        {
            service.Delete(id, AuthenticationHelper.User.CompanyId);
        }
    }
}
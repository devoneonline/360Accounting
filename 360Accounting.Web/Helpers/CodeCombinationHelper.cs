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

        private static CodeCombinition getEntityByModel(CodeCombinitionCreateViewModel model)
        {
            if (model == null) return null;

            var entity = new CodeCombinition();

            entity.AllowedPosting = model.AllowedPosting;
            entity.EndDate = model.EndDate;
            entity.Id = model.Id;
            entity.Segment1 = model.Segment1;
            entity.Segment2 = model.Segment2;
            entity.Segment3 = model.Segment3;
            entity.Segment4 = model.Segment4;
            entity.Segment5 = model.Segment5;
            entity.Segment6 = model.Segment6;
            entity.Segment7 = model.Segment7;
            entity.Segment8 = model.Segment8;
            entity.SOBId = model.SOBId;
            entity.StartDate = model.StartDate;

            if (model.Id == 0)
            {
                entity.CompanyId = AuthenticationHelper.User.CompanyId;
                entity.CreateBy = AuthenticationHelper.UserId;
                entity.CreateDate = DateTime.Now;
            }
            else
            {
                entity.CompanyId = model.CompanyId;
                entity.CreateBy = model.CreateBy;
                entity.CreateDate = model.CreateDate;
            }
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;

            return entity;
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
                return service.Update(getEntityByModel(model));
            }
            else
            {
                return service.Insert(getEntityByModel(model));
            }
        }

        public static List<CodeCombinitionViewModel> GetCodeCombinations(CodeCombinitionListModel model)
        {
            return
                service.GetAll(AuthenticationHelper.User.CompanyId, model.SOBId, model.SearchText, true, model.Page, model.SortColumn, model.SortDirection)
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
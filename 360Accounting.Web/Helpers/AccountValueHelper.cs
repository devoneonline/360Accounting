using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _360Accounting.Web
{
    public static class AccountValueHelper
    {
        private static IAccountValueService service;

        static AccountValueHelper()
        {
            service = IoC.Resolve<IAccountValueService>("AccountValueService");
        }

        private static AccountValue getEntityByModel(AccountValueViewModel model)
        {
            if (model == null) return null;
            AccountValue entity = new AccountValue();

            entity.AccountType = model.AccountType;
            entity.ChartId = model.ChartId;
            entity.EndDate = model.EndDate;
            entity.Id = model.Id;
            entity.Levl = model.Levl;
            entity.Segment = model.Segment;
            entity.StartDate = model.StartDate;
            if (model.Id == 0)
            {
                entity.CreateDate = DateTime.Now;
                entity.CreateBy = AuthenticationHelper.UserId;
                entity.CompanyId = AuthenticationHelper.CompanyId.Value;
            }
            else
            {
                entity.CreateBy = model.CreateBy;
                entity.CreateDate = model.CreateDate;
                entity.CompanyId = model.CompanyId;
            }
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            entity.Value = model.Value;
            entity.ValueName = model.ValueName;

            return entity;
        }

        public static List<AccountValueViewModel> GetAccountValues(long chartId, long sobId, string segment, int segmentNo, bool fetchSaved)
        {
            return service.GetAccountValuesBySegment(chartId,sobId, segment, segmentNo, fetchSaved).Select(x => new AccountValueViewModel(x)).ToList();
        }

        public static List<AccountValueViewModel> GetAccountValues(long chartId, long sobId, string segment)
        {
            return service.GetAccountValuesBySegment(chartId, sobId, segment).Select(x => new AccountValueViewModel(x)).ToList();
        }

        public static List<AccountValueViewModel> GetAccountValuesbyChartId(long chartId, long sobId)
        {
            return service.GetAccountValuesByChartId(chartId, sobId).Select(x => new AccountValueViewModel(x)).ToList();
        }

        public static AccountValueViewModel GetAccountValue(string id)
        {
            return new AccountValueViewModel
                (service.GetSingle(id, AuthenticationHelper.CompanyId.Value));
        }

        public static string SaveChartOfAccountValue(AccountValueViewModel model)
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

        public static void Delete(string id)
        {
            service.Delete(id, AuthenticationHelper.CompanyId.Value);
        }

        
    }
}
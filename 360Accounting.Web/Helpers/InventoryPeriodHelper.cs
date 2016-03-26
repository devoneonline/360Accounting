using _360Accounting.Core;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web
{
    public static class InventoryPeriodHelper
    {
        private static IInventoryPeriodService service;

        static InventoryPeriodHelper()
        {
            service = IoC.Resolve<IInventoryPeriodService>("InventoryPeriodService");
        }

        public static List<SelectListItem> GetPeriodList(long sobId)
        {
            List<SelectListItem> periodList = new List<SelectListItem>();
            foreach (InventoryPeriodModel period in InventoryPeriodHelper.GetInventoryPeriods(sobId))
            {
                CalendarViewModel calendar = CalendarHelper.GetCalendar(period.CalendarId.ToString());
                periodList.Add(new SelectListItem
                {
                    Text = calendar.PeriodName,
                    Value = period.Id.ToString()
                });
            }
            return periodList;
        }

        public static InventoryPeriodModel GetInventoryPeriod(string id)
        {
            return new InventoryPeriodModel(service.GetSingle(id, AuthenticationHelper.User.CompanyId));
        }

        public static string Save(InventoryPeriodModel model)
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

        public static void Delete(string id)
        {
            service.Delete(id, AuthenticationHelper.User.CompanyId);
        }

        public static List<InventoryPeriodModel> GetInventoryPeriods(long sobId)
        {
            return service.GetAll(AuthenticationHelper.User.CompanyId, sobId)
                .Select(x => new InventoryPeriodModel(x)).ToList();
        }
    }
}
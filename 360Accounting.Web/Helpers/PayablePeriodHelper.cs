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
    public static class PayablePeriodHelper
    {
        private static IPayablePeriodService service;

        static PayablePeriodHelper()
        {
            service = IoC.Resolve<IPayablePeriodService>("PayablePeriodService");
        }

        public static List<SelectListItem> GetPeriodList(long sobId)
        {
            List<SelectListItem> periodList = new List<SelectListItem>();
            foreach (PayablePeriodModel period in PayablePeriodHelper.GetPayablePeriods(sobId))
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

        public static PayablePeriodModel GetPayablePeriod(string id)
        {
            return new PayablePeriodModel(service.GetSingle(id, AuthenticationHelper.User.CompanyId));
        }

        public static string Save(PayablePeriodModel model)
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

        public static List<PayablePeriodModel> GetPayablePeriods(long sobId)
        {
            return service.GetAll(AuthenticationHelper.User.CompanyId, sobId)
                .Select(x => new PayablePeriodModel(x)).ToList();
        }
    }
}
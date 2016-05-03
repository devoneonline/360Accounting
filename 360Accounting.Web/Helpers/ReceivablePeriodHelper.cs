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
    public static class ReceivablePeriodHelper
    {
        private static IReceivablePeriodService service;

        static ReceivablePeriodHelper()
        {
            service = IoC.Resolve<IReceivablePeriodService>("ReceivablePeriodService");
        }

        private static ReceivablePeriod getEntityByModel(ReceivablePeriodModel model)
        {
            if (model == null) return null;

            ReceivablePeriod entity = new ReceivablePeriod();

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

            entity.CalendarId = model.CalendarId;
            entity.Id = model.Id;
            entity.SOBId = model.SOBId;
            entity.Status = model.Status;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            return entity;
        }

        public static List<SelectListItem> GetPeriodList(long sobId)
        {
            List<SelectListItem> periodList = new List<SelectListItem>();
            foreach (ReceivablePeriodModel period in ReceivablePeriodHelper.GetReceivablePeriods(sobId))
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

        public static ReceivablePeriodModel GetReceivablePeriod(string id)
        {
            return new ReceivablePeriodModel(service.GetSingle(id, AuthenticationHelper.CompanyId.Value));
        }

        public static string Save(ReceivablePeriodModel model)
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

        public static List<ReceivablePeriodModel> GetReceivablePeriods(long sobId)
        {
            return service.GetAll(AuthenticationHelper.CompanyId.Value, sobId)
                .Select(x => new ReceivablePeriodModel(x)).ToList();
        }

        public static IEnumerable<ReceivablePeriod> GetByCalendarId(long calendarId)
        {
            return service.GetByCalendarId(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, calendarId);
        }
    }
}
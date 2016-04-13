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
    public static class CalendarHelper
    {
        private static ICalendarService service;

        static CalendarHelper()
        {
            service = IoC.Resolve<ICalendarService>("CalendarService");
        }

        private static Calendar getEntityByModel(CalendarViewModel model)
        {
            if (model == null) return null;

            Calendar entity = new Calendar();

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

            entity.Adjusting = model.Adjusting;
            entity.ClosingStatus = model.ClosingStatus;
            entity.EndDate = model.EndDate;
            entity.Id = model.Id;
            entity.PeriodName = model.PeriodName;
            entity.PeriodQuarter = model.PeriodQuarter;
            entity.PeriodYear = model.PeriodYear;
            entity.SeqNumber = model.SeqNumber;
            entity.SOBId = model.SOBId;
            entity.StartDate = model.StartDate;
            entity.UpdateDate = DateTime.Now;
            entity.UpdateBy = AuthenticationHelper.UserId;
            return entity;
        }

        public static CalendarViewModel GetPreviousCalendar(long sobId, int periodYear)
        {
            Calendar calendar = service.GetLastCalendarByYear(AuthenticationHelper.CompanyId.Value, sobId, periodYear);
            if (calendar != null)
            {
                SessionHelper.Calendar = new CalendarViewModel(calendar);
            }

            return SessionHelper.Calendar;
        }

        public static CalendarViewModel GetCalendar(string id)
        {
            return new CalendarViewModel(service.GetSingle(id, AuthenticationHelper.User.CompanyId));
        }

        public static string SaveCalendar(CalendarViewModel model)
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
            service.Delete(id, AuthenticationHelper.User.CompanyId);
        }

        public static List<CalendarViewModel> GetCalendars(long sobId)
        {
            return service.GetAll(AuthenticationHelper.User.CompanyId, sobId, "", true, null, "", "")
                .Select(x => new CalendarViewModel(x)).ToList();
        }

        public static List<SelectListItem> GetCalendarsList(long sobId)
        {
            List<SelectListItem> list = GetCalendars(sobId).Select(x => new SelectListItem
                {
                    Text = x.PeriodName,
                    Value = x.Id.ToString()
                }).ToList();
            return list;
        }
    }
}
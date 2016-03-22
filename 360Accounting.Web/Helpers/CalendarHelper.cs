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

        public static CalendarViewModel GetPreviousCalendar(long sobId, int periodYear)
        {
            Calendar calendar = service.GetLastCalendarByYear(AuthenticationHelper.User.CompanyId, sobId, periodYear);
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

        //public static List<CalendarViewModel> GetCalendars(CalendarListModel model)
        //{
        //    return service.GetAll(AuthenticationHelper.User.CompanyId, model.SOBId != 0 ? model.SOBId : Convert.ToInt64(model.SetOfBooks.First().Value), model.SearchText, true, model.Page, model.SortColumn, model.SortDirection)
        //        .Select(x => new CalendarViewModel(x)).ToList();
        //}

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
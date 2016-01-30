using _360Accounting.Core;
using _360Accounting.Data.Repositories;
using _360Accounting.Service;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Controllers
{
    public class CalendarController : Controller
    {
        //private ICalendarService service;
        private ISetOfBookService sobService;

        public CalendarController()
        {
            service = new CalendarService(new CalendarRepository());
            sobService = new SetOfBookService(new SetOfBookRepository());
        }

        public ActionResult Index(CalendarListModel model)
        {
            if (model.SetOfBooks == null)
            {
                model.SetOfBooks = sobService
                    .GetByCompanyId(AuthenticationHelper.User.CompanyId)
                    .Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();
            }

            ////if (model.SOBId != 0 || model.SetOfBooks != null)
            ////{
            ////    model.Calendars = service.GetAll(AuthenticationHelper.User.CompanyId, model.SOBId != 0 ? model.SOBId : Convert.ToInt64(model.SetOfBooks.First().Value), model.SearchText, true, model.Page, model.SortColumn, model.SortDirection)
            ////        .Select(x => new CalendarViewModel(x)).ToList();
            ////}

            return View(model);
        }

        public ActionResult Create(long sobId)
        {
            CalendarViewModel model = new CalendarViewModel();
            model.SOBId = sobId;
            return View(model);
        }
        
    }
}
using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Data.Repositories;
using _360Accounting.Service;
using _360Accounting.Web.Models;
using _360Accounting.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace _360Accounting.Web.Controllers
{
    [Authorize]
    public class CalendarController : Controller
    {
        private ICalendarService service;
        private ISetOfBookService sobService;

        public CalendarController()
        {
            service = IoC.Resolve<ICalendarService>("CalendarService");
            sobService = IoC.Resolve<ISetOfBookService>("SetOfBookService");
        }

        public JsonResult GetPreviousCalendar(long sobId, int periodYear)
        {
            Calendar calendar = service.getLastCalendarByYear(AuthenticationHelper.User.CompanyId, sobId, periodYear);
            if (calendar != null)
            {
                SessionHelper.Calendar = new CalendarViewModel(calendar);
                return Json(SessionHelper.Calendar.SeqNumber, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ChangeStatus(CalendarViewModel model)
        {
            if (ModelState.IsValid)
            {
                Calendar entity = service.GetSingle(model.Id.ToString(), AuthenticationHelper.User.CompanyId);
                entity.ClosingStatus = model.ClosingStatus;
                string result = service.Update(entity);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult ChangeStatus(string id)
        {
            CalendarViewModel model = new CalendarViewModel(service.GetSingle(id, AuthenticationHelper.User.CompanyId));
            return View(model);
        }

        public ActionResult Delete(string id)
        {
            service.Delete(id, AuthenticationHelper.User.CompanyId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(CalendarViewModel model)
        {
            if (ModelState.IsValid)
            {
                string result = service.Update(mapModel(model));
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Edit(string id)
        {
            CalendarViewModel model = new CalendarViewModel(service.GetSingle(id, AuthenticationHelper.User.CompanyId));
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CalendarViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (SessionHelper.Calendar != null && model.StartDate < SessionHelper.Calendar.EndDate)
                {
                    ModelState.AddModelError("Error", "Start date is overlaping with previous period!");
                }
                else if (model.StartDate > model.EndDate)
                {
                    ModelState.AddModelError("Error", "Invalid Dates!");
                }
                else
                {
                    ////Not sure about the validity of this code,
                    ////To be checked later.
                    //////////////////////////////////////////////////
                    ////Calendar duplicateRecord = service.getCalendarByPeriod(AuthenticationHelper.User.CompanyId, model.SOBId, model.StartDate, model.EndDate);
                    //////////////////////////////////////////////////
                    ////if (duplicateRecord == null)
                    ////{
                    model.ClosingStatus = "Open";
                    string result = service.Insert(mapModel(model));    ////TODO: mapper should be in service
                    return RedirectToAction("Index");
                    ////}
                    ////else
                    ////{
                    ////    ModelState.AddModelError("Error", "Period Already Exists!");
                    ////}
                }
            }

            return View(model);
        }

        public ActionResult Create(long sobId)
        {
            CalendarViewModel model = new CalendarViewModel();
            SessionHelper.Calendar = model;
            model.SOBId = sobId;            
            return View(model);
        }

        public ActionResult GetCalendarList(long sobId)
        {
            CalendarListModel model = new CalendarListModel();
            model.SOBId = sobId;
            model.Calendars = getCalendarList(model);
            return PartialView("_List", model);
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
            model.SOBId = model.SOBId > 0 ? model.SOBId : Convert.ToInt64(model.SetOfBooks[0].Value.ToString());

            return View(model);
        }

        #region Private Methods

        private List<CalendarViewModel> getCalendarList(CalendarListModel model)
        {
            return service.GetAll(AuthenticationHelper.User.CompanyId, model.SOBId != 0 ? model.SOBId : Convert.ToInt64(model.SetOfBooks.First().Value), model.SearchText, true, model.Page, model.SortColumn, model.SortDirection)
                .Select(x => new CalendarViewModel(x)).ToList();
        }

        private Calendar mapModel(CalendarViewModel model)
        {
            return new Calendar
            {
                Adjusting = model.Adjusting,
                ClosingStatus = model.ClosingStatus,
                CompanyId = AuthenticationHelper.User.CompanyId,
                CreateDate = DateTime.Now,
                EndDate = model.EndDate,
                Id = model.Id,
                PeriodName = model.PeriodName,
                PeriodQuarter = model.PeriodQuarter,
                PeriodYear = model.PeriodYear,
                SeqNumber = model.SeqNumber,
                SOBId = model.SOBId,
                StartDate = model.StartDate,
                UpdateDate = DateTime.Now
            };
        }
        #endregion

        [ValidateInput(false)]
        public ActionResult CalendarListPartial(long sobId)
        {
            List<CalendarViewModel> model = service.GetAll(AuthenticationHelper.User.CompanyId, sobId, "", true, null, "", "")
                .Select(x => new CalendarViewModel(x)).ToList();

            return PartialView("_List", model);
        }
    }
}
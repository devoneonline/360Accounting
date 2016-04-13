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
using _360Accounting.Common;

namespace _360Accounting.Web.Controllers
{
    [Authorize]
    public class CalendarController : Controller
    {
        public JsonResult GetPreviousCalendar(long sobId, int periodYear)
        {
            CalendarHelper.GetPreviousCalendar(sobId, periodYear);
            if (SessionHelper.Calendar != null)
                return Json(SessionHelper.Calendar.SeqNumber == null ? 0 : SessionHelper.Calendar.SeqNumber, JsonRequestBehavior.AllowGet);
            else
                return Json(0, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ChangeStatus(CalendarViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    CalendarViewModel calendar = CalendarHelper.GetCalendar(model.Id.ToString());
                    calendar.ClosingStatus = model.ClosingStatus;
                    string result = CalendarHelper.SaveCalendar(calendar);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex.Message);
                }
            }

            return View(model);
        }

        public ActionResult ChangeStatus(string id)
        {
            CalendarViewModel model = CalendarHelper.GetCalendar(id);
            return View(model);
        }

        public ActionResult Delete(string id)
        {
            CalendarHelper.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(CalendarViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.StartDate != null && model.EndDate < model.StartDate)
                {
                    ModelState.AddModelError("Error", "Start date is less than End Date!");
                }
                else
                {
                    string result = CalendarHelper.SaveCalendar(model);
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        public ActionResult Edit(string id)
        {
            CalendarViewModel model = CalendarHelper.GetCalendar(id);
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
                else if (model.StartDate != null && model.EndDate < model.StartDate)
                {
                    ModelState.AddModelError("Error", "Start date is less than End Date!");
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
                    string result = CalendarHelper.SaveCalendar(model);    ////TODO: mapper should be in service
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
            CalendarViewModel previousCalendar = CalendarHelper.GetPreviousCalendar(sobId, Const.CurrentDate.Year);

            CalendarViewModel model = new CalendarViewModel();

            if (previousCalendar != null)
            {
                model.StartDate = SessionHelper.Calendar.EndDate.AddDays(1);
                model.EndDate = model.StartDate.AddDays(1);
            }

            model.SOBId = sobId;            
            return View(model);
        }

        public ActionResult GetCalendarList(long sobId)
        {
            CalendarListModel model = new CalendarListModel();
            model.SOBId = sobId;
            model.Calendars = CalendarHelper.GetCalendars(sobId);
            return PartialView("_List", model);
        }

        public ActionResult Index(CalendarListModel model)
        {
            if (model.SetOfBooks == null)
            {
                model.SetOfBooks = SetOfBookHelper.GetSetOfBooks()
                    .Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();
            }
            model.SOBId = model.SOBId > 0 ? model.SOBId : Convert.ToInt64(model.SetOfBooks[0].Value.ToString());

            return View(model);
        }
        
        [ValidateInput(false)]
        public ActionResult CalendarListPartial(long sobId)
        {
            return PartialView("_List", CalendarHelper.GetCalendars(sobId));
        }
    }
}
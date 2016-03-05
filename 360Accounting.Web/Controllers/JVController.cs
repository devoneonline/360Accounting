using _360Accounting.Core;
using _360Accounting.Web.Mvc;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Controllers
{
    public class JVController : Controller
    {
        //private IGLHeaderService service;
        //private IGLLineService lineService;
        private ISetOfBookService sobService;
        private ICalendarService calendarService;
        private ICurrencyService currencyService;

        public JVController()
        {
            sobService = IoC.Resolve<ISetOfBookService>("SetOfBookService");
            calendarService = IoC.Resolve<ICalendarService>("CalendarService");
            currencyService = IoC.Resolve<ICurrencyService>("CurrencyService");
        }

        #region Private Methods
        private List<SelectListItem> getPeriodList(string sobId)
        {
            List<SelectListItem> list = calendarService.GetAll(AuthenticationHelper.User.CompanyId, Convert.ToInt32(sobId))
                    .Select(x => new SelectListItem { Text = x.PeriodName, Value = x.Id.ToString() }).ToList();
            return list;
        }

        private List<SelectListItem> getCurrencyList(string sobId)
        {
            List<SelectListItem> list = currencyService.GetAll(AuthenticationHelper.User.CompanyId, Convert.ToInt32(sobId))
                    .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            return list;

        }
        #endregion

        public ActionResult Delete(string id)
        {
            DataProvider.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            GLHeaderModel model = DataProvider.GetGLHeaders(id);
            SessionHelper.SOBId = model.SOBId;
            model.GlLines = DataProvider.GetGLLines(id);
            SessionHelper.JV = model;
            return View("Create", model);
        }

        public ActionResult Index(JournalVoucherListModel model)
        {
            if (model.SetOfBooks == null)
            {
                model.SetOfBooks = sobService.GetByCompanyId(AuthenticationHelper.User.CompanyId)
                    .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                model.SOBId = model.SetOfBooks.Any() ? Convert.ToInt32(model.SetOfBooks.First().Value) : 0;
                
            }

            if (model.Periods == null && model.SetOfBooks.Any())
            {
                model.Periods = getPeriodList(model.SetOfBooks.First().Value);
                model.PeriodId = model.Periods.Any() ? Convert.ToInt32(model.Periods.First().Value) : 0;                
            }

            if (model.Currencies == null && model.SetOfBooks.Any())
            {
                model.Currencies = getCurrencyList(model.SetOfBooks.First().Value);
                model.CurrencyId = model.Currencies.Any() ? Convert.ToInt32(model.Currencies.First().Value) : 0;
                
            }
            return View(model);
        }

        public ActionResult JVPartialWithModel(JournalVoucherListModel model)
        {
            SessionHelper.SOBId = model.SOBId;
            return PartialView("_List", DataProvider
                .GetGLHeaders(model.SOBId, model.PeriodId,
                model.CurrencyId));
        }

        public ActionResult JVPartial(long sobId, long periodId, long currencyId)
        {
            SessionHelper.SOBId = sobId;
            return PartialView("_List", DataProvider
                .GetGLHeaders(sobId, periodId, currencyId));
        }

        public ActionResult Create(long sobId, long periodId, long currencyId)
        {
            SessionHelper.SOBId = sobId;

            GLHeaderModel model = SessionHelper.JV;
            if (model == null)
            {
                model = new GLHeaderModel();
                model.CompanyId = AuthenticationHelper.User.CompanyId;
                model.SOBId = sobId;
                model.PeriodId = periodId;
                model.CurrencyId = currencyId;
                model.GlLines = DataProvider.GetGLLines();
                SessionHelper.JV = model;
            }
            return View(model);
        }

        public ActionResult CreatePartial()
        {
            return PartialView("createPartial", DataProvider.GetGLLines());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewPartial(GLLinesModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    DataProvider.Insert(model);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("createPartial", DataProvider.GetGLLines());
        }

        public ActionResult DeleteDetailPartial(GLLinesModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    GLHeaderModel header = SessionHelper.JV;
                    header.GlLines.Remove(model);
                    SessionHelper.JV = header;
                    IList<GLLinesModel> glLines = DataProvider.GetGLLines();
                    return PartialView("createPartial", glLines);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("createPartial");
        }
        
        public ActionResult SaveVoucher(string journalName, string glDate, string cRate, string descr)
        {
            try
            {
                DataProvider.Update(journalName, glDate, cRate, descr);
                SessionHelper.JV = null;
                return Json("Success");
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
                return Json("Failure");
            }
        }

        public JsonResult CurrencyList(string sobId)
        {
            return Json(getCurrencyList(sobId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult PeriodList(string sobId)
        {
            return Json(getPeriodList(sobId), JsonRequestBehavior.AllowGet);
        }
    }
}
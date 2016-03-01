using _360Accounting.Core;
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

        //public JVController()
        //{
        //    service = IoC.Resolve<IGLHeaderService>("GLHeaderService");
        //    lineService = IoC.Resolve<IGLLineService>("GLLineService");
        //}

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult JVPartial()
        {
            return PartialView("_List", DataProvider.GetGLHeaders());
        }

        public ActionResult Create(long sobId, long periodId, long currencyId)
        {
            GLHeaderModel model= AuthenticationHelper.JV;
            if (model == null)
            {
                model = new GLHeaderModel();
                model.CompanyId = AuthenticationHelper.User.CompanyId;
                model.SOBId = sobId;
                model.PeriodId = periodId;
                model.CurrencyId = currencyId;
                model.GlLines = DataProvider.GetGLLines();
                AuthenticationHelper.JV = model;
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

        public ActionResult SaveVoucher(string journalName, string glDate, string cRate, string descr)
        {
            try
            {
                DataProvider.Update(journalName, glDate, cRate, descr);
                return Json("Success");
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
                return Json("Failure");
            }
        }

	}
}
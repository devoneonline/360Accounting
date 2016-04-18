using _360Accounting.Core;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Controllers
{
    public class VendorController : Controller
    {
        private ICodeCombinitionService codeCombinationService;
        
        public VendorController()
        {
            codeCombinationService = IoC.Resolve<ICodeCombinitionService>("CodeCombinitionService");
        }

        public ActionResult Index()
        {
            IEnumerable<VendorModel> modelList = VendorHelper.GetAll();
            return View(modelList);
        }

        public ActionResult Create()
        {
            return View(new VendorModel());
        }

        [HttpPost]
        public ActionResult Create(VendorModel model)
        {
            if (ModelState.IsValid)
            {
                VendorHelper.Save(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(string id)
        {
            var model = VendorHelper.GetSingle(id);
            if (model == null)
                return HttpNotFound();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(VendorModel model)
        {
            if (ModelState.IsValid)
            {
                VendorHelper.Save(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(string id)
        {
            VendorHelper.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult VendorListPartial()
        {
            return PartialView("_List", VendorHelper.GetAll());
        }

        public ActionResult ListSites(long id)
        {
            ViewBag.VendorId = id;
            var modelList = VendorHelper.GetAllSites(id);
            return View(modelList);
        }

        public ActionResult ListPartial(long id)
        {
            var modelList = VendorHelper.GetAllSites(id);
            return PartialView("_ListPartial", modelList);
        }

        public ActionResult CreateSite(long id)
        {
            VendorSiteModel model = new VendorSiteModel(id);
            model.CodeCombination = codeCombinationService.GetAllCodeCombinitionView(AuthenticationHelper.CompanyId.Value)
                    .Select(x => new SelectListItem
                    {
                        Text = x.CodeCombinitionCode,
                        Value = x.Id.ToString()
                    }).ToList();
            //model.TaxCode = taxService.GetAll(AuthenticationHelper.CompanyId.value)
            //    .Select(x => new SelectListItem
            //    {
            //        Text = x.TaxName,
            //        Value = x.Id.ToString()
            //    }).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateSite(VendorSiteModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.StartDate != null && model.StartDate > model.EndDate)
                    {
                        ModelState.AddModelError("Error", "Start Date cannot be greater than End Date.");
                    }
                    else
                    {
                        VendorHelper.Save(model);
                        return RedirectToAction("Index", new { Id = model.VendorId });
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex.Message);
                }
                //VendorHelper.Save(model);
                //return RedirectToAction("ListSites", new { Id = model.VendorId });
            }
            return View(model);
        }

        public ActionResult EditSite(long id)
        {
            var model = VendorHelper.GetSingle(id);
            model.CodeCombination = codeCombinationService.GetAllCodeCombinitionView(AuthenticationHelper.CompanyId.Value)
                    .Select(x => new SelectListItem
                    {
                        Text = x.CodeCombinitionCode,
                        Value = x.Id.ToString()
                    }).ToList();
            //model.TaxCode = taxService.GetAll(AuthenticationHelper.CompanyId.Value)
            //    .Select(x => new SelectListItem
            //    {
            //        Text = x.TaxName,
            //        Value = x.Id.ToString()
            //    }).ToList();

            return View(model);
        }

        public ActionResult DeleteSite(long id, long vendorId)
        {
            VendorHelper.Delete(id);
            return RedirectToAction("ListSites", new { Id = vendorId });
        }
	}
}
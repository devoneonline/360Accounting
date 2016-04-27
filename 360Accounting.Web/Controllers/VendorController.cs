using _360Accounting.Core;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Controllers
{
    public class VendorController : BaseController
    {
        private ICodeCombinitionService codeCombinationService;
        
        public VendorController()
        {
            codeCombinationService = IoC.Resolve<ICodeCombinitionService>("CodeCombinitionService");
        }

        public ActionResult Index(string message = "")
        {
            ViewBag.ErrorMessage = message;
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
            try
            {
                VendorHelper.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { message = ex.Message });
            }
        }

        public ActionResult VendorListPartial()
        {
            return PartialView("_List", VendorHelper.GetAll());
        }

        public ActionResult ListSites(long id, string message = "")
        {
            ViewBag.ErrorMessage = message;
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
                    VendorModel vendor = VendorHelper.GetSingle(model.VendorId.ToString());
                    if ((model.StartDate >= vendor.StartDate && model.EndDate <= vendor.EndDate) ||
                        (model.StartDate == null && vendor.StartDate == null ||
                        model.EndDate == null && vendor.EndDate == null))
                    {
                        VendorHelper.Save(model);
                        return RedirectToAction("Index", new { Id = model.VendorId });
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Site Dates must be within the range of Vendor Dates.");
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

            CodeCombinitionCreateViewModel codeCombination = CodeCombinationHelper.GetCodeCombination(model.CodeCombinationId.ToString());

            model.CodeCombinationString = Utility.Stringize(".", codeCombination.Segment1, codeCombination.Segment2, codeCombination.Segment3,
                codeCombination.Segment4, codeCombination.Segment5, codeCombination.Segment6, codeCombination.Segment7, codeCombination.Segment8);


            //model.CodeCombination = codeCombinationService.GetAllCodeCombinitionView(AuthenticationHelper.CompanyId.Value)
            //        .Select(x => new SelectListItem
            //        {
            //            Text = x.CodeCombinitionCode,
            //            Value = x.Id.ToString()
            //        }).ToList();
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
            try
            {
                VendorHelper.Delete(id);
                return RedirectToAction("ListSites", new { Id = vendorId });
            }
            catch (Exception ex)
            {
                return RedirectToAction("ListSites", new { Id = vendorId, message = ex.Message });
            }
        }
	}
}
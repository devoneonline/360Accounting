using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Controllers
{
    public class OrderTypeController : Controller
    {
        public ActionResult Index()
        {
            return View(new OrderTypeListModel());
        }

        public ActionResult CreatePartial()
        {
            IEnumerable<OrderTypeModel> model = OrderTypeHelper.GetOrderTypes();
            return PartialView("_List", model);
        }

        public ActionResult AddNewInline(OrderTypeModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CompanyId = AuthenticationHelper.CompanyId.Value;
                    OrderTypeHelper.Save(model);
                }
                catch (Exception ex)
                {
                    ViewData["EditError"] = ex.Message;
                }
            }
            else
                ViewData["EditError"] = "Please correct all errors";

            return PartialView("_List", OrderTypeHelper.GetOrderTypes());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateInline(OrderTypeModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CompanyId = AuthenticationHelper.CompanyId.Value;
                    OrderTypeHelper.Save(model);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_List", OrderTypeHelper.GetOrderTypes());
        }

        public ActionResult DeleteInline(OrderTypeModel model)
        {
            try
            {
                model.CompanyId = AuthenticationHelper.CompanyId.Value;
                OrderTypeHelper.Delete(model.Id.ToString());
                return PartialView("_List", OrderTypeHelper.GetOrderTypes());
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }

            return PartialView("_List", OrderTypeHelper.GetOrderTypes());
        }
	}
}
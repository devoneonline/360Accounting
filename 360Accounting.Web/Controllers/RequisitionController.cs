using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Controllers
{
    public class RequisitionController : BaseController
    {
        public ActionResult Index()
        {
            SessionHelper.Order = null;
            return View();
        }

        public ActionResult ListPartial()
        {
            return PartialView("_List", OrderHelper.GetOrders());
        }

        public ActionResult Create()
        {
            OrderModel order = SessionHelper.Order;
            if (order == null)
            {
                order = new OrderModel
                {
                    CompanyId = AuthenticationHelper.CompanyId.Value,
                    OrderDate = DateTime.Now,
                    OrderDetail = new List<OrderDetailModel>(),
                    OrderNo = "New",
                    Status = "Booked"
                };
                SessionHelper.Order = order;
            }

            order.Customers = CustomerHelper.GetActiveCustomersCombo(order.OrderDate);
            if (order.Customers != null && order.Customers.Count() > 0)
            {
                order.CustomerId = order.CustomerId > 0 ? order.CustomerId : Convert.ToInt64(order.Customers.FirstOrDefault().Value);
                order.CustomerSites = CustomerHelper.GetCustomerSitesCombo(order.CustomerId);
            }

            order.OrderTypes = OrderTypeHelper.GetOrderTypesCombo(order.OrderDate);
            if (order.OrderTypes != null && order.OrderTypes.Count() > 0)
                order.OrderTypeId = order.OrderTypeId > 0 ? order.OrderTypeId : Convert.ToInt64(order.OrderTypes.FirstOrDefault().Value);

            return View("Edit", order);
        }

        public ActionResult Edit(string id)
        {
            OrderModel order = OrderHelper.GetOrder(id);
            order.OrderDetail = OrderHelper.GetOrderDetail(id);
            SessionHelper.Order = order;

            order.Customers = CustomerHelper.GetCustomersCombo(order.OrderDate, order.OrderDate);
            if (order.Customers != null && order.Customers.Count() > 0)
                order.CustomerSites = CustomerHelper.GetCustomerSitesCombo(order.CustomerId);

            order.OrderTypes = OrderTypeHelper.GetOrderTypesCombo(order.OrderDate);
            if (order.OrderTypes != null && order.OrderTypes.Count() > 0)
                order.OrderTypeId = order.OrderTypeId > 0 ? order.OrderTypeId : Convert.ToInt64(order.OrderTypes.FirstOrDefault().Value);

            return View("Edit", order);
        }

        public ActionResult Delete(string id)
        {
            OrderHelper.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Save(string orderNo, DateTime orderDate, long customerId, long customerSiteId, string remarks, long orderTypeId, string status) 
        {
            if (SessionHelper.Order != null)
            {
                if (SessionHelper.Order.OrderDetail.Count == 0)
                    return Json("No detail information available to save!");

                SessionHelper.Order.CompanyId = AuthenticationHelper.CompanyId.Value;
                SessionHelper.Order.CustomerId = customerId;
                SessionHelper.Order.CustomerSiteId = customerSiteId;
                SessionHelper.Order.OrderDate = orderDate;
                SessionHelper.Order.OrderNo = orderNo;
                SessionHelper.Order.OrderTypeId = orderTypeId;
                SessionHelper.Order.Remarks = remarks;
                SessionHelper.Order.Status = status;

                if (SessionHelper.Order.OrderNo == "New")
                    SessionHelper.Order.OrderNo = OrderHelper.GenerateOrderNum(SessionHelper.Order);

                OrderHelper.Save(SessionHelper.Order);
                SessionHelper.Order = null;
                return Json("Saved Successfully");
            }

            return Json("No information available to save!");
        }

        public ActionResult DetailPartial()
        {
            return PartialView("_Detail", OrderHelper.GetOrderDetail());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewPartial(OrderDetailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (SessionHelper.Order != null)
                    {
                        if (SessionHelper.Order.OrderDetail != null && SessionHelper.Order.OrderDetail.Count() > 0)
                            model.Id = SessionHelper.Order.OrderDetail.LastOrDefault().Id + 1;
                        else
                            model.Id = 1;
                    }
                    else
                        model.Id = 1;

                    OrderHelper.Insert(model);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_Detail", OrderHelper.GetOrderDetail());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdatePartial(OrderDetailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    OrderHelper.Update(model);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_Detail", OrderHelper.GetOrderDetail());
        }

        public ActionResult DeletePartial(OrderDetailModel model)
        {
            try
            {
                OrderHelper.Delete(model);
            }
            catch (Exception ex)
            {
                ViewData["EditError"] = ex.Message;
            }

            IList<OrderDetailModel> orderDetail = OrderHelper.GetOrderDetail();
            return PartialView("_Detail", orderDetail);
        }

        public ActionResult ChangeCombos(DateTime orderDate)
        {
            if(orderDate < DateTime.Now.Date)
                return Json("Order date cannot be the past date!.");

            OrderModel order = new OrderModel();
            order.Customers = CustomerHelper.GetActiveCustomersCombo(orderDate);
            if (order.Customers != null && order.Customers.Count() > 0)
            {
                order.CustomerId = order.CustomerId > 0 ? order.CustomerId : Convert.ToInt64(order.Customers.FirstOrDefault().Value);
                order.CustomerSites = CustomerHelper.GetCustomerSitesCombo(order.CustomerId);
            }

            order.OrderTypes = OrderTypeHelper.GetOrderTypesCombo(orderDate);
            if (order.OrderTypes != null && order.OrderTypes.Count() > 0)
                order.OrderTypeId = order.OrderTypeId > 0 ? order.OrderTypeId : Convert.ToInt64(order.OrderTypes.FirstOrDefault().Value);

            return Json(order);
        }

        public ActionResult GetCustomerSites(long customerId)
        {
            return Json(CustomerHelper.GetCustomerSitesCombo(customerId));
        }
	}
}
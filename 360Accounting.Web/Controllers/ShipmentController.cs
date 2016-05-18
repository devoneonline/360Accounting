using _360Accounting.Web.Models;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Controllers
{
    public class ShipmentController : BaseController
    {
        public static long currentId = 0;

        public ActionResult Index()
        {
            SessionHelper.Shipment = null;
            return View();
        }

        public ActionResult ListPartial()
        {
            return PartialView("_List", ShipmentHelper.GetShipments());
        }

        public ActionResult Create()
        {
            OrderShipmentModel newShipment = SessionHelper.Shipment;
            if (newShipment == null)
            {
                newShipment = new OrderShipmentModel
                {
                    DeliveryDate = DateTime.Now,
                    CompanyId = AuthenticationHelper.CompanyId.Value,
                    OrderShipments = new List<OrderShipmentLine>()
                };
                SessionHelper.Shipment = newShipment;
            }

            newShipment.Customers = CustomerHelper.GetActiveCustomersCombo(newShipment.DeliveryDate);
            if (newShipment.Customers != null && newShipment.Customers.Count() > 0)
            {
                newShipment.CustomerId = newShipment.CustomerId > 0 ? newShipment.CustomerId : 0;
                newShipment.Customers.Add(new SelectListItem { Text = "-- Select --", Value = "0" });
                newShipment.CustomerSites = CustomerHelper.GetCustomerSitesCombo(newShipment.CustomerId);
                if (newShipment.CustomerSites != null && newShipment.CustomerSites.Count() > 0)
                {
                    newShipment.CustomerSiteId = newShipment.CustomerSiteId > 0 ? newShipment.CustomerSiteId : 0;
                    newShipment.CustomerSites.Add(new SelectListItem { Text = "-- Select --", Value = "0" });
                }
            }

            newShipment.Orders = OrderHelper.GetOrdersCombo();
            if (newShipment.Orders != null && newShipment.Orders.Count() > 0)
            {
                newShipment.OrderId = newShipment.OrderId > 0 ? newShipment.OrderId : 0;
                newShipment.Orders.Add(new SelectListItem { Text = "-- Select --", Value = "0" });
            }

            newShipment.Warehouses = WarehouseHelper.GetWarehousesCombo(SessionHelper.SOBId);
            if (newShipment.Warehouses != null && newShipment.Warehouses.Count() > 0)
                newShipment.WarehouseId = newShipment.WarehouseId > 0 ? newShipment.WarehouseId : Convert.ToInt64(newShipment.Warehouses.First().Value);

            newShipment.DeliveryNo = "New";
            SessionHelper.Shipment = newShipment;
            SessionHelper.Shipment.OrderShipments = ShipmentHelper.GetShipment(newShipment.WarehouseId, newShipment.CustomerId, newShipment.CustomerSiteId, newShipment.OrderId).OrderShipments;

            return View("Edit", newShipment);
        }

        public ActionResult Edit(string no, DateTime date)
        {
            OrderShipmentModel orderShipment = ShipmentHelper.GetShipmentEdit(no, date);

            orderShipment.Customers = CustomerHelper.GetActiveCustomersCombo(orderShipment.DeliveryDate);
            if (orderShipment.Customers != null && orderShipment.Customers.Count() > 0)
            {
                orderShipment.CustomerId = orderShipment.CustomerId > 0 ? orderShipment.CustomerId : Convert.ToInt64(orderShipment.Customers.First().Value);
                orderShipment.Customers.Add(new SelectListItem { Text = "-- Select --", Value = "0" });
                orderShipment.CustomerSites = CustomerHelper.GetCustomerSitesCombo(orderShipment.CustomerId);
                if (orderShipment.CustomerSites != null && orderShipment.CustomerSites.Count() > 0)
                {
                    orderShipment.CustomerSiteId = orderShipment.CustomerSiteId > 0 ? orderShipment.CustomerSiteId : Convert.ToInt64(orderShipment.CustomerSites.First().Value);
                    orderShipment.CustomerSites.Add(new SelectListItem { Text = "-- Select --", Value = "0" });
                }
            }

            orderShipment.Orders = OrderHelper.GetOrdersCombo();
            if (orderShipment.Orders != null && orderShipment.Orders.Count() > 0)
            {
                orderShipment.OrderId = orderShipment.OrderId > 0 ? orderShipment.OrderId : Convert.ToInt64(orderShipment.Orders.First().Value);
                orderShipment.Orders.Add(new SelectListItem { Text = "-- Select --", Value = "0" });
            }

            orderShipment.Warehouses = WarehouseHelper.GetWarehousesCombo(SessionHelper.SOBId);
            if (orderShipment.Warehouses != null && orderShipment.Warehouses.Count() > 0)
                orderShipment.WarehouseId = orderShipment.WarehouseId > 0 ? orderShipment.WarehouseId : Convert.ToInt64(orderShipment.Warehouses.First().Value);

            SessionHelper.Shipment = orderShipment;

            return View("Edit", orderShipment);
        }

        public ActionResult Delete(string no, DateTime date)
        {
            ShipmentHelper.Delete(no, date);
            return RedirectToAction("Index");
        }

        public ActionResult DetailPartial()
        {
            //Temporary, because unable to pass Grid column as a parameter to fill combo in grid
            SessionHelper.ItemId = 6;
            //Because Id field is not passing when updating the row, saving the Id of the particular record here on Edit click..

            //Edit or Delete..
            if (SessionHelper.Shipment.OrderShipments.Count() == 1)
            {
                currentId = SessionHelper.Shipment.OrderShipments.First().Id;
            }

            return PartialView("_Detail", SessionHelper.Shipment.OrderShipments);
        }

        public ActionResult DetailPartialParams(long warehouseId, long customerId, long customerSiteId, long orderId)
        {
            SessionHelper.Shipment.OrderShipments = ShipmentHelper.GetShipment(warehouseId, customerId, customerSiteId, orderId).OrderShipments;
            return PartialView("_Detail", SessionHelper.Shipment.OrderShipments);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewPartial(OrderShipmentLine model)
        {
            //Never be in this case..
            if (ModelState.IsValid)
            {
                try
                {
                    if (SessionHelper.Order.OrderDetail != null && SessionHelper.Order.OrderDetail.Count() > 0)
                        model.Id = SessionHelper.Invoice.InvoiceDetail.LastOrDefault().Id + 1;
                    else
                        model.Id = 1;

                    string result = ShipmentHelper.Insert(model);
                    if (!string.IsNullOrEmpty(result))
                        ViewData["EditError"] = result;
                }
                catch (Exception ex)
                {
                    ViewData["EditError"] = ex.Message;
                }
            }
            else
                ViewData["EditError"] = "Please correct all errors!";
            return PartialView("_Detail", SessionHelper.Shipment.OrderShipments);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdatePartial(OrderShipmentLine model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Temporary, because unable to pass Grid column as a parameter to fill combo in grid
                    model.ItemId = SessionHelper.ItemId;
                    model.Id = currentId;
                    string result = ShipmentHelper.Update(model);
                    if (!string.IsNullOrEmpty(result))
                        ViewData["EditError"] = result;
                }
                catch (Exception ex)
                {
                    ViewData["EditError"] = ex.Message;
                }
            }
            else
                ViewData["EditError"] = "Please correct all errors!";

            return PartialView("_Detail", SessionHelper.Shipment.OrderShipments);
        }

        public ActionResult DeletePartial(OrderShipmentLine model)
        {
            try
            {
                model.Id = currentId;
                string result = ShipmentHelper.Delete(model);
                if (!string.IsNullOrEmpty(result))
                    ViewData["EditError"] = result;
            }
            catch (Exception ex)
            {
                ViewData["EditError"] = ex.Message;
            }

            return PartialView("_Detail", SessionHelper.Shipment.OrderShipments);
        }

        public ActionResult Save(DateTime deliveryDate, string deliveryNo, long warehouseId, long companyId)
        {
            SessionHelper.Shipment.WarehouseId = warehouseId;
            SessionHelper.Shipment.CompanyId = companyId;

            if (deliveryNo == "New")
            {
                SessionHelper.Shipment.DeliveryDate = DateTime.Now;
                SessionHelper.Shipment.DeliveryNo = ShipmentHelper.GenerateDeliveryNum(SessionHelper.Shipment);
            }
            else
            {
                SessionHelper.Shipment.DeliveryDate = deliveryDate;
                SessionHelper.Shipment.DeliveryNo = deliveryNo;
            }

            string result = ShipmentHelper.Save(SessionHelper.Shipment);
            SessionHelper.Shipment = null;
            return Json(result);
        }

        public ActionResult GetCustomerSites(long customerId)
        {
            List<SelectListItem> customerSites = CustomerHelper.GetCustomerSitesCombo(customerId);
            customerSites.Add(new SelectListItem { Text = "-- Select --", Value = "0" });
            return Json(customerSites);
        }

        public ActionResult GetCustomers(long orderId, DateTime deliveryDate)
        {
            List<SelectListItem> customerList = new List<SelectListItem>();
            if (orderId > 0)
            {
                CustomerModel customer = CustomerHelper.GetCustomer(OrderHelper.GetOrder(orderId.ToString()).CustomerId.ToString());
                customerList.Add(new SelectListItem
                    {
                        Value = customer.Id.ToString(),
                        Text = customer.CustomerName
                    });
            }
            else
                customerList = CustomerHelper.GetActiveCustomersCombo(deliveryDate);

            customerList.Add(new SelectListItem { Text = "-- Select --", Value = "0" });
            return Json(customerList);
        }
    }
}
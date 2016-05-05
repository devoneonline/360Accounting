using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Controllers
{
    public class ShipmentController : Controller
    {
        public ActionResult Index()
        {
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
                newShipment.CustomerId = newShipment.CustomerId > 0 ? newShipment.CustomerId : Convert.ToInt64(newShipment.Customers.FirstOrDefault().Value);
                newShipment.CustomerSites = CustomerHelper.GetCustomerSitesCombo(newShipment.CustomerId);
                if (newShipment.CustomerSites != null && newShipment.CustomerSites.Count() > 0)
                    newShipment.CustomerSiteId = newShipment.CustomerSiteId > 0 ? newShipment.CustomerSiteId : Convert.ToInt64(newShipment.CustomerSites.FirstOrDefault().Value);
            }

            newShipment.Orders = OrderHelper.GetOrdersCombo();
            if (newShipment.Orders != null && newShipment.Orders.Count() > 0)
                newShipment.OrderId = newShipment.OrderId > 0 ? newShipment.OrderId : Convert.ToInt64(newShipment.Orders.FirstOrDefault().Value);

            newShipment.Warehouses = WarehouseHelper.GetWarehousesCombo(SessionHelper.SOBId);
            if (newShipment.Warehouses != null && newShipment.Warehouses.Count() > 0)
                newShipment.WarehouseId = newShipment.WarehouseId > 0 ? newShipment.WarehouseId : Convert.ToInt64(newShipment.Warehouses.FirstOrDefault().Value);

            SessionHelper.Shipment = newShipment;
            SessionHelper.Shipment.OrderShipments = ShipmentHelper.GetShipment(newShipment.WarehouseId, newShipment.CustomerId, newShipment.CustomerSiteId, newShipment.OrderId).OrderShipments;

            ViewBag.IsNewRecord = true;

            return View("Edit", newShipment);
        }

        public ActionResult Edit(string id)//orderId
        {
            OrderShipmentModel orderShipment = ShipmentHelper.GetShipment(id);

            ViewBag.Customer = CustomerHelper.GetCustomer(orderShipment.CustomerId.ToString()).CustomerName;
            ViewBag.CustomerSite = CustomerHelper.GetCustomerSite(orderShipment.CustomerSiteId.ToString()).SiteName;
            ViewBag.OrderNo = OrderHelper.GetOrder(orderShipment.OrderId.ToString()).OrderNo;
            ViewBag.Warehouse = WarehouseHelper.GetWarehouse(orderShipment.WarehouseId.ToString()).WarehouseName;

            SessionHelper.Shipment = orderShipment;

            ViewBag.IsNewRecord = false;

            return View("Edit", orderShipment);
        }

        public ActionResult Delete(string Id)
        {
            ShipmentHelper.Delete(Convert.ToInt64(Id));
            return RedirectToAction("Index");
        }

        public ActionResult DetailPartial()
        {
            return PartialView("_Detail", SessionHelper.Shipment.OrderShipments);
        }

        public ActionResult DetailPartialParams(long warehouseId, long customerId, long customerSiteId, long orderId)
        {
            return PartialView("_Detail", ShipmentHelper.GetShipment(warehouseId, customerId, customerSiteId, orderId).OrderShipments);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewPartial(OrderShipmentLine model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (SessionHelper.Order.OrderDetail != null && SessionHelper.Order.OrderDetail.Count() > 0)
                        model.Id = SessionHelper.Invoice.InvoiceDetail.LastOrDefault().Id + 1;
                    else
                        model.Id = 1;

                    ShipmentHelper.Insert(model);
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
                    ShipmentHelper.Update(model);
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
                ShipmentHelper.Delete(model);
            }
            catch (Exception ex)
            {
                ViewData["EditError"] = ex.Message;
            }

            return PartialView("_Detail", SessionHelper.Shipment.OrderShipments);
        }

        public ActionResult Save(DateTime deliveryDate, long orderId, long warehouseId)
        {
            SessionHelper.Shipment.DeliveryDate = deliveryDate;
            SessionHelper.Shipment.OrderId = orderId;
            SessionHelper.Shipment.WarehouseId = warehouseId;

            return Json(ShipmentHelper.Save(SessionHelper.Shipment));
        }

        public ActionResult ChangeCombos(DateTime deliveryDate)
        {
            if (deliveryDate < DateTime.Now.Date)
                return Json("Shipment date can not be the past date!");

            OrderShipmentModel changedCombo = new OrderShipmentModel();
            changedCombo.Customers = CustomerHelper.GetActiveCustomersCombo(deliveryDate);
            if (changedCombo.Customers != null && changedCombo.Customers.Count() > 0)
            {
                changedCombo.CustomerId = changedCombo.CustomerId > 0 ? changedCombo.CustomerId : Convert.ToInt64(changedCombo.Customers.FirstOrDefault().Value);
                changedCombo.CustomerSites = CustomerHelper.GetCustomerSitesCombo(changedCombo.CustomerId);
            }

            return Json(changedCombo);
        }

        public ActionResult GetCustomerSites(long customerId)
        {
            return Json(CustomerHelper.GetCustomerSitesCombo(customerId));
        }


	}
}
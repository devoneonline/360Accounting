using _360Accounting.Web.Models;
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

        public ActionResult Edit(string id, DateTime date)
        {
            OrderShipmentModel orderShipment = ShipmentHelper.GetShipmentEdit(id, date);

            ViewBag.Customer = CustomerHelper.GetCustomer(orderShipment.CustomerId.ToString()).CustomerName;
            ViewBag.CustomerSite = CustomerHelper.GetCustomerSite(orderShipment.CustomerSiteId.ToString()).SiteName;
            ViewBag.OrderNo = OrderHelper.GetOrder(orderShipment.OrderId.ToString()).OrderNo;
            ViewBag.Warehouse = WarehouseHelper.GetWarehouse(orderShipment.WarehouseId.ToString()).WarehouseName;

            SessionHelper.Shipment = orderShipment;

            ViewBag.IsNewRecord = false;

            return View("Edit", orderShipment);
        }

        public ActionResult Delete(string Id, DateTime date)
        {
            ShipmentHelper.Delete(Convert.ToInt64(Id), date);
            return RedirectToAction("Index");
        }

        public ActionResult DetailPartial()
        {
            //Because Id field is not passing when updating the row, saving the Id of the particular record here on Edit click..

            //Edit or Delete..
            if (SessionHelper.Shipment.OrderShipments.Count() == 1)
                currentId = SessionHelper.Shipment.OrderShipments.FirstOrDefault().Id;

            return PartialView("_Detail", SessionHelper.Shipment.OrderShipments);
        }

        public ActionResult NoData()
        {
            return PartialView("_Detail", new List<OrderShipmentLine>());
        }

        public ActionResult DetailPartialParams(long warehouseId, long customerId, long customerSiteId, long orderId)
        {
            SessionHelper.Shipment.OrderShipments = ShipmentHelper.GetShipment(warehouseId, customerId, customerSiteId, orderId).OrderShipments;
            return PartialView("_Detail", SessionHelper.Shipment.OrderShipments);
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

                    string result = ShipmentHelper.Insert(model);
                    if(!string.IsNullOrEmpty(result))
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
            //Showing two records on new.. Check.. start from here..
            if (ModelState.IsValid)
            {
                try
                {
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
                if(!string.IsNullOrEmpty(result))
                    ViewData["EditError"] = result;
            }
            catch (Exception ex)
            {
                ViewData["EditError"] = ex.Message;
            }

            return PartialView("_Detail", SessionHelper.Shipment.OrderShipments);
        }

        public ActionResult Save(long orderId, long warehouseId, long companyId)
        {
            SessionHelper.Shipment.DeliveryDate = DateTime.Now;
            SessionHelper.Shipment.OrderId = orderId;
            SessionHelper.Shipment.WarehouseId = warehouseId;
            SessionHelper.Shipment.CompanyId = companyId;

            string result = ShipmentHelper.Save(SessionHelper.Shipment);
            SessionHelper.Shipment = null;
            return Json(result);
        }

        public ActionResult GetCustomerSites(long customerId)
        {
            return Json(CustomerHelper.GetCustomerSitesCombo(customerId));
        }
	}
}
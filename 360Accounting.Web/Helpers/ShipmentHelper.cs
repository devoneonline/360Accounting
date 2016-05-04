using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _360Accounting.Web
{
    public static class ShipmentHelper
    {
        private static IShipmentService service;

        static ShipmentHelper()
        {
            service = IoC.Resolve<IShipmentService>("ShipmentService");
        }

        #region Private Methods

        private static Shipment getEntityByModel(ShipmentModel model)
        {
            if (model == null) return null;

            Shipment entity = new Shipment();

            if (model.Id == 0)
            {
                entity.CreateBy = AuthenticationHelper.UserId;
                entity.CreateDate = DateTime.Now;
                entity.CompanyId = AuthenticationHelper.CompanyId.Value;
            }
            else
            {
                entity.CreateBy = model.CreateBy;
                entity.CreateDate = model.CreateDate;
            }

            entity.DeliveryDate = model.DeliveryDate;
            entity.Id = model.Id;
            entity.LineId = model.LineId;
            entity.LocatorId = model.LocatorId;
            entity.LotNo = model.LotNo;
            entity.OrderId = model.OrderId;
            entity.Quantity = model.Quantity;
            entity.SerialNo = model.SerialNo;
            entity.SOBId = SessionHelper.SOBId;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            entity.WarehouseId = model.WarehouseId;

            return entity;
        }

        private static OrderShipmentLine fromOrderDetailtoShipment(OrderDetailModel model, List<ShipmentModel> shipments)
        {
            return new OrderShipmentLine
            {
                LineId = model.Id,
                OrderQuantity = model.Quantity,
                BalanceQuantity = model.Quantity - shipments.Sum(rec => rec.Quantity),
                ShipedQuantity = shipments.Sum(rec => rec.Quantity),
                ThisShipQuantity = model.Quantity - shipments.Sum(rec => rec.Quantity),
                ItemName = ItemHelper.GetItem(model.ItemId.ToString()).ItemName
            };
        }

        private static string validateShipmentQuantity(OrderShipmentLine model)
        {
            decimal totalQty = model.ThisShipQuantity;
            OrderDetailModel orderDetail = OrderHelper.GetSingleOrderDetail(model.LineId);
            List<Shipment> shiped = service.GetAllByLineId(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, model.LineId).ToList();
            if (shiped != null && shiped.Count() > 0)
                totalQty += shiped.Sum(rec => rec.Quantity);
            if (totalQty > orderDetail.Quantity)
                return "Quantity can not be more than order";
            //Other validations..

            return "";
        }

        #endregion

        public static List<ShipmentModel> GetShipments()
        {
            List<ShipmentModel> shipments = service.GetAll(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId).Select(x => new ShipmentModel(x)).ToList();
            return shipments;
        }

        public static OrderShipmentModel GetShipment(string orderId)
        {
            List<ShipmentModel> shipmentDetail = service.GetAllByOrderId(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, Convert.ToInt64(orderId)).Select(x => new ShipmentModel(x)).ToList();
            OrderModel order = OrderHelper.GetOrder(orderId);
            OrderShipmentModel orderShipment = new OrderShipmentModel(shipmentDetail.FirstOrDefault(), order.CustomerId, order.CustomerSiteId);
            orderShipment.OrderShipments = new List<OrderShipmentLine>();
            foreach (var item in shipmentDetail)
            {
                string itemName = "";
                OrderDetailModel orderDetail = OrderHelper.GetSingleOrderDetail(item.LineId);
                if (orderDetail != null)
                    itemName = ItemHelper.GetItem(orderDetail.ItemId.ToString()).ItemName;
                orderShipment.OrderShipments.Add(new OrderShipmentLine(item, itemName));
            }

            return orderShipment;
        }

        public static List<ShipmentModel> GetShipments(long lineId)
        {
            List<ShipmentModel> shipments = service.GetAllByLineId(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, lineId).Select(x => new ShipmentModel(x)).ToList();
            return shipments;
        }

        public static OrderShipmentModel GetShipment(long warehouseId, long customerId, long customerSiteId, long orderId)
        {
            OrderShipmentModel newShipment = new OrderShipmentModel
            {
                CustomerId = customerId,
                CustomerSiteId = customerSiteId,
                DeliveryDate = DateTime.Now,
                Id = 0,
                OrderId = orderId,
                OrderShipments = new List<OrderShipmentLine>(),
                WarehouseId = warehouseId
            };
            OrderModel order = OrderHelper.GetOrders(customerId, customerId).FirstOrDefault(rec => rec.Id == orderId);
            if (order != null)
            {
                List<OrderDetailModel> orderDetail = OrderHelper.GetOrderDetail(order.Id.ToString()).ToList();
                if (orderDetail != null && orderDetail.Count() > 0)
                {
                    orderDetail = orderDetail.Where(rec => rec.WarehouseId == warehouseId).ToList();
                    if (orderDetail != null && orderDetail.Count() > 0)
                    {
                        foreach (var detailItem in orderDetail)
                        {
                            List<ShipmentModel> shipments = GetShipments(detailItem.Id);
                            if (shipments != null && shipments.Count() > 0)
                            {
                                if (shipments.Sum(rec => rec.Quantity) < detailItem.Quantity)
                                    newShipment.OrderShipments.Add(fromOrderDetailtoShipment(detailItem, shipments));
                            }
                            else
                                newShipment.OrderShipments.Add(fromOrderDetailtoShipment(detailItem, shipments));
                        }
                    }
                }
            }

            return newShipment;
        }

        public static string Insert(OrderShipmentLine model)
        {
            string validationResult = validateShipmentQuantity(model);
            if (string.IsNullOrEmpty(validationResult))
            {
                OrderShipmentModel orderShipment = SessionHelper.Shipment;
                orderShipment.OrderShipments.Add(model);
                return "Record added";
            }
            else
                return validationResult;
        }

        //Assuming that lineId is unique, no record can be entered without its order..
        public static string Update(OrderShipmentLine model)
        {
            string validationResult = validateShipmentQuantity(model);
            if (string.IsNullOrEmpty(validationResult))
            {
                OrderShipmentModel orderShipment = SessionHelper.Shipment;
                orderShipment.OrderShipments.FirstOrDefault(x => x.LineId == model.LineId).BalanceQuantity = model.BalanceQuantity;
                orderShipment.OrderShipments.FirstOrDefault(x => x.LineId == model.LineId).ItemName = model.ItemName;
                orderShipment.OrderShipments.FirstOrDefault(x => x.LineId == model.LineId).LineId = model.LineId;
                orderShipment.OrderShipments.FirstOrDefault(x => x.LineId == model.LineId).LocatorId = model.LocatorId;
                orderShipment.OrderShipments.FirstOrDefault(x => x.LineId == model.LineId).LotNo = model.LotNo;
                orderShipment.OrderShipments.FirstOrDefault(x => x.LineId == model.LineId).OrderQuantity = model.OrderQuantity;
                orderShipment.OrderShipments.FirstOrDefault(x => x.LineId == model.LineId).SerialNo = model.SerialNo;
                orderShipment.OrderShipments.FirstOrDefault(x => x.LineId == model.LineId).ShipedQuantity = model.ShipedQuantity;
                orderShipment.OrderShipments.FirstOrDefault(x => x.LineId == model.LineId).ThisShipQuantity = model.ThisShipQuantity;

                return "Record added";
            }
            else
                return validationResult;
        }

        //Assuming that lineId is unique, no record can be entered without its order..
        public static string Delete(OrderShipmentLine model)
        {
            OrderShipmentModel orderShipment = SessionHelper.Shipment;
            OrderShipmentLine shipment = orderShipment.OrderShipments.FirstOrDefault(x => x.LineId == model.LineId);
            SessionHelper.Shipment.OrderShipments.Remove(shipment);

            return "Record removed";
        }

        //Assuming that Shipment could not be edited.
        public static string Save(OrderShipmentModel model)
        {
            return "";
        }

        public static void Delete(long orderId)
        {
 
        }
    }
}
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

        private static List<Shipment> getEntitiesByModel(OrderShipmentModel model)
        {
            if (model == null) return null;
            List<Shipment> entityList = new List<Shipment>();

            if (model.OrderShipments == null || model.OrderShipments.Count == 0)
                return null;
            foreach (var item in model.OrderShipments)
            {
                Shipment entity = new Shipment();
                if (item.Id == 0)
                {
                    entity.CreateBy = AuthenticationHelper.UserId;
                    entity.CreateDate = DateTime.Now;
                    entity.CompanyId = AuthenticationHelper.CompanyId.Value;
                }
                else
                {
                    entity.CreateBy = model.CreateBy;
                    entity.CreateDate = model.CreateDate;
                    entity.CompanyId = model.CompanyId;
                }
                entity.DeliveryDate = model.DeliveryDate;
                entity.SOBId = SessionHelper.SOBId;
                entity.WarehouseId = model.WarehouseId;
                entity.OrderId = model.OrderId;
                entity.UpdateBy = model.UpdateBy;
                entity.UpdateDate = model.UpdateDate;
                entity.Id = item.Id;
                entity.LineId = item.LineId;
                entity.LocatorId = item.LocatorId;
                entity.LotNo = item.LotNo;
                entity.Quantity = item.ThisShipQuantity;
                entity.SerialNo = item.SerialNo;

                entityList.Add(entity);
            }
            return entityList;
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
            List<Shipment> shiped = new List<Shipment>();
            if (model.Id > 0)
                shiped = service.GetAllByLineId(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, model.LineId)
                    .Where(rec => rec.Id < model.Id).ToList();
            else
                shiped = service.GetAllByLineId(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, model.LineId).ToList();

            if (shiped != null && shiped.Count() > 0)
                totalQty += shiped.Sum(rec => rec.Quantity);
            if (totalQty > orderDetail.Quantity)
                return "Quantity is exceeding than order!";
            //Other validations if any..

            OrderShipmentModel orderShipment = SessionHelper.Shipment;
            if (model.Id > 0)
            {
                orderShipment.OrderShipments.FirstOrDefault(x => x.LineId == model.LineId).BalanceQuantity = orderDetail.Quantity - (shiped.Sum(rec => rec.Quantity) + model.ThisShipQuantity);
                orderShipment.OrderShipments.FirstOrDefault(x => x.LineId == model.LineId).Id = model.Id;
                orderShipment.OrderShipments.FirstOrDefault(x => x.LineId == model.LineId).LocatorId = model.LocatorId;
                orderShipment.OrderShipments.FirstOrDefault(x => x.LineId == model.LineId).LotNo = model.LotNo;
                orderShipment.OrderShipments.FirstOrDefault(x => x.LineId == model.LineId).OrderQuantity = orderDetail.Quantity;
                orderShipment.OrderShipments.FirstOrDefault(x => x.LineId == model.LineId).SerialNo = model.SerialNo;
                orderShipment.OrderShipments.FirstOrDefault(x => x.LineId == model.LineId).ShipedQuantity = shiped.Sum(rec => rec.Quantity);
                orderShipment.OrderShipments.FirstOrDefault(x => x.LineId == model.LineId).ThisShipQuantity = model.ThisShipQuantity;
            }
            else
            {
                if (orderShipment.OrderShipments.Any(x => x.LineId == model.LineId))
                {
                    orderShipment.OrderShipments.FirstOrDefault(x => x.LineId == model.LineId).BalanceQuantity = orderDetail.Quantity - (shiped.Sum(rec => rec.Quantity) + model.ThisShipQuantity);
                    orderShipment.OrderShipments.FirstOrDefault(x => x.LineId == model.LineId).Id = model.Id;
                    orderShipment.OrderShipments.FirstOrDefault(x => x.LineId == model.LineId).LocatorId = model.LocatorId;
                    orderShipment.OrderShipments.FirstOrDefault(x => x.LineId == model.LineId).LotNo = model.LotNo;
                    orderShipment.OrderShipments.FirstOrDefault(x => x.LineId == model.LineId).OrderQuantity = orderDetail.Quantity;
                    orderShipment.OrderShipments.FirstOrDefault(x => x.LineId == model.LineId).SerialNo = model.SerialNo;
                    orderShipment.OrderShipments.FirstOrDefault(x => x.LineId == model.LineId).ShipedQuantity = shiped.Sum(rec => rec.Quantity);
                    orderShipment.OrderShipments.FirstOrDefault(x => x.LineId == model.LineId).ThisShipQuantity = model.ThisShipQuantity;
                }
                else
                {
                    //Never be in this case..
                    orderShipment.OrderShipments.Add(new OrderShipmentLine
                        {
                            BalanceQuantity = orderDetail.Quantity - (shiped.Sum(rec => rec.Quantity) + model.ThisShipQuantity),
                            Id = model.Id,
                            ItemName = model.ItemName,
                            LineId = model.LineId,
                            LocatorId = model.LocatorId,
                            LotNo = model.LotNo,
                            OrderQuantity = orderDetail.Quantity,
                            SerialNo = model.SerialNo,
                            ShipedQuantity = shiped.Sum(rec => rec.Quantity),
                            ThisShipQuantity = model.ThisShipQuantity
                        });
                }
            }
            return "";
        }

        #endregion

        public static List<ShipmentModel> GetShipments()
        {
            List<ShipmentModel> shipments = service.GetAllShipments(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId).Select(x => new ShipmentModel(x, true)).ToList();
            return shipments;
        }

        public static OrderShipmentModel GetShipmentEdit(string orderId, DateTime date)
        {
            List<ShipmentModel> shipmentDetail = service.GetAllByOrderId(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, Convert.ToInt64(orderId), date).Select(x => new ShipmentModel(x)).ToList();
            OrderModel order = OrderHelper.GetOrder(orderId);

            OrderShipmentModel orderShipment = new OrderShipmentModel(shipmentDetail.FirstOrDefault());
            orderShipment.CreateBy = shipmentDetail.FirstOrDefault().CreateBy;
            orderShipment.CreateDate = shipmentDetail.FirstOrDefault().CreateDate;
            orderShipment.CustomerId = order.CustomerId;
            orderShipment.CustomerSiteId = order.CustomerSiteId;
            orderShipment.UpdateBy = shipmentDetail.FirstOrDefault().UpdateBy;
            orderShipment.UpdateDate = shipmentDetail.FirstOrDefault().UpdateDate;

            orderShipment.OrderShipments = new List<OrderShipmentLine>();
            foreach (var item in shipmentDetail)
            {
                string itemName = "";
                OrderDetailModel orderDetail = OrderHelper.GetSingleOrderDetail(item.LineId);
                if (orderDetail != null)
                    itemName = ItemHelper.GetItem(orderDetail.ItemId.ToString()).ItemName;

                OrderShipmentLine shipmentLine = new OrderShipmentLine(item, itemName);
                decimal shippedQty = GetShipments(item.LineId).Where(rec => rec.Id < item.Id).Sum(x => x.Quantity);
                shipmentLine.BalanceQuantity = orderDetail.Quantity - (shippedQty + item.Quantity);
                shipmentLine.OrderQuantity = orderDetail.Quantity;
                shipmentLine.ShipedQuantity = shippedQty;
                shipmentLine.ThisShipQuantity = item.Quantity;

                orderShipment.OrderShipments.Add(shipmentLine);
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
            OrderModel order = OrderHelper.GetOrders(customerId, customerSiteId).FirstOrDefault(rec => rec.Id == orderId);
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

            return newShipment == null ? new OrderShipmentModel() : newShipment;
        }

        public static string Insert(OrderShipmentLine model)
        {
            return validateShipmentQuantity(model);
        }

        //Assuming that no record can be entered without its order..
        public static string Update(OrderShipmentLine model)
        {
            return validateShipmentQuantity(model);
        }

        //Assuming no record can be entered without its order and order detail is unique in every line..
        //Assuming no record can be deleted if the form is in edit..
        public static string Delete(OrderShipmentLine model)
        {
            if (model.Id > 0)
                return "Can not remove the shipped items";
            OrderShipmentModel orderShipment = SessionHelper.Shipment;
            OrderShipmentLine shipment = orderShipment.OrderShipments.FirstOrDefault(x => x.LineId == model.LineId);
            SessionHelper.Shipment.OrderShipments.Remove(shipment);

            return "";
        }

        //Assuming no record can be entered except the order and no Shipment row can be removed on edit..
        public static string Save(OrderShipmentModel model)
        {
            string result = "";
            List<Shipment> currentShipments = getEntitiesByModel(model);
            if (model.OrderShipments.Any(rec => rec.LocatorId == 0 || rec.LotNo == "" || rec.SerialNo == ""))
            {
                return "Invalid Shipment, please select required fields!";
            }
            if (currentShipments != null && currentShipments.Count() > 0)
            {
                foreach (var item in currentShipments)
                {
                    List<ShipmentModel> shipments = GetShipments(item.LineId);
                    OrderDetailModel orderDetail = OrderHelper.GetSingleOrderDetail(item.LineId);
                    decimal savedQty = 0;
                    if (shipments != null && shipments.Count() > 0)
                        savedQty = shipments.Sum(x => x.Quantity);

                    if (item.Id > 0)
                        result = service.Update(item);
                    else
                        result = service.Insert(item);
                }
                List<OrderDetailModel> orderDetailQty = OrderHelper.GetOrderDetail(model.OrderId.ToString());
                string status = "";
                foreach (var orderDetailItem in orderDetailQty)
                {
                    List<ShipmentModel> ShippedQty = GetShipments(orderDetailItem.Id);
                    if (ShippedQty != null && ShippedQty.Count() > 0)
                    {
                        if (ShippedQty.Sum(x => x.Quantity) < orderDetailItem.Quantity)
                        {
                            status = "Partially Shipped";
                            break;
                        }
                        else
                            status = "Shipped";
                    }
                    else
                    {
                        status = "Partially Shipped";
                        break;
                    }
                }
                OrderModel updatedOrder = OrderHelper.GetOrder(model.OrderId.ToString());
                updatedOrder.Status = status;
                result = OrderHelper.UpdateOrder(updatedOrder);
            }
            else
                result = "Please select order to ship!";
            return result;
        }

        public static void Delete(long orderId, DateTime date)
        {
            service.DeleteByOrderId(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, orderId, date);
        }
    }
}
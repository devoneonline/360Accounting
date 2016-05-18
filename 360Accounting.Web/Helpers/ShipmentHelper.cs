using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
                entity.OrderId = item.OrderId;
                entity.UpdateBy = model.UpdateBy;
                entity.UpdateDate = model.UpdateDate;
                entity.Id = item.Id;
                entity.LineId = item.LineId;
                entity.LocatorId = item.LocatorId;
                entity.LotNoId = item.LotNoId == 0 ? null : item.LotNoId;
                entity.Quantity = item.ThisShipQuantity;
                entity.SerialNo = item.SerialNo;
                entity.DeliveryNo = model.DeliveryNo;

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
                ItemName = ItemHelper.GetItem(model.ItemId.ToString()).ItemName,
                ItemId = model.ItemId,
                OrderId = model.OrderId
            };
        }

        private static string validateShipment(OrderShipmentLine model)
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

            string lotAndSerialValidation = checkLotandSerials(model);
            if (!string.IsNullOrEmpty(lotAndSerialValidation))
                return lotAndSerialValidation;

            //Other validations if any..

            OrderShipmentModel orderShipment = SessionHelper.Shipment;
            if (model.Id > 0)
            {
                orderShipment.OrderShipments.First(x => x.LineId == model.LineId).BalanceQuantity = orderDetail.Quantity - (shiped.Sum(rec => rec.Quantity) + model.ThisShipQuantity);
                orderShipment.OrderShipments.First(x => x.LineId == model.LineId).Id = model.Id;
                orderShipment.OrderShipments.First(x => x.LineId == model.LineId).LocatorId = model.LocatorId;
                orderShipment.OrderShipments.First(x => x.LineId == model.LineId).LotNoId = model.LotNoId;
                orderShipment.OrderShipments.First(x => x.LineId == model.LineId).OrderQuantity = orderDetail.Quantity;
                orderShipment.OrderShipments.First(x => x.LineId == model.LineId).SerialNo = model.SerialNo;
                orderShipment.OrderShipments.First(x => x.LineId == model.LineId).ShipedQuantity = shiped.Sum(rec => rec.Quantity);
                orderShipment.OrderShipments.First(x => x.LineId == model.LineId).ThisShipQuantity = model.ThisShipQuantity;
            }
            else
            {
                if (orderShipment.OrderShipments.Any(x => x.LineId == model.LineId))
                {
                    orderShipment.OrderShipments.First(x => x.LineId == model.LineId).BalanceQuantity = orderDetail.Quantity - (shiped.Sum(rec => rec.Quantity) + model.ThisShipQuantity);
                    orderShipment.OrderShipments.First(x => x.LineId == model.LineId).Id = model.Id;
                    orderShipment.OrderShipments.First(x => x.LineId == model.LineId).LocatorId = model.LocatorId;
                    orderShipment.OrderShipments.First(x => x.LineId == model.LineId).LotNoId = model.LotNoId;
                    orderShipment.OrderShipments.First(x => x.LineId == model.LineId).OrderQuantity = orderDetail.Quantity;
                    orderShipment.OrderShipments.First(x => x.LineId == model.LineId).SerialNo = model.SerialNo;
                    orderShipment.OrderShipments.First(x => x.LineId == model.LineId).ShipedQuantity = shiped.Sum(rec => rec.Quantity);
                    orderShipment.OrderShipments.First(x => x.LineId == model.LineId).ThisShipQuantity = model.ThisShipQuantity;
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
                            LotNoId = model.LotNoId,
                            OrderQuantity = orderDetail.Quantity,
                            SerialNo = model.SerialNo,
                            ShipedQuantity = shiped.Sum(rec => rec.Quantity),
                            ThisShipQuantity = model.ThisShipQuantity
                        });
                }
            }
            return "";
        }

        private static void updateOrders(List<long> tobeUpdated)
        {
            tobeUpdated = tobeUpdated.Distinct().ToList();
            foreach (var orderId in tobeUpdated)
            {
                List<OrderDetailModel> orderDetailQty = OrderHelper.GetOrderDetail(orderId.ToString());
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
                        status = "Booked";
                        break;
                    }
                }
                OrderModel updatedOrder = OrderHelper.GetOrder(orderId.ToString());
                updatedOrder.Status = status;
                string result = OrderHelper.UpdateOrder(updatedOrder);
            }
        }

        private static string checkLotandSerials(OrderShipmentLine model)
        {
            //Selection check..
            if (model.LotNoId == null || model.LotNoId == 0)
            {
                ItemModel item = ItemHelper.GetItem(model.ItemId.ToString());
                if (item.LotControl)
                    return "Please provide item lot!";

                if (!string.IsNullOrEmpty(model.SerialNo))
                    return "Serial no. can not be defined without its lot!";
                else
                    return "";
            }
            else
            {
                List<SerialNumber> serialExist = LotNumberHelper.GetSerialsbyLotNo(model.LotNoId.Value).ToList();
                List<SerialNumber> availableSerials = LotNumberHelper.GetAvailableSerials(model.LotNoId.Value);
                List<string> serialonGrid = SessionHelper.Shipment.OrderShipments.Where(rec => rec.LotNoId == model.LotNoId && rec.LineId != model.LineId).Select(x => x.SerialNo).ToList();

                if (!string.IsNullOrEmpty(model.SerialNo))
                {
                    List<string> serials = model.SerialNo.Split(new char[] { ',' }).ToList();

                    //Check duplication within the same record..
                    if (serials.GroupBy(rec => rec).Any(d => d.Count() > 1))
                        return "Serial can not be duplicate!";

                    //Quantity check..
                    if (serials.Count() < model.ThisShipQuantity || serials.Count() > model.ThisShipQuantity)
                        return "Serials are not matching with the quantity!";

                    //Serial availablity within shipment..
                    if (serialonGrid != null && serialonGrid.Count() > 0)
                    {
                        foreach (var unsaved in serialonGrid)
                        {
                            if (!string.IsNullOrEmpty(unsaved))
                            {
                                //Check by another entries on grid..
                                List<string> unsavedSerial = unsaved.Split(new char[] { ',' }).ToList();
                                if (serials.Any(rec => rec == unsavedSerial.FirstOrDefault(x => x == rec)))
                                    return "One of your serial no is already defined!";
                            }
                        }
                    }

                    //Lot serial existence check..
                    if (serialExist != null && serialExist.Count() > 0)
                    {
                        if (serials.Count() > availableSerials.Count())
                            return "Lot is exceeding the available qty!";
                    }
                    else
                        return "This lot does not support serial.";

                    //Serial availablity within db..
                    foreach (var serial in serials)
                    {
                        bool isAvailable = LotNumberHelper.CheckSerialNumAvailability(model.LotNoId.Value, serial);
                        if (isAvailable)
                            continue;
                        else
                        {
                            if (model.Id > 0)
                            {
                                LotNumber savedLot = LotNumberHelper.GetLotNumber(LotNumberHelper.GetSerialNo(serial, model.LotNoId.Value).LotNoId);
                                if (savedLot.SourceId == model.Id)
                                    continue;
                                else
                                    return "Serial No. " + serial + " does not exist or may have been already shipped";
                            }
                            else
                                return "Serial No. " + serial + " does not exist or may have been already shipped";
                        }
                    }
                }
                else
                {
                    //Lot serial existence check..
                    if (serialExist != null && serialExist.Count() > 0)
                    {
                        //Serial Generation
                        if (model.ThisShipQuantity > availableSerials.Count() - serialonGrid.Count())
                            return "Quantity is exceeding!";
                        else
                        {
                            //Generate from available serials..
                            //Qty can not be decimal..
                            List<SerialNumber> useAvailable = new List<SerialNumber>();
                            if (serialonGrid != null && serialonGrid.Count() > 0)
                            {
                                List<string> gridSerial = new List<string>();
                                foreach (var serial in serialonGrid)
                                {
                                    List<string> currentLine = serial.Split(new char[] { ',' }).ToList();
                                    gridSerial.AddRange(currentLine);
                                }

                                useAvailable = availableSerials.Where(rec => rec.SerialNo != gridSerial.FirstOrDefault(x => x == rec.SerialNo)).Take(Convert.ToInt32(model.ThisShipQuantity)).ToList();
                            }
                            else
                            {
                                useAvailable = availableSerials.Take(Convert.ToInt32(model.ThisShipQuantity)).ToList();
                            }

                            List<string> autoGeneratedSerial = useAvailable.Select(rec => rec.SerialNo).ToList();
                            string newSerial = string.Join(",", autoGeneratedSerial.ToArray());

                            SessionHelper.Shipment.OrderShipments.FirstOrDefault(x => x.LineId == model.LineId && x.Id == model.Id).SerialNo = newSerial;
                        }
                    }
                    else
                    {
                        //Lot can be used only one time..
                        if (SessionHelper.Shipment.OrderShipments.Any(re => re.LotNoId == model.LotNoId && re.LineId != model.LineId))
                            return "Lot is in use in the current shipment";
                    }
                }
            }

            return "";
        }

        private static void deleteSerials(List<string> serials, long lotNoId)
        {
            foreach (var serial in serials)
            {
                SerialNumber savedSerial = LotNumberHelper.GetSerialNo(serial, lotNoId);
                LotNumberHelper.DeleteSerialNumber(savedSerial.Id.ToString());
            }
        }

        private static void updateSerials(Shipment model)
        {
            List<string> modelSerials = string.IsNullOrEmpty(model.SerialNo) ? new List<string>() : model.SerialNo.Split(new char[] { ',' }).ToList();
            if (model.Id > 0)
            {
                Shipment savedShipment = service.GetSingle(model.Id.ToString(), AuthenticationHelper.CompanyId.Value);
                if (!string.IsNullOrEmpty(savedShipment.SerialNo))
                {
                    List<string> savedSerials = model.SerialNo.Split(new char[] { ',' }).ToList();
                    if (savedSerials.Count() > modelSerials.Count())
                    {
                        savedSerials = savedSerials.Take(savedSerials.Count() - modelSerials.Count()).ToList();
                        deleteSerials(savedSerials, model.LotNoId.Value);
                    }
                }
                foreach (var updateSerial in modelSerials)
                {
                    SerialNumber savedSerial = LotNumberHelper.GetSerialNo(updateSerial, model.LotNoId.Value);
                    if (savedSerial != null)
                    {
                        savedSerial.UpdateDate = DateTime.Now;
                        savedSerial.UpdateBy = AuthenticationHelper.UserId;
                        savedSerial.SerialNo = updateSerial;
                        LotNumberHelper.UpdateSerialNumber(savedSerial);
                    }
                    else
                        LotNumberHelper.SaveSerial(new SerialNumber
                        {
                            CompanyId = AuthenticationHelper.CompanyId.Value,
                            SerialNo = updateSerial,
                            UpdateDate = null,
                            UpdateBy = null,
                            LotNoId = model.LotNoId.Value,
                            LotNo = "",
                            CreateBy = AuthenticationHelper.UserId,
                            CreateDate = DateTime.Now
                        });
                }
            }
            else
            {
                if (modelSerials.Count() > 0)
                {
                    foreach (var serial in modelSerials)
                    {
                        LotNumberHelper.SaveSerial(new SerialNumber
                        {
                            CompanyId = AuthenticationHelper.CompanyId.Value,
                            CreateBy = AuthenticationHelper.UserId,
                            CreateDate = DateTime.Now,
                            LotNo = "",
                            LotNoId = model.LotNoId.Value,
                            SerialNo = serial,
                            UpdateBy = null,
                            UpdateDate = null
                        });
                    }
                }
            }
        }

        private static void deleteLots(List<long> lotNoIds)
        {
            foreach (var lotNoId in lotNoIds)
            {
                LotNumberHelper.Delete(lotNoId.ToString());
            }
        }

        private static string updateLots(Shipment model)
        {
            if (model.Id > 0)
            {
                if (model.LotNoId != null && model.LotNoId > 0)
                {
                    LotNumber savedLot = LotNumberHelper.GetLotNumber(model.LotNoId.Value);
                    if (savedLot != null && savedLot.SourceId == model.Id)
                        //Lot is already saved..
                        return "";
                    else
                    {
                        savedLot = LotNumberHelper.GetLotBySourceId(model.Id);
                        if (savedLot != null)
                        {
                            LotNumberHelper.Delete(savedLot.Id.ToString());
                        }

                        savedLot.SourceId = savedLot.Id;
                        savedLot.SourceType = "Shipment";
                        return LotNumberHelper.SaveLot(savedLot);
                    }
                }
            }
            else
            {
                if (model.LotNoId != null && model.LotNoId > 0)
                {
                    LotNumber newLot = LotNumberHelper.GetLotNumber(model.LotNoId.Value);
                    newLot.SourceId = 0; //Temporary..
                    newLot.SourceType = "Shipment";
                    return LotNumberHelper.SaveLot(newLot);
                }
            }

            return "";
        }

        #endregion

        public static string GenerateDeliveryNum(OrderShipmentModel model)
        {
            var currentDocument = service.GetAll(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId).OrderByDescending(rec => rec.Id).FirstOrDefault();
            string newDocNo = "";
            if (currentDocument != null)
            {
                int outVal;
                bool isNumeric = int.TryParse(currentDocument.DeliveryNo, out outVal);
                if (isNumeric && currentDocument.DeliveryNo.Length == 8)
                {
                    newDocNo = (int.Parse(currentDocument.DeliveryNo) + 1).ToString();
                    return newDocNo;
                }
            }

            //Create New DocNum..
            string yearDigit = model.DeliveryDate.ToString("yy");
            string monthDigit = model.DeliveryDate.ToString("MM");
            string docNo = int.Parse("1").ToString().PadLeft(4, '0');

            return yearDigit + monthDigit + docNo;
        }

        public static List<ShipmentModel> GetShipments()
        {
            List<ShipmentModel> shipments = service.GetAllShipments(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId).Select(x => new ShipmentModel(x, true)).ToList();
            return shipments;
        }

        public static OrderShipmentModel GetShipmentEdit(string deliveryNo, DateTime date)
        {
            List<ShipmentModel> shipmentDetail = service.GetDelivery(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, deliveryNo, date).Select(x => new ShipmentModel(x)).ToList();
            OrderModel order = OrderHelper.GetOrder(shipmentDetail.First().OrderId.ToString());

            //Can be multiple, showing the first one on the header..
            OrderShipmentModel orderShipment = new OrderShipmentModel(shipmentDetail.First());
            orderShipment.CreateBy = shipmentDetail.First().CreateBy;
            orderShipment.CreateDate = shipmentDetail.First().CreateDate;
            orderShipment.CustomerId = order.CustomerId;
            orderShipment.CustomerSiteId = order.CustomerSiteId;
            orderShipment.UpdateBy = shipmentDetail.First().UpdateBy;
            orderShipment.UpdateDate = shipmentDetail.First().UpdateDate;

            orderShipment.OrderShipments = new List<OrderShipmentLine>();
            foreach (var item in shipmentDetail)
            {
                OrderDetailModel orderDetail = OrderHelper.GetSingleOrderDetail(item.LineId);

                OrderShipmentLine shipmentLine = new OrderShipmentLine(item);
                decimal shippedQty = GetShipments(item.LineId).Where(rec => rec.Id < item.Id).Sum(x => x.Quantity);
                shipmentLine.BalanceQuantity = orderDetail.Quantity - (shippedQty + item.Quantity);
                shipmentLine.OrderQuantity = orderDetail.Quantity;
                shipmentLine.ShipedQuantity = shippedQty;
                shipmentLine.ThisShipQuantity = item.Quantity;

                shipmentLine.ItemName = ItemHelper.GetItem(orderDetail.ItemId.ToString()).ItemName;
                shipmentLine.CustomerId = OrderHelper.GetSingleOrder(item.OrderId.ToString()).CustomerId;
                shipmentLine.CustomerName = OrderHelper.GetSingleOrder(item.OrderId.ToString()).CustomerName;
                shipmentLine.CustomerSiteId = OrderHelper.GetSingleOrder(item.OrderId.ToString()).CustomerSiteId;
                shipmentLine.CustomerSiteName = OrderHelper.GetSingleOrder(item.OrderId.ToString()).CustomerSiteName;

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

            List<OrderModel> orders = OrderHelper.GetOrders(orderId, customerId, customerSiteId);
            if (orders.Count() > 0)
            {
                foreach (var order in orders)
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
                                    {
                                        OrderShipmentLine current = fromOrderDetailtoShipment(detailItem, shipments);
                                        current.CustomerName = order.CustomerName;
                                        current.CustomerSiteName = order.CustomerSiteName;
                                        current.OrderNo = order.OrderNo;

                                        if (!SessionHelper.Shipment.OrderShipments.Any(rec => rec.LineId == detailItem.Id))
                                            newShipment.OrderShipments.Add(current);
                                    }
                                }
                                else
                                {
                                    OrderShipmentLine current = fromOrderDetailtoShipment(detailItem, shipments);
                                    current.CustomerName = order.CustomerName;
                                    current.CustomerSiteName = order.CustomerSiteName;
                                    current.OrderNo = order.OrderNo;

                                    newShipment.OrderShipments.Add(current);
                                }
                            }
                        }
                    }
                }
            }

            return newShipment;
        }

        public static string Insert(OrderShipmentLine model)
        {
            return validateShipment(model);
        }

        public static string Update(OrderShipmentLine model)
        {
            return validateShipment(model);
        }

        public static string Delete(OrderShipmentLine model)
        {
            OrderShipmentModel orderShipment = SessionHelper.Shipment;
            OrderShipmentLine shipment = orderShipment.OrderShipments.FirstOrDefault(x => x.LineId == model.LineId);
            SessionHelper.Shipment.OrderShipments.Remove(shipment);

            return "";
        }

        public static string Save(OrderShipmentModel model)
        {
            List<long> orderIds = new List<long>();
            string result = "";
            if (model.OrderShipments != null && model.OrderShipments.Count() > 0)
                model.OrderShipments = model.OrderShipments.Where(rec => rec.LocatorId > 0).ToList();

            List<Shipment> currentShipments = getEntitiesByModel(model);

            if (currentShipments != null && currentShipments.Count() > 0)
            {
                if (model.DeliveryNo != "New")
                {
                    List<Shipment> savedShipment = service.GetDelivery(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, model.DeliveryNo, model.DeliveryDate).ToList();
                    if (savedShipment != null && savedShipment.Count() > 0)
                    {
                        if (savedShipment.Count() > currentShipments.Count())
                        {
                            List<Shipment> tobeDeleted = savedShipment.Take(savedShipment.Count() - currentShipments.Count()).ToList();
                            foreach (var deleteSingle in tobeDeleted)
                            {
                                if (!string.IsNullOrEmpty(deleteSingle.SerialNo))
                                {
                                    List<string> serials = deleteSingle.SerialNo.Split(new char[] { ',' }).ToList();
                                    deleteSerials(serials, deleteSingle.LotNoId.Value);
                                }

                                orderIds.Add(deleteSingle.OrderId);
                                service.Delete(deleteSingle.Id.ToString(), AuthenticationHelper.CompanyId.Value);
                            }
                        }
                    }
                }
                foreach (var item in currentShipments)
                {
                    List<ShipmentModel> shipments = GetShipments(item.LineId);
                    OrderDetailModel orderDetail = OrderHelper.GetSingleOrderDetail(item.LineId);
                    decimal savedQty = 0;
                    if (shipments != null && shipments.Count() > 0)
                        savedQty = shipments.Sum(x => x.Quantity);

                    orderIds.Add(item.OrderId);

                    if (item.LotNoId != null && item.LotNoId > 0)
                    {
                        long lotNoId = 0;
                        string lotNumResult = updateLots(item);
                        int outVal;
                        bool isNumeric = int.TryParse(lotNumResult, out outVal);
                        if (isNumeric)
                            lotNoId = int.Parse(lotNumResult);

                        item.LotNoId = lotNoId;
                    }

                    updateSerials(item);

                    if (item.Id > 0)
                        result = service.Update(item);
                    else
                        result = service.Insert(item);

                    //Update lot num to set the sourceId..
                    if (item.LotNoId != null && item.LotNoId > 0)
                    {
                        LotNumber tobeUpdated = LotNumberHelper.GetLotNumber(item.LotNoId.Value);
                        tobeUpdated.SourceId = item.Id;
                        LotNumberHelper.Update(tobeUpdated);
                    }
                }

                updateOrders(orderIds);

            }
            else
                result = "Please select order to ship!";
            return result;
        }

        public static void Delete(string deliveryNo, DateTime date)
        {
            List<Shipment> shipments = service.GetDelivery(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, deliveryNo, date).ToList();
            List<long> orderIds = shipments.Select(rec => rec.OrderId).ToList();

            List<Shipment> lotsShipments = shipments.Where(rec => rec.LotNoId > 0 && !string.IsNullOrEmpty(rec.SerialNo)).ToList();

            List<long> lotstobeDeleted = shipments.Where(rec => rec.LotNoId > 0).Select(cri => cri.LotNoId.Value).ToList();
            deleteLots(lotstobeDeleted);

            if (lotsShipments != null && lotsShipments.Count() > 0)
            {
                foreach (var lotNoShipment in lotsShipments)
                {
                    List<string> serials = lotNoShipment.SerialNo.Split(new char[] { ',' }).ToList();
                    deleteSerials(serials, lotNoShipment.LotNoId.Value);
                }
            }

            service.DeleteDelivery(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, deliveryNo, date);

            updateOrders(orderIds);
        }
    }
}
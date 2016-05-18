using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web
{
    public static class OrderHelper
    {
        private static IOrderService service;

        static OrderHelper()
        {
            service = IoC.Resolve<IOrderService>("OrderService");
        }

        #region Private Methods

        private static Order getEntityByModel(OrderModel model)
        {
            if (model == null) return null;

            Order entity = new Order();

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
                entity.CompanyId = model.CompanyId;
            }

            entity.CustomerId = model.CustomerId;
            entity.CustomerSiteId = model.CustomerSiteId;
            entity.Id = model.Id;
            entity.OrderDate = model.OrderDate;
            entity.OrderNo = model.OrderNo;
            entity.OrderTypeId = model.OrderTypeId;
            entity.Remarks = model.Remarks;
            entity.SOBId = SessionHelper.SOBId;
            entity.Status = model.Status;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;

            return entity;
        }

        private static OrderDetail getEntityByModel(OrderDetailModel model)
        {
            if (model == null) return null;

            OrderDetail entity = new OrderDetail();

            if (model.Id == 0)
            {
                entity.CreateBy = AuthenticationHelper.UserId;
                entity.CreateDate = DateTime.Now;
            }
            else
            {
                entity.CreateBy = model.CreateBy;
                entity.CreateDate = model.CreateDate;
            }

            entity.Amount = model.Amount;
            entity.Id = model.Id;
            entity.ItemId = model.ItemId;
            entity.OrderId = model.OrderId;
            entity.Quantity = model.Quantity;
            entity.Rate = model.Rate;
            entity.TaxId = model.TaxId;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            entity.WarehouseId = model.WarehouseId;

            return entity;
        }

        private static List<OrderDetailModel> getDetail()
        {
            return SessionHelper.Order.OrderDetail.ToList();
        }

        private static List<OrderDetailModel> getDetailByOrderId(long orderId)
        {
            IList<OrderDetailModel> modelList = service.GetAllOrderDetail(orderId).Select(x => new OrderDetailModel(x)).ToList();
            return modelList.ToList();
        }

        #endregion

        public static List<OrderModel> GetOrders()
        {
            return service.GetAllOrders(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId).Select(x => new OrderModel(x, true)).ToList();
        }

        public static List<SelectListItem> GetOrdersCombo()
        {
            return service.GetAll(SessionHelper.SOBId).Where(rec => rec.Status != "Shipped").Select(x => new SelectListItem { Text = x.OrderNo, Value = x.Id.ToString() }).ToList();
        }

        public static OrderModel GetOrder(string id)
        {
            return new OrderModel(service.GetSingle(id, AuthenticationHelper.CompanyId.Value));
        }

        public static List<OrderDetailModel> GetOrderDetail([Optional]string orderId)
        {
            if (orderId == null)
                return getDetail();
            else
                return getDetailByOrderId(Convert.ToInt64(orderId));
        }

        public static OrderDetailModel GetSingleOrderDetail(long id)
        {
            return new OrderDetailModel(service.GetSingleOrderDetail(id));
        }

        public static string GenerateOrderNum(OrderModel model)
        {
            var currentDocument = service.GetAll(SessionHelper.SOBId).OrderByDescending(rec => rec.Id).FirstOrDefault();
            string newDocNo = "";
            if (currentDocument != null)
            {
                int outVal;
                bool isNumeric = int.TryParse(currentDocument.OrderNo, out outVal);
                if (isNumeric && currentDocument.OrderNo.Length == 8)
                {
                    newDocNo = (int.Parse(currentDocument.OrderNo) + 1).ToString();
                    return newDocNo;
                }
            }

            //Create New DocNum..
            string yearDigit = model.OrderDate.ToString("yy");
            string monthDigit = model.OrderDate.ToString("MM");
            string docNo = int.Parse("1").ToString().PadLeft(4, '0');

            return yearDigit + monthDigit + docNo;
        }

        public static string UpdateOrder(OrderModel model)
        {
            Order entity = getEntityByModel(model);
            return service.Update(entity);
        }

        public static void Save(OrderModel model)
        {
            Order entity = getEntityByModel(model);

            string result = string.Empty;
            if (entity.IsValid())
            {
                if (model.Id > 0)
                    result = service.Update(entity);
                else
                    result = service.Insert(entity);

                if (!string.IsNullOrEmpty(result))
                {
                    var savedLines = getDetailByOrderId(Convert.ToInt64(result));
                    if (savedLines.Count() > model.OrderDetail.Count())
                    {
                        var tobeDeleted = savedLines.Take(savedLines.Count() - model.OrderDetail.Count());
                        foreach (var item in tobeDeleted)
                        {
                            service.DeleteOrderDetail(item.Id);
                        }
                        savedLines = getDetailByOrderId(Convert.ToInt64(result));
                    }

                    foreach (var detail in model.OrderDetail)
                    {
                        OrderDetail detailEntity = getEntityByModel(detail);
                        if (detailEntity.IsValid())
                        {
                            detailEntity.OrderId = Convert.ToInt64(result);
                            if (savedLines.Count() > 0)
                            {
                                detailEntity.Id = savedLines.FirstOrDefault().Id;
                                savedLines.Remove(savedLines.FirstOrDefault(rec => rec.Id == detailEntity.Id));
                                service.Update(detailEntity);
                            }
                            else
                                service.Insert(detailEntity);
                        }
                    }
                }
            }
        }

        public static void Delete(string id)
        {
            service.Delete(id, AuthenticationHelper.CompanyId.Value);
        }

        public static void Update(OrderDetailModel model)
        {
            OrderModel order = SessionHelper.Order;
            order.OrderDetail.FirstOrDefault(x => x.Id == model.Id).Amount = model.Amount;
            order.OrderDetail.FirstOrDefault(x => x.Id == model.Id).CreateBy = model.CreateBy;
            order.OrderDetail.FirstOrDefault(x => x.Id == model.Id).CreateDate = model.CreateDate;
            order.OrderDetail.FirstOrDefault(x => x.Id == model.Id).Id = model.Id;
            order.OrderDetail.FirstOrDefault(x => x.Id == model.Id).ItemId = model.ItemId;
            order.OrderDetail.FirstOrDefault(x => x.Id == model.Id).OrderId = model.OrderId;
            order.OrderDetail.FirstOrDefault(x => x.Id == model.Id).Quantity = model.Quantity;
            order.OrderDetail.FirstOrDefault(x => x.Id == model.Id).Rate = model.Rate;
            order.OrderDetail.FirstOrDefault(x => x.Id == model.Id).TaxId = model.TaxId;
            order.OrderDetail.FirstOrDefault(x => x.Id == model.Id).UpdateBy = model.UpdateBy;
            order.OrderDetail.FirstOrDefault(x => x.Id == model.Id).UpdateDate = model.UpdateDate;
            order.OrderDetail.FirstOrDefault(x => x.Id == model.Id).WarehouseId = model.WarehouseId;
        }

        public static void Delete(OrderDetailModel model)
        {
            OrderModel order = SessionHelper.Order;
            OrderDetailModel orderDetail = order.OrderDetail.FirstOrDefault(x => x.Id == model.Id);
            SessionHelper.Order.OrderDetail.Remove(orderDetail);
        }

        public static void Insert(OrderDetailModel model)
        {
            OrderModel order = SessionHelper.Order;
            order.OrderDetail.Add(model);
        }

        public static List<OrderModel> GetOrders(long customerId, long customerSiteId)
        {
            List<OrderModel> orders = service.GetAll(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, customerId, customerSiteId).Select(x => new OrderModel(x)).ToList();
            return orders;
        }

        public static List<OrderModel> GetOrders(long orderId, long customerId, long customerSiteId)
        {
            List<OrderModel> result = new List<OrderModel>();
            if (customerId > 0)
            {
                result = service.GetAll(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, customerId).Select(x => new OrderModel(x, true)).ToList();
                if (customerSiteId > 0)
                    result = result.Where(rec => rec.CustomerSiteId == customerSiteId).ToList();
            }

            if (orderId > 0)
            {
                if (result.Count() > 0)
                    result = result.Where(rec => rec.Id == orderId).ToList();
                else if(customerId == 0)
                    result.Add(new OrderModel(service.GetSingleOrder(orderId.ToString(), AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId), true));
            }

            return result;
        }

        public static OrderModel GetSingleOrder(string orderId)
        {
            return new OrderModel(service.GetSingleOrder(orderId, AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId), true);
        }
    }
}
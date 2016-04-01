using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Helpers
{
    public static class ItemHelper
    {
        private static IItemService service;

        static ItemHelper() 
        {
            service = IoC.Resolve<IItemService>("ItemService");
        }
        
        #region Private Methods
        private static Item getEntityByModel(ItemModel model)
        {
            if (model == null)
                return null;

            Item entity = new Item();

            if (model.Id == 0)
            {
                entity.CompanyId = AuthenticationHelper.CompanyId.Value;
                entity.CreateBy = AuthenticationHelper.UserId;
                entity.CreateDate = DateTime.Now;
            }
            else
            {
                entity.CompanyId = model.CompanyId;
                entity.CreateBy = model.CreateBy;
                entity.CreateDate = model.CreateDate;
            }

            entity.COGSCodeCombinationId = model.COGSCodeCombinationId;
            entity.DefaultBuyer = model.DefaultBuyer;
            entity.Description = model.Description;
            entity.Id = model.Id;
            entity.ItemCode = model.ItemCode;
            entity.ItemName = model.ItemName;
            entity.LotControl = model.LotControl;
            entity.Orderable = model.Orderable;
            entity.Purchaseable = model.Purchaseable;
            entity.ReceiptRouting = model.ReceiptRouting;
            entity.SalesCodeCombinationId = model.SalesCodeCombinationId;
            entity.SerialControl = model.SerialControl;
            entity.Shipable = model.Shipable;
            entity.SOBId = model.SOBId;
            entity.Status = model.Status;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            return entity;
        }

        private static ItemWarehouse getEntityByModel(ItemWarehouseModel model, int count)
        {
            if (model == null)
                return null;

            ItemWarehouse entity = new ItemWarehouse();

            if (count == 0)
            {
                entity.CreateBy = AuthenticationHelper.UserId;
                entity.CreateDate = DateTime.Now;
            }
            else
            {
                entity.CreateBy = model.CreateBy;
                entity.CreateDate = model.CreateDate;
            }

            entity.EndDate = model.EndDate;
            entity.Id = model.Id;
            entity.ItemId = model.ItemId;
            entity.SOBId = model.SOBId;
            entity.StartDate = model.StartDate;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            entity.WarehouseId = model.WarehouseId;
            return entity;
        }

        private static IList<ItemWarehouseModel> getItemWarehousesByItemId(string itemId)
        {
            IList<ItemWarehouseModel> modelList = service
                .GetAllItemWarehouses(Convert.ToInt32(itemId))
                .Select(x => new ItemWarehouseModel(x)).ToList();
            return modelList;
        }

        private static IList<ItemWarehouseModel> getItemWarehouses()
        {
            return SessionHelper.Item.ItemWarehouses;
        }
        #endregion

        public static IList<ItemModel> GetItems(long sobId)
        {
            IList<ItemModel> modelList = service
                .GetAll(AuthenticationHelper.User.CompanyId, sobId)
                .Select(x => new ItemModel(x)).ToList();
            return modelList;
        }

        public static ItemModel GetItem(string id)
        {
            ItemModel model = new ItemModel(service.GetSingle(id, AuthenticationHelper.User.CompanyId));
            return model;
        }

        public static List<SelectListItem> GetItemList(long sobId)
        {
            List<SelectListItem> modelList = ItemHelper.GetItems(sobId)
                .Select(x => new SelectListItem { Text = x.ItemName, Value = x.Id.ToString() }).ToList();
            return modelList;
        }

        public static IList<ItemWarehouseModel> GetItemWarehouses([Optional]string itemId)
        {
            if (itemId == null)
                return getItemWarehouses();
            else
                return getItemWarehousesByItemId(itemId);
        }

        public static void InsertItemDetail(ItemWarehouseModel model)
        {
            ItemModel item = SessionHelper.Item;
            item.ItemWarehouses.Add(model);
        }

        public static void UpdateItemDetail(ItemWarehouseModel model)
        {
            ItemModel item = SessionHelper.Item;

            item.ItemWarehouses.FirstOrDefault(x => x.Id == model.Id).EndDate = model.EndDate;
            item.ItemWarehouses.FirstOrDefault(x => x.Id == model.Id).StartDate = model.StartDate;
            item.ItemWarehouses.FirstOrDefault(x => x.Id == model.Id).WarehouseId = model.WarehouseId;
        }

        public static void DeleteItemDetail(ItemWarehouseModel model)
        {
            ItemModel item = SessionHelper.Item;
            ItemWarehouseModel itemWarehouse = item.ItemWarehouses.FirstOrDefault(x => x.Id == model.Id);
            item.ItemWarehouses.Remove(itemWarehouse);
        }

        public static void Save(ItemModel itemModel)
        {
            Item entity = getEntityByModel(itemModel);

            string result = string.Empty;
            if (entity.IsValid())
            {
                if (itemModel.Id > 0)
                    result = service.Update(entity);
                else
                    result = service.Insert(entity);

                if (!string.IsNullOrEmpty(result))
                {
                    var savedDetail = GetItemWarehouses(result);
                    if (savedDetail.Count() > itemModel.ItemWarehouses.Count())
                    {
                        var tobeDeleted = savedDetail.Take(savedDetail.Count() - itemModel.ItemWarehouses.Count());
                        foreach (var item in tobeDeleted)
                        {
                            service.DeleteItemWarehouse(item.Id);
                        }
                        savedDetail = GetItemWarehouses(result);
                    }

                    foreach (var detail in itemModel.ItemWarehouses)
                    {
                        ItemWarehouse detailEntity = getEntityByModel(detail, savedDetail.Count());
                        if (detailEntity.IsValid())
                        {
                            detailEntity.ItemId = Convert.ToInt64(result);
                            if (savedDetail.Count() > 0)
                            {
                                detailEntity.Id = savedDetail.FirstOrDefault().Id;
                                savedDetail.Remove(savedDetail.FirstOrDefault(rec => rec.Id == detailEntity.Id));
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
            service.Delete(id, AuthenticationHelper.User.CompanyId);
        }
    }
}
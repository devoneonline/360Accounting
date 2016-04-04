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
    public static class LocatorHelper
    {
        private static ILocatorService service;

        static LocatorHelper()
        {
            service = IoC.Resolve<ILocatorService>("LocatorService");
        }

        #region Private Methods
        private static LocatorWarehouse getEntityByModel(LocatorWarehouseModel model, int count)
        {
            if (model == null) return null;

            LocatorWarehouse entity = new LocatorWarehouse();

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
            entity.LocatorId = model.LocatorId;
            entity.SOBId = model.SOBId;
            entity.StartDate = model.StartDate;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            entity.WarehouseId = model.WarehouseId;
            return entity;
        }

        private static Locator getEntityByModel(LocatorModel model)
        {
            if (model == null) return null;

            Locator entity = new Locator();
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

            entity.Description = model.Description;
            entity.Id = model.Id;
            entity.SOBId = model.SOBId;
            entity.Status = model.Status;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            return entity;
        }

        private static IList<LocatorWarehouseModel> getLocatorWarehousesByLocatorId(string locatorId)
        {
            IList<LocatorWarehouseModel> modelList = service.GetAllLocatorWarehouses(Convert.ToInt32(locatorId))
                .Select(x => new LocatorWarehouseModel(x)).ToList();
            return modelList;
        }

        private static IList<LocatorWarehouseModel> getLocatorWarehouses()
        {
            return SessionHelper.Locator.LocatorWarehouses;
        }
        #endregion

        public static IList<LocatorModel> GetLocatorsCombo(long sobId)
        {
            IList<LocatorModel> modelList = service.GetAll(AuthenticationHelper.User.CompanyId, sobId)
                .Select(x => new LocatorModel(x)).ToList();
            return modelList;
        }

        public static IList<SelectListItem> GetLocators(long sobId)
        {
            IList<SelectListItem> modelList = service.GetAll(AuthenticationHelper.User.CompanyId, sobId).Where(x => x.Status == "Active")
                .Select(x => new SelectListItem { Text = x.Id.ToString(), Value = x.Id.ToString() }).ToList();
            return modelList;
        }

        public static LocatorModel GetLocator(string id)
        {
            LocatorModel model = new LocatorModel(service.GetSingle(id, AuthenticationHelper.User.CompanyId));
            return model;
        }

        public static IList<LocatorWarehouseModel> GetLocatorWarehouses([Optional]string locatorId)
        {
            if (locatorId == null)
                return getLocatorWarehouses();
            else
                return getLocatorWarehousesByLocatorId(locatorId);
        }

        public static void InsertLocatorWarehouse(LocatorWarehouseModel model)
        {
            LocatorModel item = SessionHelper.Locator;
            item.LocatorWarehouses.Add(model);
        }

        public static void UpdateLocatorWarehouse(LocatorWarehouseModel model)
        {
            LocatorModel item = SessionHelper.Locator;

            item.LocatorWarehouses.FirstOrDefault(x => x.Id == model.Id).EndDate = model.EndDate;
            item.LocatorWarehouses.FirstOrDefault(x => x.Id == model.Id).Id = model.Id;
            item.LocatorWarehouses.FirstOrDefault(x => x.Id == model.Id).LocatorId = model.LocatorId;
            item.LocatorWarehouses.FirstOrDefault(x => x.Id == model.Id).SOBId = model.SOBId;
            item.LocatorWarehouses.FirstOrDefault(x => x.Id == model.Id).StartDate = model.StartDate;
        }

        public static void DeleteLocatorWarehouse(LocatorWarehouseModel model)
        {
            LocatorModel item = SessionHelper.Locator;
            LocatorWarehouseModel locatorWarehouse = item.LocatorWarehouses.FirstOrDefault(x => x.Id == model.Id);
            item.LocatorWarehouses.Remove(locatorWarehouse);
        }

        public static void Save(LocatorModel locatorModel)
        {
            Locator entity = getEntityByModel(locatorModel);

            string result = string.Empty;
            if (entity.IsValid())
            {
                if (locatorModel.Id > 0)
                    result = service.Update(entity);
                else
                    result = service.Insert(entity);

                if (!string.IsNullOrEmpty(result))
                {
                    var savedDetail = GetLocatorWarehouses(result);
                    if (savedDetail.Count() > locatorModel.LocatorWarehouses.Count())
                    {
                        var tobeDeleted = savedDetail.Take(savedDetail.Count() - locatorModel.LocatorWarehouses.Count());
                        foreach (var item in tobeDeleted)
                        {
                            service.DeleteLocatorWarehouse(item.Id);
                        }
                        savedDetail = GetLocatorWarehouses(result);
                    }

                    foreach (var detail in locatorModel.LocatorWarehouses)
                    {
                        LocatorWarehouse detailEntity = getEntityByModel(detail, 0);
                        if (detailEntity.IsValid())
                        {
                            detailEntity.LocatorId = Convert.ToInt64(result);
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
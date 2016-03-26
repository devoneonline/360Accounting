using _360Accounting.Core;
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
        
        #region Private Methods
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

        public static void Insert(ItemWarehouseModel model)
        {
            ItemModel item = SessionHelper.Item;
            item.ItemWarehouses.Add(model);
        }

        public static void UpdateItemDetail(ItemWarehouseModel model)
        {
            ItemModel item = SessionHelper.Item;

            item.ItemWarehouses.FirstOrDefault(x => x.Id == model.Id).EndDate = model.EndDate;
            item.ItemWarehouses.FirstOrDefault(x => x.Id == model.Id).StartDate = model.StartDate;
            item.ItemWarehouses.FirstOrDefault(x => x.Id == model.Id).WarehouseCode = model.WarehouseCode;
        }

        public static void DeleteInvoiceDetail(ItemWarehouseModel model)
        {
            ItemModel item = SessionHelper.Item;
            ItemWarehouseModel itemWarehouse = item.ItemWarehouses.FirstOrDefault(x => x.Id == model.Id);
            item.ItemWarehouses.Remove(itemWarehouse);
        }

        //public static string GetInvoiceNo(long companyId, long sobId, long periodId, long currencyId)
        //{
        //    ///TODO: plz audit this code
        //    var previousInvoice = service.GetSingle(companyId, sobId, periodId, currencyId);
        //    string newInvoiceNo = "";
        //    if (previousInvoice != null)
        //    {
        //        int outVal;
        //        bool isNumeric = int.TryParse(previousInvoice.InvoiceNo, out outVal);
        //        if (isNumeric && previousInvoice.InvoiceNo.Length == 8)
        //        {
        //            newInvoiceNo = (int.Parse(previousInvoice.InvoiceNo) + 1).ToString();
        //            return newInvoiceNo;
        //        }
        //    }

        //    //Create New Invoice #...
        //    string yearDigit = SessionHelper.Invoice.InvoiceDate.ToString("yy");
        //    string monthDigit = SessionHelper.Invoice.InvoiceDate.ToString("MM");
        //    string invoiceNo = int.Parse("1").ToString().PadLeft(4, '0');

        //    return yearDigit + monthDigit + invoiceNo;
        //}

        //public static void Update(ItemModel invoiceModel)
        //{
        //    Invoice entity = Mappers.GetEntityByModel(invoiceModel);

        //    string result = string.Empty;
        //    if (entity.IsValid())
        //    {
        //        if (invoiceModel.Id > 0)
        //            result = service.Update(entity);
        //        else
        //            result = service.Insert(entity);

        //        if (!string.IsNullOrEmpty(result))
        //        {
        //            var savedDetail = getInvoiceDetailByInvoiceId(result);
        //            if (savedDetail.Count() > invoiceModel.InvoiceDetail.Count())
        //            {
        //                var tobeDeleted = savedDetail.Take(savedDetail.Count() - invoiceModel.InvoiceDetail.Count());
        //                foreach (var item in tobeDeleted)
        //                {
        //                    detailService.Delete(item.Id.ToString(), AuthenticationHelper.User.CompanyId);
        //                }
        //                savedDetail = getInvoiceDetailByInvoiceId(result);
        //            }

        //            foreach (var detail in invoiceModel.InvoiceDetail)
        //            {
        //                InvoiceDetail detailEntity = Mappers.GetEntityByModel(detail);
        //                if (detailEntity.IsValid())
        //                {
        //                    detailEntity.InvoiceId = Convert.ToInt64(result);
        //                    if (savedDetail.Count() > 0)
        //                    {
        //                        detailEntity.Id = savedDetail.FirstOrDefault().Id;
        //                        savedDetail.Remove(savedDetail.FirstOrDefault(rec => rec.Id == detailEntity.Id));
        //                        detailService.Update(detailEntity);
        //                    }
        //                    else
        //                        detailService.Insert(detailEntity);
        //                }
        //            }
        //        }
        //    }
        //}

        //public static ItemModel GetInvoice(string id)
        //{
        //    return new InvoiceModel(service.GetSingle
        //        (id, AuthenticationHelper.User.CompanyId));
        //}

        //public static void Delete(string id)
        //{
        //    service.Delete(id, AuthenticationHelper.User.CompanyId);
        //}
    }
}
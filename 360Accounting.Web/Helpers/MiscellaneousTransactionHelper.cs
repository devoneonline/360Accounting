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
    public static class MiscellaneousTransactionHelper
    {
        private static IMiscellaneousTransactionService service;

        static MiscellaneousTransactionHelper()
        {
            service = IoC.Resolve<IMiscellaneousTransactionService>("MiscellaneousTransactionService");
        }

        #region Private Methods

        private static MiscellaneousTransaction GetEntityByModel(MiscellaneousTransactionDetailModel model, int count)
        {
            if (model == null) return null;

            MiscellaneousTransaction entity = new MiscellaneousTransaction();

            if (count == 0)
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

            entity.CodeCombinationId = model.CodeCombinationId;
            entity.Cost = model.Cost;
            entity.Id = model.Id;
            entity.ItemId = model.ItemId;
            entity.LocatorId = model.LocatorId;
            entity.LotNo = model.LotNo;
            entity.Quantity = model.Quantity;
            entity.SerialNo = model.SerialNo;
            entity.SOBId = model.SOBId;
            entity.TransactionDate = model.TransactionDate;
            entity.TransactionType = model.TransactionType;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            entity.WarehouseId = model.WarehouseId;
            return entity;
        }

        private static IList<MiscellaneousTransactionDetailModel> getMiscellaneousTransaction()
        {
            return SessionHelper.MiscellaneousTransaction.MiscellaneousTransactionDetail;
        }

        private static IList<MiscellaneousTransactionDetailModel> getMiscellaneousTransaction(long sobId, string type, long codeCombinationId)
        {
            IList<MiscellaneousTransactionDetailModel> modelList = service
                .GetAll(AuthenticationHelper.User.CompanyId, sobId, type, codeCombinationId)
                .Select(x => new MiscellaneousTransactionDetailModel(x)).ToList();
            return modelList;
        }

        #endregion

        public static IList<MiscellaneousTransactionModel> GetMiscellaneousTransactions(long sobId)
        {
            IList<MiscellaneousTransactionModel> modelList = service
                .GetAll(AuthenticationHelper.User.CompanyId, sobId)
                .Select(x => new MiscellaneousTransactionModel(x)).ToList();
            return modelList;
        }

        public static MiscellaneousTransactionModel GetMiscellaneousTransaction(string id)
        {
            MiscellaneousTransactionModel model = new MiscellaneousTransactionModel(service.GetSingle(id, AuthenticationHelper.User.CompanyId));
            return model;
        }

        public static List<SelectListItem> GetMiscellaneousTransactionList(long sobId)
        {
            List<SelectListItem> modelList = GetMiscellaneousTransactions(sobId)
                .Select(x => new SelectListItem { Text = x.Id.ToString(), Value = x.Id.ToString() }).ToList();
            return modelList;
        }

        public static IList<MiscellaneousTransactionDetailModel> GetMiscellaneousTransactionDetail(long sobId, string type, long codeCombinationId)
        {
            if (SessionHelper.MiscellaneousTransaction != null)
                return getMiscellaneousTransaction();
            else
            {
                IList<MiscellaneousTransactionDetailModel> modelList = service
                .GetAll(AuthenticationHelper.User.CompanyId, sobId, type, codeCombinationId)
                .Select(x => new MiscellaneousTransactionDetailModel(x)).ToList();
                return modelList;
            }
        }

        public static void InsertMiscellaneousTransactionDetail(MiscellaneousTransactionDetailModel model)
        {
            MiscellaneousTransactionModel moveOrder = SessionHelper.MiscellaneousTransaction;
            moveOrder.MiscellaneousTransactionDetail.Add(model);
        }

        public static void UpdateMiscellaneousTransactionDetail(MiscellaneousTransactionDetailModel model)
        {
            MiscellaneousTransactionModel miscellaneousTransaction = SessionHelper.MiscellaneousTransaction;

            miscellaneousTransaction.MiscellaneousTransactionDetail.FirstOrDefault(x => x.Id == model.Id).CodeCombinationId = model.CodeCombinationId;
            miscellaneousTransaction.MiscellaneousTransactionDetail.FirstOrDefault(x => x.Id == model.Id).CompanyId = model.CompanyId;
            miscellaneousTransaction.MiscellaneousTransactionDetail.FirstOrDefault(x => x.Id == model.Id).Cost = model.Cost;
            miscellaneousTransaction.MiscellaneousTransactionDetail.FirstOrDefault(x => x.Id == model.Id).Id = model.Id;
            miscellaneousTransaction.MiscellaneousTransactionDetail.FirstOrDefault(x => x.Id == model.Id).ItemId = model.ItemId;
            miscellaneousTransaction.MiscellaneousTransactionDetail.FirstOrDefault(x => x.Id == model.Id).LocatorId = model.LocatorId;
            miscellaneousTransaction.MiscellaneousTransactionDetail.FirstOrDefault(x => x.Id == model.Id).LotNo = model.LotNo;
            miscellaneousTransaction.MiscellaneousTransactionDetail.FirstOrDefault(x => x.Id == model.Id).Quantity = model.Quantity;
            miscellaneousTransaction.MiscellaneousTransactionDetail.FirstOrDefault(x => x.Id == model.Id).SerialNo = model.SerialNo;
            miscellaneousTransaction.MiscellaneousTransactionDetail.FirstOrDefault(x => x.Id == model.Id).SOBId = model.SOBId;
            miscellaneousTransaction.MiscellaneousTransactionDetail.FirstOrDefault(x => x.Id == model.Id).TransactionDate = model.TransactionDate;
            miscellaneousTransaction.MiscellaneousTransactionDetail.FirstOrDefault(x => x.Id == model.Id).TransactionType = model.TransactionType;
            miscellaneousTransaction.MiscellaneousTransactionDetail.FirstOrDefault(x => x.Id == model.Id).WarehouseId = model.WarehouseId;
        }

        public static void DeleteMiscellaneousTransactionDetail(MiscellaneousTransactionDetailModel model)
        {
            MiscellaneousTransactionModel miscellaneousTransaction = SessionHelper.MiscellaneousTransaction;
            MiscellaneousTransactionDetailModel miscellaneousTransactionDetail = miscellaneousTransaction.MiscellaneousTransactionDetail.FirstOrDefault(x => x.Id == model.Id);
            miscellaneousTransaction.MiscellaneousTransactionDetail.Remove(miscellaneousTransactionDetail);
        }

        public static void Save(MiscellaneousTransactionModel miscellaneousTransactionModel)
        {
            var savedDetail = getMiscellaneousTransaction(miscellaneousTransactionModel.SOBId, miscellaneousTransactionModel.TransactionType, miscellaneousTransactionModel.CodeCombinationId);
            if (SessionHelper.MiscellaneousTransaction != null)
            {
                if (savedDetail.Count() > miscellaneousTransactionModel.MiscellaneousTransactionDetail.Count())
                {
                    var tobeDeleted = savedDetail.Take(savedDetail.Count() - miscellaneousTransactionModel.MiscellaneousTransactionDetail.Count());
                    foreach (var item in tobeDeleted)
                    {
                        service.Delete(item.Id.ToString(), AuthenticationHelper.User.CompanyId);
                    }
                    savedDetail = getMiscellaneousTransaction(miscellaneousTransactionModel.SOBId, miscellaneousTransactionModel.TransactionType, miscellaneousTransactionModel.CodeCombinationId);
                }

                foreach (var detail in miscellaneousTransactionModel.MiscellaneousTransactionDetail)
                {
                    MiscellaneousTransaction detailEntity = GetEntityByModel(detail, savedDetail.Count());
                    if (detailEntity.IsValid())
                    {
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

        public static void Delete(string id)
        {
            service.Delete(id, AuthenticationHelper.User.CompanyId);
        }
    }
}
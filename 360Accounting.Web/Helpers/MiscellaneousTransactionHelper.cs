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
        private static ILotNumberService lotNumService;

        static MiscellaneousTransactionHelper()
        {
            service = IoC.Resolve<IMiscellaneousTransactionService>("MiscellaneousTransactionService");
            lotNumService = IoC.Resolve<ILotNumberService>("LotNumberService");
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

        private static IList<MiscellaneousTransactionDetailModel> getMiscellaneousTransaction(long sobId, string type, long codeCombinationId, DateTime transDate)
        {
            IList<MiscellaneousTransactionDetailModel> modelList = service
                .GetAll(AuthenticationHelper.CompanyId.Value, sobId, type, codeCombinationId, transDate)
                .Select(x => new MiscellaneousTransactionDetailModel(x)).ToList();
            return modelList;
        }

        #endregion

        public static IList<MiscellaneousTransactionModel> GetMiscellaneousTransactions(long sobId)
        {
            IList<MiscellaneousTransactionModel> modelList = service
                .GetAll(AuthenticationHelper.CompanyId.Value, sobId)
                .Select(x => new MiscellaneousTransactionModel(x)).ToList();
            return modelList;
        }

        public static MiscellaneousTransactionModel GetMiscellaneousTransaction(string id)
        {
            MiscellaneousTransactionModel model = new MiscellaneousTransactionModel(service.GetSingle(id, AuthenticationHelper.CompanyId.Value));
            return model;
        }

        public static List<SelectListItem> GetMiscellaneousTransactionList(long sobId)
        {
            List<SelectListItem> modelList = GetMiscellaneousTransactions(sobId)
                .Select(x => new SelectListItem { Text = x.Id.ToString(), Value = x.Id.ToString() }).ToList();
            return modelList;
        }

        public static IList<MiscellaneousTransactionDetailModel> GetMiscellaneousTransactionDetail(long sobId, string type, long codeCombinationId, DateTime transDate)
        {
            if (SessionHelper.MiscellaneousTransaction != null)
            {
                if (SessionHelper.MiscellaneousTransaction.MiscellaneousTransactionDetail != null && SessionHelper.MiscellaneousTransaction.MiscellaneousTransactionDetail.Count() > 0)
                    return getMiscellaneousTransaction().Where(rec => rec.SOBId == sobId && rec.TransactionType == type && rec.CodeCombinationId == codeCombinationId && rec.TransactionDate == transDate).ToList();
                if (SessionHelper.MiscellaneousTransaction.Id > 0)
                {
                    IList<MiscellaneousTransactionDetailModel> modelList = service
                    .GetAll(AuthenticationHelper.CompanyId.Value, sobId, type, codeCombinationId, transDate)
                    .Select(x => new MiscellaneousTransactionDetailModel(x)).ToList();
                    return modelList;
                }
            }

            return new List<MiscellaneousTransactionDetailModel>();
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
            var savedDetail = getMiscellaneousTransaction(miscellaneousTransactionModel.SOBId, miscellaneousTransactionModel.TransactionType, miscellaneousTransactionModel.CodeCombinationId, miscellaneousTransactionModel.TransactionDate);
            if (SessionHelper.MiscellaneousTransaction != null)
            {
                if (savedDetail.Count() > miscellaneousTransactionModel.MiscellaneousTransactionDetail.Count())
                {
                    var tobeDeleted = savedDetail.Take(savedDetail.Count() - miscellaneousTransactionModel.MiscellaneousTransactionDetail.Count());
                    foreach (var item in tobeDeleted)
                    {
                        service.Delete(item.Id.ToString(), AuthenticationHelper.CompanyId.Value);
                        IEnumerable<LotNumber> lotNum = lotNumService.CheckLotNumAvailability(AuthenticationHelper.CompanyId.Value, item.LotNo, item.ItemId, SessionHelper.SOBId);
                        if (lotNum.Any())
                        {
                            LotNumberHelper.Delete(lotNum.FirstOrDefault().Id.ToString());
                        }

                        //IEnumerable<SerialNumber> serialNum = lotNumService.CheckSerialNumAvailability(AuthenticationHelper.CompanyId.Value, item.LotNo, item.SerialNo);
                        //if (serialNum.Any())
                        //{
                        //    LotNumberHelper.DeleteSerialNumber(serialNum.FirstOrDefault().Id.ToString());
                        //}
                    }
                    savedDetail = getMiscellaneousTransaction(miscellaneousTransactionModel.SOBId, miscellaneousTransactionModel.TransactionType, miscellaneousTransactionModel.CodeCombinationId, miscellaneousTransactionModel.TransactionDate);
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
                            IEnumerable<LotNumber> lotNum = lotNumService.CheckLotNumAvailability(AuthenticationHelper.CompanyId.Value, detailEntity.LotNo, detailEntity.ItemId, SessionHelper.SOBId);
                            if (lotNum.Any())
                            {
                                LotNumberHelper.Update(lotNum.FirstOrDefault());
                            }

                            //IEnumerable<SerialNumber> serialNum = lotNumService.CheckSerialNumAvailability(AuthenticationHelper.CompanyId.Value, detailEntity.LotNo, detailEntity.SerialNo);
                            //if (serialNum.Any())
                            //{
                            //    LotNumberHelper.UpdateSerialNumber(serialNum.FirstOrDefault());
                            //}
                        }
                        else
                        {
                            service.Insert(detailEntity);
                            IEnumerable<LotNumber> lotNum = lotNumService.CheckLotNumAvailability(AuthenticationHelper.CompanyId.Value, detailEntity.LotNo, detailEntity.ItemId, SessionHelper.SOBId);
                            if (!lotNum.Any())
                            {
                                LotNumberHelper.Insert(new MiscellaneousTransactionDetailModel(detailEntity));
                            }

                            //IEnumerable<SerialNumber> serialNum = lotNumService.CheckSerialNumAvailability(AuthenticationHelper.CompanyId.Value, detailEntity.LotNo, detailEntity.SerialNo);
                            //if (!serialNum.Any())
                            //{
                            //    LotNumberHelper.InsertSerialNumber(new MiscellaneousTransactionDetailModel(detailEntity));
                            //}
                        }
                    }
                }
            }
        }

        public static void Delete(string id)
        {
            service.Delete(id, AuthenticationHelper.CompanyId.Value);
        }
    }
}
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
    public class MoveOrderHelper
    {
        private static IMoveOrderService service;
        private static ILotNumberService lotNumService;

        static MoveOrderHelper()
        {
            service = IoC.Resolve<IMoveOrderService>("MoveOrderService");
            lotNumService = IoC.Resolve<ILotNumberService>("LotNumberService");
        }

        #region Private Methods

        private static MoveOrder GetEntityByModel(MoveOrderModel model)
        {
            if (model == null) return null;

            MoveOrder entity = new MoveOrder();
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

            entity.DateRequired = model.DateRequired;
            entity.Description = model.Description;
            entity.Id = model.Id;
            entity.MoveOrderDate = model.MoveOrderDate;
            entity.MoveOrderNo = model.MoveOrderNo;
            entity.SOBId = model.SOBId;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            return entity;
        }

        private static MoveOrderDetail GetEntityByModel(MoveOrderDetailModel model, int count)
        {
            if (model == null) return null;

            MoveOrderDetail entity = new MoveOrderDetail();
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

            entity.DateRequired = model.DateRequired;
            entity.Id = model.Id;
            entity.ItemId = model.ItemId;
            entity.LocatorId = model.LocatorId;
            entity.LotNo = model.LotNo;
            entity.MoveOrderId = model.MoveOrderId;
            entity.Quantity = model.Quantity;
            entity.SerialNo = model.SerialNo;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            entity.WarehouseId = model.WarehouseId;
            return entity;
        }

        private static IList<MoveOrderDetailModel> getMoveOrderDetailById(string moveOrderId)
        {
            IList<MoveOrderDetailModel> modelList = service
                .GetAllMoveOrderDetail(Convert.ToInt32(moveOrderId))
                .Select(x => new MoveOrderDetailModel(x)).ToList();
            return modelList;
        }

        private static IList<MoveOrderDetailModel> getMoveOrderDetail()
        {
            return SessionHelper.MoveOrder.MoveOrderDetail;
        }

        #endregion

        public static IList<MoveOrderModel> GetMoveOrders(long sobId)
        {
            IList<MoveOrderModel> modelList = service
                .GetAll(AuthenticationHelper.CompanyId.Value, sobId)
                .Select(x => new MoveOrderModel(x)).ToList();
            return modelList;
        }

        public static MoveOrderModel GetMoveOrder(string id)
        {
            MoveOrderModel model = new MoveOrderModel(service.GetSingle(id, AuthenticationHelper.CompanyId.Value));
            return model;
        }

        public static List<SelectListItem> GetMoveOrderList(long sobId)
        {
            List<SelectListItem> modelList = GetMoveOrders(sobId)
                .Select(x => new SelectListItem { Text = x.MoveOrderNo, Value = x.Id.ToString() }).ToList();
            return modelList;
        }

        public static IList<MoveOrderDetailModel> GetMoveOrderLines([Optional]string moveOrderId)
        {
            if (moveOrderId == null)
                return getMoveOrderDetail();
            else
                return getMoveOrderDetailById(moveOrderId);
        }

        public static void InsertMoveOrderDetail(MoveOrderDetailModel model)
        {
            MoveOrderModel moveOrder = SessionHelper.MoveOrder;
            moveOrder.MoveOrderDetail.Add(model);
        }

        public static void UpdateMoveOrderDetail(MoveOrderDetailModel model)
        {
            MoveOrderModel moveOrder = SessionHelper.MoveOrder;

            moveOrder.MoveOrderDetail.FirstOrDefault(x => x.Id == model.Id).DateRequired = model.DateRequired;
            moveOrder.MoveOrderDetail.FirstOrDefault(x => x.Id == model.Id).Id = model.Id;
            moveOrder.MoveOrderDetail.FirstOrDefault(x => x.Id == model.Id).ItemId = model.ItemId;
            moveOrder.MoveOrderDetail.FirstOrDefault(x => x.Id == model.Id).LocatorId = model.LocatorId;
            moveOrder.MoveOrderDetail.FirstOrDefault(x => x.Id == model.Id).LotNo = model.LotNo;
            moveOrder.MoveOrderDetail.FirstOrDefault(x => x.Id == model.Id).MoveOrderId = model.MoveOrderId;
            moveOrder.MoveOrderDetail.FirstOrDefault(x => x.Id == model.Id).Quantity = model.Quantity;
            moveOrder.MoveOrderDetail.FirstOrDefault(x => x.Id == model.Id).SerialNo = model.SerialNo;
            moveOrder.MoveOrderDetail.FirstOrDefault(x => x.Id == model.Id).WarehouseId = model.WarehouseId;
        }

        public static void DeleteMoveOrderDetail(MoveOrderDetailModel model)
        {
            MoveOrderModel moveOrder = SessionHelper.MoveOrder;
            MoveOrderDetailModel moveOrderDetail = moveOrder.MoveOrderDetail.FirstOrDefault(x => x.Id == model.Id);
            moveOrder.MoveOrderDetail.Remove(moveOrderDetail);
        }

        public static string GetDocNo(long companyId, long sobId)
        {
            var currentDocument = service.GetAll(companyId, sobId).OrderByDescending(x => x.Id).FirstOrDefault();
            string newDocNo = "";
            if (currentDocument != null)
            {
                int outVal;
                bool isNumeric = int.TryParse(currentDocument.MoveOrderNo, out outVal);
                if (isNumeric && currentDocument.MoveOrderNo.Length == 8)
                {
                    newDocNo = (int.Parse(currentDocument.MoveOrderNo) + 1).ToString();
                    return newDocNo;
                }
            }

            //Create New DocNum..
            string yearDigit = SessionHelper.MoveOrder.MoveOrderDate.ToString("yy");
            string monthDigit = SessionHelper.MoveOrder.MoveOrderDate.ToString("MM");
            string docNo = int.Parse("1").ToString().PadLeft(4, '0');

            return yearDigit + monthDigit + docNo;
        }

        public static void Save(MoveOrderModel moveOrderModel)
        {
            MoveOrder entity = GetEntityByModel(moveOrderModel);

            string result = string.Empty;
            if (entity.IsValid())
            {
                if (moveOrderModel.Id > 0)
                    result = service.Update(entity);
                else
                    result = service.Insert(entity);

                if (!string.IsNullOrEmpty(result))
                {
                    var savedDetail = GetMoveOrderLines(result);
                    if (savedDetail.Count() > moveOrderModel.MoveOrderDetail.Count())
                    {
                        var tobeDeleted = savedDetail.Take(savedDetail.Count() - moveOrderModel.MoveOrderDetail.Count());
                        foreach (var item in tobeDeleted)
                        {
                            service.Delete(item.Id);
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
                        savedDetail = GetMoveOrderLines(result);
                    }

                    foreach (var detail in moveOrderModel.MoveOrderDetail)
                    {
                        MoveOrderDetail detailEntity = GetEntityByModel(detail, savedDetail.Count());
                        if (detailEntity.IsValid())
                        {
                            detailEntity.MoveOrderId = Convert.ToInt64(result);
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
                                    LotNumberHelper.Insert(new MoveOrderDetailModel(detailEntity));
                                }

                                //IEnumerable<SerialNumber> serialNum = lotNumService.CheckSerialNumAvailability(AuthenticationHelper.CompanyId.Value, detailEntity.LotNo, detailEntity.SerialNo);
                                //if (!serialNum.Any())
                                //{
                                //    LotNumberHelper.InsertSerialNumber(new MoveOrderDetailModel(detailEntity));
                                //}
                            }
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
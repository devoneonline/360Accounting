using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Controllers
{
    public class ReceivingController : BaseController
    {
        private IReceivingService service;
        private IPurchaseOrderService poService;
        private IItemService itemService;
        private ILotNumberService lotService;

        public ReceivingController()
        {
            service = IoC.Resolve<IReceivingService>("ReceivingService");
            poService = IoC.Resolve<IPurchaseOrderService>("PurchaseOrderService");
            itemService = IoC.Resolve<IItemService>("ItemService");
            lotService = IoC.Resolve<ILotNumberService>("LotNumberService");
        }

        #region Private Methods

        private Receiving getEntityByModel(ReceivingModel model)
        {
            if (model == null) return null;

            Receiving entity = new Receiving();

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

            entity.Date = model.Date;
            entity.DCNo = model.DCNo;
            entity.Id = model.Id;
            entity.POId = model.POId;
            entity.ReceiptNo = model.ReceiptNo;
            entity.SOBId = model.SOBId;
            entity.UpdateBy = model.UpdateBy;
            entity.UpdateDate = model.UpdateDate;

            return entity;
        }

        private ReceivingDetail getEntityByModel(ReceivingDetailModel model)
        {
            if (model == null) return null;

            ReceivingDetail entity = new ReceivingDetail();

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

            entity.Id = model.Id;
            entity.ItemId = model.ItemId;
            entity.LocatorId = model.LocatorId;
            entity.LotNoId = model.LotNoId;
            entity.Quantity = model.ThisPurchaseQty;
            entity.ReceiptId = model.ReceiptId;
            entity.SerialNo = model.SerialNo;
            entity.UpdateBy = model.UpdateBy;
            entity.UpdateDate = model.UpdateDate;
            entity.WarehouseId = model.WarehouseId;
            entity.PODetailId = model.PODetailId;

            return entity;
        }

        private string generateReceiptNum(ReceivingModel model)
        {
            var currentDocument = service.GetAll(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId).OrderByDescending(rec => rec.Id).FirstOrDefault();
            string newDocNo = "";
            if (currentDocument != null)
            {
                int outVal;
                bool isNumeric = int.TryParse(currentDocument.ReceiptNo, out outVal);
                if (isNumeric && currentDocument.ReceiptNo.Length == 8)
                {
                    newDocNo = (int.Parse(currentDocument.ReceiptNo) + 1).ToString();
                    return newDocNo;
                }
            }

            //Create New DocNum..
            string yearDigit = model.Date.ToString("yy");
            string monthDigit = model.Date.ToString("MM");
            string docNo = int.Parse("1").ToString().PadLeft(4, '0');

            return yearDigit + monthDigit + docNo;
        }

        private List<ReceivingDetailModel> getReceivingDetail([Optional]string receivingId)
        {
            if (receivingId == null)
                return SessionHelper.Receiving.ReceivingDetail.ToList();
            else
            {
                IList<ReceivingDetailModel> modelList = service.GetAllReceivingDetail(Convert.ToInt64(receivingId)).Select(x => new ReceivingDetailModel(x, true)).ToList();
                return modelList.ToList();
            }
        }

        //Pending..
        private string updateLot(ReceivingDetailModel model)
        {
            string lotNoId = null;
            ReceivingDetail savedDetail = service.GetSingleReceivingDetail(model.Id);
            if (!string.IsNullOrEmpty(model.LotNo))
            {
                if (savedDetail.LotNoId > 0)
                {
                    LotNumber lot = lotService.GetSingle(savedDetail.LotNoId.ToString(), AuthenticationHelper.CompanyId.Value);
                    if (lot.LotNo == model.LotNo)
                        lotNoId = lot.Id.ToString();
                    else
                    {
                        lot.LotNo = model.LotNo;
                        List<LotNumber> lots = lotService.GetAllbyLotNo(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, lot.LotNo, lot.ItemId).ToList();
                        if (lots.Any(rec => rec.SourceType == "Shipment"))
                            return "Lot Number can not be edited";
                        else
                            lotService.Update(lot);
                    }
                }
                else
                {
                    lotNoId = lotService.Insert(new LotNumber
                    {
                        CompanyId = AuthenticationHelper.CompanyId.Value,
                        CreateBy = model.CreateBy,
                        CreateDate = DateTime.Now,
                        ItemId = model.ItemId,
                        LotNo = model.LotNo,
                        SOBId = SessionHelper.SOBId,
                        SourceId = model.Id,
                        SourceType = "Receiving"
                    });
                }
            }
            else
            {
                LotNumber lot = lotService.GetSingle(savedDetail.LotNoId.ToString(), AuthenticationHelper.CompanyId.Value);
                if (lot != null)
                {
                    List<LotNumber> lots = lotService.GetAllbyLotNo(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, lot.LotNo, lot.ItemId).ToList();
                    if (lots.Any(rec => rec.SourceType == "Shipment"))
                        return "Lot Number can not be deleted";
                    else
                        lotService.Delete(lot.Id.ToString(), AuthenticationHelper.CompanyId.Value);
                }
            }

            return lotNoId;
        }

        //Pending..
        private string updateSerials(ReceivingDetailModel model)
        {
            string result = "";
            if (model.SerialNo != null)
            {
                List<string> serials = model.SerialNo.Split(new char[] { ',' }).ToList();
                if (model.LotNo == null)
                    return "Serials can not be defined without lot!";

                if (serials.Count() > model.ThisPurchaseQty)
                    return "Serials can not be more than quantity!";

                if (model.LotNoId != null && model.LotNoId.Value > 0)
                {
                    List<SerialNumber> savedSerials = lotService.GetSerialsbyLotNo(model.LotNoId.Value, AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId).ToList();
                    if (savedSerials != null && savedSerials.Count() > 0)
                    {

                    }
                    else
                    {
                        foreach (var serial in serials)
                        {
                            lotService.InsertSerialNum(new SerialNumber
                                {
                                    CompanyId = AuthenticationHelper.CompanyId.Value,
                                    CreateBy = model.CreateBy,
                                    CreateDate = DateTime.Now,
                                    LotNo = model.LotNo,
                                    SerialNo = serial
                                });
                        }
                    }
                }
            }
            return result;
        }

        private void save(ReceivingModel model)
        {
            Receiving entity = getEntityByModel(model);

            string result = string.Empty;
            if (entity.IsValid())
            {
                if (model.Id > 0)
                    result = service.Update(entity);
                else
                    result = service.Insert(entity);

                if (!string.IsNullOrEmpty(result))
                {
                    var savedLines = getReceivingDetail(result);
                    if (savedLines.Count() > model.ReceivingDetail.Count())
                    {
                        var tobeDeleted = savedLines.Take(savedLines.Count() - model.ReceivingDetail.Count());
                        foreach (var item in tobeDeleted)
                        {
                            service.DeleteReceivingDetail(item.Id);
                        }
                        savedLines = getReceivingDetail(result);
                    }

                    foreach (var detail in model.ReceivingDetail)
                    {
                        ReceivingDetail detailEntity = getEntityByModel(detail);
                        if (detailEntity.IsValid())
                        {
                            detailEntity.ReceiptId = Convert.ToInt64(result);
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

        private ReceivingDetailModel fromPODetailtoReceivingDetail(PurchaseOrderDetail poDetail, List<ReceivingDetailView> receivings)
        {
            return new ReceivingDetailModel
            {
                CreateBy = AuthenticationHelper.UserId,
                CreateDate = DateTime.Now,
                ItemId = poDetail.ItemId,
                ItemName = itemService.GetSingle(poDetail.ItemId.ToString(), AuthenticationHelper.CompanyId.Value).ItemName,
                BalanceQty = receivings == null ? poDetail.Quantity : poDetail.Quantity - receivings.Sum(rec => rec.Quantity),
                OrderQty = poDetail.Quantity,
                PurchaseQty = receivings == null ? 0 : receivings.Sum(rec => rec.Quantity),
                ThisPurchaseQty = receivings == null ? poDetail.Quantity : poDetail.Quantity - receivings.Sum(rec => rec.Quantity),
                PODetailId = poDetail.Id,
                Id = -poDetail.Id
            };
        }

        private List<ReceivingDetailModel> getPendingReceivingDetail(long poId)
        {
            List<ReceivingDetailModel> pendingReceivings = new List<ReceivingDetailModel>();

            List<PurchaseOrderDetail> poDetails = poService.GetAllPODetail(poId).ToList();

            List<Receiving> receivings = service.GetAllByPOId(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, poId).ToList();

            foreach (var poDetail in poDetails)
            {
                if (receivings != null && receivings.Count() > 0)
                {
                    foreach (var receiving in receivings)
                    {
                        List<ReceivingDetailView> receivingDetails = service.GetAllReceivingDetail(receiving.Id).ToList();
                        if (receivingDetails != null && receivingDetails.Count() > 0)
                        {
                            List<ReceivingDetailView> receivedItem = receivingDetails.Where(rec => rec.PODetailId == poDetail.Id).ToList();
                            if (receivedItem != null && receivedItem.Count() > 0)
                            {
                                if (receivedItem.Sum(rec => rec.Quantity) < poDetail.Quantity)
                                    pendingReceivings.Add(fromPODetailtoReceivingDetail(poDetail, receivedItem));
                            }
                            else
                                pendingReceivings.Add(fromPODetailtoReceivingDetail(poDetail, null));
                        }
                        else
                            pendingReceivings.Add(fromPODetailtoReceivingDetail(poDetail, null));
                    }
                }
                else
                    pendingReceivings.Add(fromPODetailtoReceivingDetail(poDetail, null));
            }

            return pendingReceivings;
        }

        #endregion

        #region Actions

        public ActionResult Index()
        {
            SessionHelper.Receiving = null;
            return View();
        }

        public ActionResult ListPartial()
        {
            return PartialView("_List", service.GetAll(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId).Select(x => new ReceivingModel(x, true)).ToList());
        }

        public ActionResult Create()
        {
            ReceivingModel receiving = SessionHelper.Receiving;
            if (receiving == null)
            {
                receiving = new ReceivingModel
                {
                    CompanyId = AuthenticationHelper.CompanyId.Value,
                    Date = DateTime.Now,
                    ReceiptNo = "New",
                    SOBId = SessionHelper.SOBId
                };
            }

            receiving.POs = poService.GetAllPO(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId).
                Select(x => new SelectListItem
                {
                    Text = x.PONo,
                    Value = x.Id.ToString()
                }).ToList();

            if (receiving.POs != null && receiving.POs.Count() > 0)
                receiving.POId = receiving.POId > 0 ? receiving.POId : Convert.ToInt64(receiving.POs.FirstOrDefault().Value);

            SessionHelper.Receiving = receiving;
            if (receiving.POId > 0)
                SessionHelper.Receiving.ReceivingDetail = getPendingReceivingDetail(receiving.POId);

            return View("Edit", receiving);
        }

        public ActionResult Edit(string id)
        {
            ReceivingModel receiving = new ReceivingModel(service.GetSingle(id, AuthenticationHelper.CompanyId.Value));
            receiving.ReceivingDetail = service.GetAllReceivingDetail(receiving.Id).Select(x => new ReceivingDetailModel(x, true)).ToList();

            if (receiving.ReceivingDetail != null && receiving.ReceivingDetail.Count() > 0)
            {
                foreach (var detail in receiving.ReceivingDetail)
                {
                    PurchaseOrderDetail currentPODetail = poService.GetSinglePODetail(detail.PODetailId);
                    List<ReceivingDetail> totalReceived = service.GetAllByPODetailId(detail.PODetailId).ToList();

                    detail.BalanceQty = currentPODetail.Quantity - (totalReceived.Sum(rec => rec.Quantity) - detail.ThisPurchaseQty);
                    detail.OrderQty = currentPODetail.Quantity;
                    detail.PurchaseQty = totalReceived.Sum(rec => rec.Quantity) - detail.ThisPurchaseQty;
                    detail.ThisPurchaseQty = detail.ThisPurchaseQty;
                }
            }
            
            SessionHelper.Receiving = receiving;

            receiving.POs = poService.GetAllPO(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId).
                Select(x => new SelectListItem
                {
                    Text = x.PONo,
                    Value = x.Id.ToString()
                }).ToList();

            return View("Edit", receiving);
        }

        public ActionResult Delete(string id)
        {
            service.Delete(id, AuthenticationHelper.CompanyId.Value);
            return RedirectToAction("Index");
        }

        public ActionResult Save(long id, string receiptNo, DateTime date, long poId, string dcNo, long companyId, long sobId)
        {
            if (SessionHelper.Receiving != null)
            {
                if (SessionHelper.Receiving.ReceivingDetail.Count == 0)
                    return Json("No detail information available to save!");

                SessionHelper.Receiving.CompanyId = companyId;
                SessionHelper.Receiving.Date = date;
                SessionHelper.Receiving.DCNo = dcNo;
                SessionHelper.Receiving.Id = id;
                SessionHelper.Receiving.POId = poId;
                SessionHelper.Receiving.ReceiptNo = receiptNo;
                SessionHelper.Receiving.SOBId = sobId;

                if (SessionHelper.Receiving.ReceiptNo == "New")
                    SessionHelper.Receiving.ReceiptNo = generateReceiptNum(SessionHelper.Receiving);

                save(SessionHelper.Receiving);
                SessionHelper.Receiving = null;
                return Json("Saved Successfully");
            }

            return Json("No information available to save!");
        }

        public ActionResult DetailPartial()
        {
            return PartialView("_Detail", getReceivingDetail());
        }

        public ActionResult DetailPartialParams(long poId)
        {
            return PartialView("_Detail", getPendingReceivingDetail(poId));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewPartial(ReceivingDetailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.ThisPurchaseQty > model.BalanceQty)
                    {
                        ViewData["EditError"] = "Quantity can not be more than balance!";
                    }
                    if (!string.IsNullOrEmpty(model.SerialNo))
                    {
                        if (string.IsNullOrEmpty(model.LotNo))
                            ViewData["EditError"] = "Serials can not be defined without Lot!";
                    }
                    else
                    {
                        if (SessionHelper.Receiving != null)
                        {
                            if (SessionHelper.Receiving.ReceivingDetail != null && SessionHelper.Receiving.ReceivingDetail.Count() > 0)
                                model.Id = SessionHelper.Receiving.ReceivingDetail.LastOrDefault().Id + 1;
                            else
                                model.Id = 1;
                        }
                        else
                            model.Id = 1;

                        SessionHelper.Receiving.ReceivingDetail.Add(model);
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_Detail", getReceivingDetail());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdatePartial(ReceivingDetailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.WarehouseId == 0)
                    {
                        ViewData["EditError"] = "Please select Warehouse!";
                        return PartialView("_Detail", getReceivingDetail());
                    }
                    if (model.LocatorId == 0)
                    {
                        ViewData["EditError"] = "Please select Locator!";
                        return PartialView("_Detail", getReceivingDetail());
                    }

                    ReceivingDetailModel completeModel = SessionHelper.Receiving.ReceivingDetail.FirstOrDefault(x => x.Id == model.Id);
                    if (model.ThisPurchaseQty > completeModel.BalanceQty)
                    {
                        ViewData["EditError"] = "Quantity can not be more than balance!";
                        return PartialView("_Detail", getReceivingDetail());
                    }
                    if (!string.IsNullOrEmpty(model.SerialNo))
                    {
                        if (string.IsNullOrEmpty(model.LotNo))
                        {
                            ViewData["EditError"] = "Serials can not be defined without Lot!";
                            return PartialView("_Detail", getReceivingDetail());
                        }
                    }
                    else
                    {
                        SessionHelper.Receiving.ReceivingDetail.FirstOrDefault(rec => rec.Id == model.Id).Id = model.Id;
                        SessionHelper.Receiving.ReceivingDetail.FirstOrDefault(rec => rec.Id == model.Id).LocatorId = model.LocatorId;
                        SessionHelper.Receiving.ReceivingDetail.FirstOrDefault(rec => rec.Id == model.Id).LotNo = model.LotNo;
                        SessionHelper.Receiving.ReceivingDetail.FirstOrDefault(rec => rec.Id == model.Id).LotNoId = model.LotNoId;
                        SessionHelper.Receiving.ReceivingDetail.FirstOrDefault(rec => rec.Id == model.Id).SerialNo = model.SerialNo;
                        SessionHelper.Receiving.ReceivingDetail.FirstOrDefault(rec => rec.Id == model.Id).WarehouseId = model.WarehouseId;
                        SessionHelper.Receiving.ReceivingDetail.FirstOrDefault(rec => rec.Id == model.Id).ThisPurchaseQty = model.ThisPurchaseQty;
                        SessionHelper.Receiving.ReceivingDetail.FirstOrDefault(rec => rec.Id == model.Id).BalanceQty = completeModel.BalanceQty - model.ThisPurchaseQty;
                        SessionHelper.Receiving.ReceivingDetail.FirstOrDefault(rec => rec.Id == model.Id).PurchaseQty = completeModel.PurchaseQty;
                        SessionHelper.Receiving.ReceivingDetail.FirstOrDefault(rec => rec.Id == model.Id).OrderQty = completeModel.OrderQty;
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_Detail", getReceivingDetail());
        }

        public ActionResult DeletePartial(ReceivingDetailModel model)
        {
            try
            {
                ReceivingModel receiving = SessionHelper.Receiving;
                ReceivingDetailModel receivingDetail = receiving.ReceivingDetail.FirstOrDefault(rec => rec.Id == model.Id);
                SessionHelper.Receiving.ReceivingDetail.Remove(receivingDetail);
            }
            catch (Exception ex)
            {
                ViewData["EditError"] = ex.Message;
            }

            return PartialView("_Detail", getReceivingDetail());
        }

        public ActionResult ChangeDate(DateTime date)
        {
            if (date < DateTime.Now.Date)
                return Json("Date cannot be the past date!");

            SessionHelper.Receiving.Date = date;
            return Json("Success");
        }

        #endregion
    }
}
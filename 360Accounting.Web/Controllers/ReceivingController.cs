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

        private string deleteLot(ReceivingDetailModel model)
        {
            LotNumber lot = lotService.GetSingle(model.LotNoId.Value.ToString(), AuthenticationHelper.CompanyId.Value);
            List<LotNumber> savedLots = lotService.GetAllbyLotNo(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, lot.LotNo, lot.ItemId).ToList();
            if (model.LotNoId != null)
            {
                if (savedLots.Count() > 1)
                {
                    return "Record can not be deleted!";
                }
                else
                    lotService.Delete(model.LotNoId.Value.ToString(), AuthenticationHelper.CompanyId.Value);
            }

            return "";
        }

        private string updateLot(ReceivingDetailModel model)
        {
            if (!string.IsNullOrEmpty(model.LotNo))
            {
                if (model.Id > 0)
                {
                    ReceivingDetail savedDetail = service.GetSingleReceivingDetail(model.Id);
                    LotNumber savedLot = lotService.GetSingle(model.LotNoId.ToString(), AuthenticationHelper.CompanyId.Value);
                    if (savedLot.LotNo == model.LotNo)
                    {
                        savedLot.Qty = savedLot.Qty - savedDetail.Quantity + model.ThisPurchaseQty;
                        return lotService.Update(savedLot);
                    }
                    else
                    {
                        List<LotNumber> savedLots = lotService.GetAllbyLotNo(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, savedLot.LotNo, savedLot.ItemId).ToList();
                        if (savedLots.Count() > 1)
                            return "Lot can not be edited!";
                        else
                        {
                            savedLot.Qty = savedLot.Qty - savedDetail.Quantity + model.ThisPurchaseQty;
                            savedLot.LotNo = model.LotNo;
                            return lotService.Update(savedLot);
                        }
                    }
                }
                else
                {
                    return lotService.Insert(new LotNumber
                    {
                        CompanyId = AuthenticationHelper.CompanyId.Value,
                        CreateBy = AuthenticationHelper.UserId,
                        CreateDate = DateTime.Now,
                        ItemId = model.ItemId,
                        LotNo = model.LotNo,
                        Qty = model.ThisPurchaseQty,
                        SOBId = SessionHelper.SOBId,
                        SourceId = 0,
                        SourceType = "Receiving",
                        UpdateBy = null,
                        UpdateDate = null
                    });
                }
            }
            else
            {
                if (model.Id > 0)
                {
                    ReceivingDetail savedDetail = service.GetSingleReceivingDetail(model.Id);
                    LotNumber lot = lotService.GetSingle(savedDetail.LotNoId.Value.ToString(), AuthenticationHelper.CompanyId.Value);
                    List<LotNumber> savedLots = lotService.GetAllbyLotNo(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, lot.LotNo, lot.ItemId).ToList();
                    if (savedDetail.LotNoId != null)
                    {
                        if (savedLots.Count() > 1)
                        {
                            return "Lot can not be deleted!";
                        }
                        else
                            lotService.Delete(savedDetail.LotNoId.Value.ToString(), AuthenticationHelper.CompanyId.Value);
                    }
                    return "";
                }
                return "";
            }
        }

        private string deleteSerials(ReceivingDetailModel model)
        {
            ReceivingDetail savedDetail = service.GetSingleReceivingDetail(model.Id);

            if (!string.IsNullOrEmpty(savedDetail.SerialNo))
            {
                List<string> savedSerials = savedDetail.SerialNo.Split(new char[] { ',' }).ToList();
                bool isAllowed = true;
                List<SerialNumber> tobeDeleted = new List<SerialNumber>();
                foreach (var serial in savedSerials)
                {
                    isAllowed = lotService.CheckSerialNumAvailability(AuthenticationHelper.CompanyId.Value, savedDetail.LotNoId.Value, serial);
                    if (!isAllowed)
                        return "Record can not be deleted!";
                    else
                        tobeDeleted.Add(lotService.GetSerialNo(serial, savedDetail.LotNoId.Value, AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId));
                }
                if (isAllowed)
                {
                    if (tobeDeleted.Count() > 0)
                    {
                        foreach (var item in tobeDeleted)
                        {
                            lotService.DeleteSerialNum(item.Id.ToString(), AuthenticationHelper.CompanyId.Value);
                        }
                    }
                }
            }
            return "";
        }

        private string updateSerials(ReceivingDetailModel model)
        {
            if (!string.IsNullOrEmpty(model.SerialNo))
            {
                List<string> newSerials = model.SerialNo.Trim().Split(new char[] { ',' }).ToList();
                if (newSerials.Count() != model.ThisPurchaseQty)
                    return "Serials must be according to the Quantity";
            }
            if (model.Id > 0)
            {
                ReceivingDetail savedDetail = service.GetSingleReceivingDetail(model.Id);
                if (!string.IsNullOrEmpty(model.SerialNo))
                {
                    List<string> unsavedSerials = model.SerialNo.Trim().Split(new char[] { ',' }).ToList();
                    if (!string.IsNullOrEmpty(savedDetail.SerialNo))
                    {
                        List<string> savedSerials = savedDetail.SerialNo.Split(new char[] { ',' }).ToList();
                        bool isAvailable = true;
                        foreach (var serial in savedSerials)
                        {
                            isAvailable = lotService.CheckSerialNumAvailability(AuthenticationHelper.CompanyId.Value, savedDetail.LotNoId.Value, serial);
                            if (!isAvailable)
                                return "Serial is in use!";
                        }

                        if (isAvailable)
                        {
                            if (savedSerials.Count() > unsavedSerials.Count())
                            {
                                List<string> tobeDeleted = savedSerials.Take(savedSerials.Count() - unsavedSerials.Count()).ToList();
                                if (tobeDeleted != null && tobeDeleted.Count() > 0)
                                {
                                    foreach (var item in tobeDeleted)
                                    {
                                        SerialNumber serialNum = lotService.GetSerialNo(item, savedDetail.LotNoId.Value, AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId);
                                        if (serialNum != null)
                                            lotService.DeleteSerialNum(serialNum.Id.ToString(), AuthenticationHelper.CompanyId.Value);
                                    }
                                }
                            }
                            foreach (var serial in unsavedSerials)
                            {
                                SerialNumber entity = lotService.GetSerialNo(serial, savedDetail.LotNoId.Value, AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId);
                                if (entity != null)
                                {
                                    entity.LotNo = model.LotNo;
                                    entity.SerialNo = serial;
                                    lotService.UpdateSerialNum(entity);
                                }
                                else
                                {
                                    lotService.InsertSerialNum(new SerialNumber
                                    {
                                        CompanyId = AuthenticationHelper.CompanyId.Value,
                                        CreateBy = AuthenticationHelper.UserId,
                                        CreateDate = DateTime.Now,
                                        LotNo = model.LotNo,
                                        LotNoId = savedDetail.LotNoId.Value,
                                        SerialNo = serial,
                                        UpdateBy = null,
                                        UpdateDate = null
                                    });
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (var serial in unsavedSerials)
                        {
                            lotService.InsertSerialNum(new SerialNumber
                            {
                                CompanyId = AuthenticationHelper.CompanyId.Value,
                                CreateBy = AuthenticationHelper.UserId,
                                CreateDate = DateTime.Now,
                                LotNo = model.LotNo,
                                LotNoId = savedDetail.Id,
                                SerialNo = serial,
                                UpdateBy = null,
                                UpdateDate = null
                            });
                        }
                    }
                }
            }
            else 
            {
                if (!string.IsNullOrEmpty(model.SerialNo))
                {
                    if (!string.IsNullOrEmpty(model.LotNo))
                    {
                        LotNumber lot = lotService.GetLotbyItem(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, model.ItemId, model.LotNo);
                        if (lot != null)
                        {
                            List<string> serials = model.SerialNo.Trim().Split(new char[] { ',' }).ToList();
                            foreach (var serial in serials)
                            {
                                bool notAvailable = lotService.CheckSerialNumAvailability(AuthenticationHelper.CompanyId.Value, lot.Id, serial);
                                if (!notAvailable)
                                {
                                    lotService.InsertSerialNum(new SerialNumber
                                    {
                                        CompanyId = AuthenticationHelper.CompanyId.Value,
                                        CreateBy = AuthenticationHelper.UserId,
                                        CreateDate = DateTime.Now,
                                        LotNo = lot.LotNo,
                                        LotNoId = lot.Id,
                                        SerialNo = serial,
                                        UpdateBy = null,
                                        UpdateDate = null
                                    });
                                }
                                else
                                    return "Serial # " + serial + " is already defined";
                            }
                        }
                        else
                            return "Lot not found!";
                    }
                    else
                        return "Serials can not be defined without lot!";
                }
            }

            return "";
        }

        private string save(ReceivingModel model)
        {
            Receiving entity = getEntityByModel(model);

            string result = string.Empty;
            if (entity.IsValid())
            {
                bool goodToSave = false;
                foreach (var item in model.ReceivingDetail)
                {
                    ReceivingDetailModel updatedModel = item;
                    string lotResult = updateLot(updatedModel);
                    int outVal;
                    bool isNumeric = int.TryParse(lotResult, out outVal);
                    if (isNumeric || string.IsNullOrEmpty(lotResult))
                    {
                        string serialResult = updateSerials(updatedModel);
                        if (string.IsNullOrEmpty(serialResult))
                        {
                            item.LotNoId = isNumeric ? (long?)Convert.ToInt64(lotResult) : null;
                            goodToSave = true;
                        }
                        else
                            return serialResult;
                    }
                    else
                        return lotResult;
                }
                if (goodToSave)
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
                                string lotResult = deleteLot(item);
                                if (string.IsNullOrEmpty(lotResult))
                                {
                                    string serialResult = deleteSerials(item);
                                    if (string.IsNullOrEmpty(serialResult))
                                        service.DeleteReceivingDetail(item.Id);
                                }

                                else
                                    return "Record can not be deleted";
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

                                    string receivingDetailId = service.Update(detailEntity);

                                    if (detailEntity.LotNoId != null)
                                    {
                                        LotNumber lottobeUpdated = lotService.GetSingle(detailEntity.LotNoId.ToString(), AuthenticationHelper.CompanyId.Value);
                                        lottobeUpdated.SourceId = Convert.ToInt64(receivingDetailId);
                                        lotService.Update(lottobeUpdated);
                                    }
                                }
                                else
                                {
                                    string receivingDetailId = service.Insert(detailEntity);

                                    if (detailEntity.LotNoId != null)
                                    {
                                        LotNumber lottobeUpdated = lotService.GetSingle(detailEntity.LotNoId.ToString(), AuthenticationHelper.CompanyId.Value);
                                        lottobeUpdated.SourceId = Convert.ToInt64(receivingDetailId);
                                        lotService.Update(lottobeUpdated);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return "";
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
                    List<ReceivingDetail> totalReceived = service.GetAllByPODetailId(detail.PODetailId).Where(rec => rec.ReceiptId != Convert.ToInt64(id)).ToList();

                    detail.BalanceQty = currentPODetail.Quantity - (totalReceived.Count() > 0 ? totalReceived.Sum(rec => rec.Quantity) - detail.ThisPurchaseQty : detail.ThisPurchaseQty);
                    detail.OrderQty = currentPODetail.Quantity;
                    detail.PurchaseQty = totalReceived.Count() > 0 ? totalReceived.Sum(rec => rec.Quantity) - detail.ThisPurchaseQty : 0;
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

            ViewBag.PO = poService.GetSingle(receiving.POId.ToString(), AuthenticationHelper.CompanyId.Value);

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

                string saveResult = save(SessionHelper.Receiving);
                if (string.IsNullOrEmpty(saveResult))
                {
                    SessionHelper.Receiving = null;
                    return Json("Saved Successfully");
                }
                else
                    return Json(saveResult);
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
                    ReceivingDetailModel completeModel = SessionHelper.Receiving.ReceivingDetail.FirstOrDefault(x => x.Id == model.Id);

                    Item item = itemService.GetSingle(completeModel.ItemId.ToString(), AuthenticationHelper.CompanyId.Value);
                    if (item.LotControl)
                    {
                        if (string.IsNullOrEmpty(model.LotNo))
                        {
                            ViewData["EditError"] = "Please provide Lot #!";
                            return PartialView("_Detail", getReceivingDetail());
                        }
                    }
                    else 
                    {
                        if(!string.IsNullOrEmpty(model.LotNo))
                        {
                            ViewData["EditError"] = "Item does not support Lot!";
                            return PartialView("_Detail", getReceivingDetail());
                        }
                    }

                    if (item.SerialControl)
                    {
                        if (string.IsNullOrEmpty(model.SerialNo))
                        {
                            ViewData["EditError"] = "Please provide Serial #!";
                            return PartialView("_Detail", getReceivingDetail());
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(model.LotNo))
                            {
                                ViewData["EditError"] = "Please provide Lot #!";
                                return PartialView("_Detail", getReceivingDetail());
                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(model.SerialNo))
                        {
                            ViewData["EditError"] = "Item does not support Serials!";
                            return PartialView("_Detail", getReceivingDetail());
                        }
                    }

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

                    if (model.ThisPurchaseQty > completeModel.BalanceQty+model.ThisPurchaseQty)
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
                        else
                        {
                            List<string> serials = model.SerialNo.Split(new char[] { ',' }).ToList();
                            if (serials.Count() != model.ThisPurchaseQty)
                            {
                                ViewData["EditError"] = "Serials must be according to the Quantity!";
                                return PartialView("_Detail", getReceivingDetail());
                            }
                        }
                    }
                    ReceivingDetailModel currentModel = SessionHelper.Receiving.ReceivingDetail.FirstOrDefault(rec => rec.Id == model.Id);

                    currentModel.Id = model.Id;
                    currentModel.LocatorId = model.LocatorId;
                    currentModel.LotNo = model.LotNo;
                    currentModel.SerialNo = model.SerialNo;
                    currentModel.WarehouseId = model.WarehouseId;
                    currentModel.ThisPurchaseQty = model.ThisPurchaseQty;
                    currentModel.BalanceQty = completeModel.BalanceQty + completeModel.ThisPurchaseQty - model.ThisPurchaseQty;
                    currentModel.OrderQty = completeModel.OrderQty;
                    currentModel.LotNoId = completeModel.LotNoId;
                    currentModel.ItemId = completeModel.ItemId;
                    currentModel.ItemName = completeModel.ItemName;
                    currentModel.PODetailId = completeModel.PODetailId;
                    currentModel.ReceiptId = completeModel.ReceiptId;

                    SessionHelper.Receiving.ReceivingDetail.Remove(SessionHelper.Receiving.ReceivingDetail.FirstOrDefault(rec => rec.Id == model.Id));
                    SessionHelper.Receiving.ReceivingDetail.Add(currentModel);
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
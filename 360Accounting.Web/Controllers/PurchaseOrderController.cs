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
    public class PurchaseOrderController : BaseController
    {
        private IPurchaseOrderService service;
        private IBuyerService buyerService;
        private IVendorService vendorService;

        public PurchaseOrderController()
        {
            service = IoC.Resolve<IPurchaseOrderService>("PurchaseOrderService");
            buyerService = IoC.Resolve<IBuyerService>("BuyerService");
            vendorService = IoC.Resolve<IVendorService>("VendorService");
        }

        #region Private Methods

        private PurchaseOrder getEntityByModel(PurchaseOrderModel model)
        {
            if (model == null) return null;

            PurchaseOrder entity = new PurchaseOrder();

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

            entity.BuyerId = model.BuyerId;
            entity.CompanyId = model.CompanyId;
            entity.CreateBy = model.CreateBy;
            entity.CreateDate = model.CreateDate;
            entity.Description = model.Description;
            entity.Id = model.Id;
            entity.PODate = model.PODate;
            entity.PONo = model.PONo;
            entity.SOBId = model.SOBId;
            entity.Status = model.Status;
            entity.UpdateBy = model.UpdateBy;
            entity.UpdateDate = model.UpdateDate;
            entity.VendorId = model.VendorId;
            entity.VendorSiteId = model.VendorSiteId;

            return entity;
        }

        private PurchaseOrderDetail getEntityByModel(PurchaseOrderDetailModel model)
        {
            if (model == null) return null;

            PurchaseOrderDetail entity = new PurchaseOrderDetail();

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

            entity.CreateBy = model.CreateBy;
            entity.CreateDate = model.CreateDate;
            entity.Id = model.Id;
            entity.ItemId = model.ItemId;
            entity.NeedByDate = model.NeedByDate;
            entity.POId = model.POId;
            entity.Price = model.Price;
            entity.Quantity = model.Quantity;
            entity.UpdateBy = model.UpdateBy;
            entity.UpdateDate = model.UpdateDate;

            return entity;
        }

        private string generatePONum(PurchaseOrderModel model)
        {
            var currentDocument = service.GetAll(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId).OrderByDescending(rec => rec.Id).FirstOrDefault();
            string newDocNo = "";
            if (currentDocument != null)
            {
                int outVal;
                bool isNumeric = int.TryParse(currentDocument.PONo, out outVal);
                if (isNumeric && currentDocument.PONo.Length == 8)
                {
                    newDocNo = (int.Parse(currentDocument.PONo) + 1).ToString();
                    return newDocNo;
                }
            }

            //Create New DocNum..
            string yearDigit = model.PODate.ToString("yy");
            string monthDigit = model.PODate.ToString("MM");
            string docNo = int.Parse("1").ToString().PadLeft(4, '0');

            return yearDigit + monthDigit + docNo;
        }

        private List<PurchaseOrderDetailModel> getPODetail([Optional]string poId)
        {
            if (poId == null)
                return SessionHelper.PurchaseOrder.PurchaseOrderDetail.ToList();
            else
            {
                IList<PurchaseOrderDetailModel> modelList = service.GetAllPODetail(Convert.ToInt64(poId)).Select(x => new PurchaseOrderDetailModel(x)).ToList();
                return modelList.ToList();
            }
        }

        private void save(PurchaseOrderModel model)
        {
            PurchaseOrder entity = getEntityByModel(model);

            string result = string.Empty;
            if (entity.IsValid())
            {
                if (model.Id > 0)
                    result = service.Update(entity);
                else
                    result = service.Insert(entity);

                if (!string.IsNullOrEmpty(result))
                {
                    var savedLines = getPODetail(result);
                    if (savedLines.Count() > model.PurchaseOrderDetail.Count())
                    {
                        var tobeDeleted = savedLines.Take(savedLines.Count() - model.PurchaseOrderDetail.Count());
                        foreach (var item in tobeDeleted)
                        {
                            service.DeletePODetail(item.Id);
                        }
                        savedLines = getPODetail(result);
                    }

                    foreach (var detail in model.PurchaseOrderDetail)
                    {
                        PurchaseOrderDetail detailEntity = getEntityByModel(detail);
                        if (detailEntity.IsValid())
                        {
                            detailEntity.POId = Convert.ToInt64(result);
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

        #endregion

        #region Actions

        public ActionResult Index()
        {
            SessionHelper.PurchaseOrder= null;
            return View();
        }

        public ActionResult ListPartial()
        {
            return PartialView("_List", service.GetAllPO(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId).Select(x => new PurchaseOrderModel(x, true)).ToList());
        }

        public ActionResult Create()
        {
            PurchaseOrderModel po = SessionHelper.PurchaseOrder;
            if (po == null)
            {
                po = new PurchaseOrderModel
                {
                    CompanyId = AuthenticationHelper.CompanyId.Value,
                    PODate = DateTime.Now,
                    PONo = "New",
                    SOBId = SessionHelper.SOBId
                };
                SessionHelper.PurchaseOrder = po;
            }

            po.Buyers = buyerService.GetAll(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, po.PODate, po.PODate).
                Select(x => new SelectListItem { 
                     Text = x.Name,
                     Value = x.Id.ToString()
                }).ToList();

            po.Vendors = vendorService.GetAll(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, po.PODate, po.PODate).
                Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();

            if (po.Vendors != null && po.Vendors.Count() > 0)
            {
                po.VendorId = po.VendorId > 0 ? po.VendorId : Convert.ToInt64(po.Vendors.FirstOrDefault().Value);
                po.VendorSites = vendorService.GetAllSites(po.VendorId, AuthenticationHelper.CompanyId.Value).
                    Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();

                if(po.VendorSites != null && po.VendorSites.Count()>0)
                    po.VendorSiteId = po.VendorSiteId > 0 ? po.VendorSiteId : Convert.ToInt64(po.VendorSites.FirstOrDefault().Value);
            }

            if (po.Buyers != null && po.Buyers.Count() > 0)
                po.BuyerId = po.BuyerId > 0 ? po.BuyerId : Convert.ToInt64(po.Buyers.FirstOrDefault().Value);

            return View("Edit", po);
        }

        public ActionResult Edit(string id)
        {
            PurchaseOrderModel po = new PurchaseOrderModel(service.GetSingle(id, AuthenticationHelper.CompanyId.Value));
            po.PurchaseOrderDetail = service.GetAllPODetail(po.Id).Select(x => new PurchaseOrderDetailModel(x)).ToList();
            SessionHelper.PurchaseOrder = po;

            po.Buyers = buyerService.GetAll(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, po.PODate, po.PODate).
               Select(x => new SelectListItem
               {
                   Text = x.Name,
                   Value = x.Id.ToString()
               }).ToList();

            po.Vendors = vendorService.GetAll(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, po.PODate, po.PODate).
                Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();

            if (po.Vendors != null && po.Vendors.Count() > 0)
            {
                po.VendorSites = vendorService.GetAllSites(po.VendorId, AuthenticationHelper.CompanyId.Value).
                    Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();
            }

            return View("Edit", po);
        }

        public ActionResult Delete(string id)
        {
            service.Delete(id, AuthenticationHelper.CompanyId.Value);
            return RedirectToAction("Index");
        }

        public ActionResult Save(long id, string poNo, DateTime poDate, long buyerId, long vendorId, long vendorSiteId, string description, string status, long companyId, long sobId) 
        {
            if (SessionHelper.PurchaseOrder != null)
            {
                if (SessionHelper.PurchaseOrder.PurchaseOrderDetail.Count == 0)
                    return Json("No detail information available to save!");

                SessionHelper.PurchaseOrder.BuyerId = buyerId;
                SessionHelper.PurchaseOrder.CompanyId = companyId;
                SessionHelper.PurchaseOrder.Description = description;
                SessionHelper.PurchaseOrder.Id = id;
                SessionHelper.PurchaseOrder.PODate = poDate;
                SessionHelper.PurchaseOrder.PONo = poNo;
                SessionHelper.PurchaseOrder.SOBId = sobId;
                SessionHelper.PurchaseOrder.Status = status;
                SessionHelper.PurchaseOrder.VendorId = vendorId;
                SessionHelper.PurchaseOrder.VendorSiteId = vendorSiteId;

                if (SessionHelper.PurchaseOrder.PONo == "New")
                    SessionHelper.PurchaseOrder.PONo = generatePONum(SessionHelper.PurchaseOrder);

                save(SessionHelper.PurchaseOrder);
                SessionHelper.PurchaseOrder = null;
                return Json("Saved Successfully");
            }

            return Json("No information available to save!");
        }

        public ActionResult DetailPartial()
        {
            return PartialView("_Detail", getPODetail());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewPartial(PurchaseOrderDetailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (SessionHelper.PurchaseOrder != null)
                    {
                        if (SessionHelper.PurchaseOrder.PurchaseOrderDetail != null && SessionHelper.PurchaseOrder.PurchaseOrderDetail.Count() > 0)
                            model.Id = SessionHelper.PurchaseOrder.PurchaseOrderDetail.LastOrDefault().Id + 1;
                        else
                            model.Id = 1;
                    }
                    else
                        model.Id = 1;

                    SessionHelper.PurchaseOrder.PurchaseOrderDetail.Add(model);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_Detail", getPODetail());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdatePartial(PurchaseOrderDetailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    SessionHelper.PurchaseOrder.PurchaseOrderDetail.FirstOrDefault(rec => rec.Id == model.Id).CreateBy = model.CreateBy;
                    SessionHelper.PurchaseOrder.PurchaseOrderDetail.FirstOrDefault(rec => rec.Id == model.Id).CreateDate = model.CreateDate;
                    SessionHelper.PurchaseOrder.PurchaseOrderDetail.FirstOrDefault(rec => rec.Id == model.Id).Id = model.Id;
                    SessionHelper.PurchaseOrder.PurchaseOrderDetail.FirstOrDefault(rec => rec.Id == model.Id).ItemId = model.ItemId;
                    SessionHelper.PurchaseOrder.PurchaseOrderDetail.FirstOrDefault(rec => rec.Id == model.Id).NeedByDate = model.NeedByDate;
                    SessionHelper.PurchaseOrder.PurchaseOrderDetail.FirstOrDefault(rec => rec.Id == model.Id).POId = model.POId;
                    SessionHelper.PurchaseOrder.PurchaseOrderDetail.FirstOrDefault(rec => rec.Id == model.Id).Price = model.Price;
                    SessionHelper.PurchaseOrder.PurchaseOrderDetail.FirstOrDefault(rec => rec.Id == model.Id).Quantity = model.Quantity;
                    SessionHelper.PurchaseOrder.PurchaseOrderDetail.FirstOrDefault(rec => rec.Id == model.Id).UpdateBy = model.UpdateBy;
                    SessionHelper.PurchaseOrder.PurchaseOrderDetail.FirstOrDefault(rec => rec.Id == model.Id).UpdateDate = model.UpdateDate;
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_Detail", getPODetail());
        }

        public ActionResult DeletePartial(PurchaseOrderDetailModel model)
        {
            try
            {
                PurchaseOrderModel po = SessionHelper.PurchaseOrder;
                PurchaseOrderDetailModel poDetail = po.PurchaseOrderDetail.FirstOrDefault(rec => rec.Id == model.Id);
                SessionHelper.PurchaseOrder.PurchaseOrderDetail.Remove(poDetail);
            }
            catch (Exception ex)
            {
                ViewData["EditError"] = ex.Message;
            }

            return PartialView("_Detail", getPODetail());
        }

        public ActionResult ChangeDate(DateTime poDate)
        {
            if (poDate < DateTime.Now.Date)
                return Json("PO date cannot be the past date!");

            SessionHelper.PurchaseOrder.PODate = poDate;
            return Json("Success");
        }

        public JsonResult VendorSiteList(long vendorId)
        {
            List<SelectListItem> vendorList = VendorHelper.GetVendorSiteList(vendorId);
            return Json(vendorList, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
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
    public class RequisitionController : BaseController
    {
        private IRequisitionService service;
        private IBuyerService buyerService;

        public RequisitionController()
        {
            service = IoC.Resolve<IRequisitionService>("RequisitionService");
            buyerService = IoC.Resolve<IBuyerService>("BuyerService");
        }

        #region Private Methods

        private Requisition getEntityByModel(RequisitionModel model)
        {
            if (model == null) return null;

            Requisition entity = new Requisition();

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
            entity.Description = model.Description;
            entity.Id = model.Id;
            entity.RequisitionDate = model.RequisitionDate;
            entity.RequisitionNo = model.RequisitionNo;
            entity.SOBId = model.SOBId;

            return entity;
        }

        private RequisitionDetail getEntityByModel(RequisitionDetailModel model)
        {
            if (model == null) return null;

            RequisitionDetail entity = new RequisitionDetail();

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
            entity.NeedByDate = model.NeedByDate;
            entity.Price = model.Price;
            entity.Quantity = model.Quantity;
            entity.RequisitionId = model.RequisitionId;
            entity.Status = model.Status;
            entity.VendorId = model.VendorId;
            entity.VendorSiteId = model.VendorSiteId;

            return entity;
        }

        private string generateReqNum(RequisitionModel model)
        {
            var currentDocument = service.GetAll(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId).OrderByDescending(rec => rec.Id).FirstOrDefault();
            string newDocNo = "";
            if (currentDocument != null)
            {
                int outVal;
                bool isNumeric = int.TryParse(currentDocument.RequisitionNo, out outVal);
                if (isNumeric && currentDocument.RequisitionNo.Length == 8)
                {
                    newDocNo = (int.Parse(currentDocument.RequisitionNo) + 1).ToString();
                    return newDocNo;
                }
            }

            //Create New DocNum..
            string yearDigit = model.RequisitionDate.ToString("yy");
            string monthDigit = model.RequisitionDate.ToString("MM");
            string docNo = int.Parse("1").ToString().PadLeft(4, '0');

            return yearDigit + monthDigit + docNo;
        }

        private List<RequisitionDetailModel> getRequisitionDetail([Optional]string requisitionId)
        {
            if (requisitionId == null)
                return SessionHelper.Requisition.RequisitionDetail.ToList();
            else
            {
                IList<RequisitionDetailModel> modelList = service.GetAllRequisitionDetail(Convert.ToInt64(requisitionId)).Select(x => new RequisitionDetailModel(x)).ToList();
                return modelList.ToList();
            }
        }

        private void save(RequisitionModel model)
        {
            Requisition entity = getEntityByModel(model);

            string result = string.Empty;
            if (entity.IsValid())
            {
                if (model.Id > 0)
                    result = service.Update(entity);
                else
                    result = service.Insert(entity);

                if (!string.IsNullOrEmpty(result))
                {
                    var savedLines = getRequisitionDetail(result);
                    if (savedLines.Count() > model.RequisitionDetail.Count())
                    {
                        var tobeDeleted = savedLines.Take(savedLines.Count() - model.RequisitionDetail.Count());
                        foreach (var item in tobeDeleted)
                        {
                            service.DeleteRequisitionDetail(item.Id);
                        }
                        savedLines = getRequisitionDetail(result);
                    }

                    foreach (var detail in model.RequisitionDetail)
                    {
                        RequisitionDetail detailEntity = getEntityByModel(detail);
                        if (detailEntity.IsValid())
                        {
                            detailEntity.RequisitionId = Convert.ToInt64(result);
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
            SessionHelper.Requisition = null;
            return View();
        }

        public ActionResult ListPartial()
        {
            return PartialView("_List", service.GetAllRequisition(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId).Select(x => new RequisitionModel(x, true)).ToList());
        }

        public ActionResult Create()
        {
            RequisitionModel requisition = SessionHelper.Requisition;
            if (requisition == null)
            {
                requisition = new RequisitionModel
                {
                    RequisitionDate = DateTime.Now,
                    RequisitionNo = "New",
                    CompanyId = AuthenticationHelper.CompanyId.Value,
                    SOBId = SessionHelper.SOBId
                };
                SessionHelper.Requisition = requisition;
            }

            requisition.Buyers = buyerService.GetAll(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, requisition.RequisitionDate, requisition.RequisitionDate).
                Select(x => new SelectListItem { 
                     Text = x.Name,
                     Value = x.Id.ToString()
                }).ToList();

            if (requisition.Buyers != null && requisition.Buyers.Count() > 0)
                requisition.BuyerId = requisition.BuyerId > 0 ? requisition.BuyerId : Convert.ToInt64(requisition.Buyers.FirstOrDefault().Value);

            return View("Edit", requisition);
        }

        public ActionResult Edit(string id)
        {
            RequisitionModel requisition = new RequisitionModel(service.GetSingle(id, AuthenticationHelper.CompanyId.Value));
            requisition.RequisitionDetail = service.GetAllRequisitionDetail(requisition.Id).Select(x => new RequisitionDetailModel(x)).ToList();
            SessionHelper.Requisition = requisition;

            requisition.Buyers = buyerService.GetAll(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, requisition.RequisitionDate, requisition.RequisitionDate).
                Select(x => new SelectListItem { 
                     Value = x.Id.ToString(),
                      Text = x.Name
                }).ToList();

            return View("Edit", requisition);
        }

        public ActionResult Delete(string id)
        {
            service.Delete(id, AuthenticationHelper.CompanyId.Value);
            return RedirectToAction("Index");
        }

        public ActionResult Save(long id, string requisitionNo, DateTime requisitionDate, long buyerId, string description, long companyId, long sobId) 
        {
            if (SessionHelper.Requisition != null)
            {
                if (SessionHelper.Requisition.RequisitionDetail.Count == 0)
                    return Json("No detail information available to save!");

                SessionHelper.Requisition.BuyerId = buyerId;
                SessionHelper.Requisition.CompanyId = companyId;
                SessionHelper.Requisition.Description = description;
                SessionHelper.Requisition.Id = id;
                SessionHelper.Requisition.RequisitionDate = requisitionDate;
                SessionHelper.Requisition.RequisitionNo = requisitionNo;
                SessionHelper.Requisition.SOBId = sobId;

                if (SessionHelper.Requisition.RequisitionNo == "New")
                    SessionHelper.Requisition.RequisitionNo = generateReqNum(SessionHelper.Requisition);

                save(SessionHelper.Requisition);
                SessionHelper.Requisition = null;
                return Json("Saved Successfully");
            }

            return Json("No information available to save!");
        }

        public ActionResult DetailPartial()
        {
            return PartialView("_Detail", getRequisitionDetail());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewPartial(RequisitionDetailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (SessionHelper.Requisition != null)
                    {
                        if (SessionHelper.Requisition.RequisitionDetail != null && SessionHelper.Requisition.RequisitionDetail.Count() > 0)
                            model.Id = SessionHelper.Requisition.RequisitionDetail.LastOrDefault().Id + 1;
                        else
                            model.Id = 1;
                    }
                    else
                        model.Id = 1;

                    SessionHelper.Requisition.RequisitionDetail.Add(model);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_Detail", getRequisitionDetail());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdatePartial(RequisitionDetailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    SessionHelper.Requisition.RequisitionDetail.FirstOrDefault(rec => rec.Id == model.Id).CreateBy = model.CreateBy;
                    SessionHelper.Requisition.RequisitionDetail.FirstOrDefault(rec => rec.Id == model.Id).CreateDate = model.CreateDate;
                    SessionHelper.Requisition.RequisitionDetail.FirstOrDefault(rec => rec.Id == model.Id).Id = model.Id;
                    SessionHelper.Requisition.RequisitionDetail.FirstOrDefault(rec => rec.Id == model.Id).ItemId = model.ItemId;
                    SessionHelper.Requisition.RequisitionDetail.FirstOrDefault(rec => rec.Id == model.Id).NeedByDate = model.NeedByDate;
                    SessionHelper.Requisition.RequisitionDetail.FirstOrDefault(rec => rec.Id == model.Id).Price = model.Price;
                    SessionHelper.Requisition.RequisitionDetail.FirstOrDefault(rec => rec.Id == model.Id).Quantity = model.Quantity;
                    SessionHelper.Requisition.RequisitionDetail.FirstOrDefault(rec => rec.Id == model.Id).RequisitionId = model.RequisitionId;
                    SessionHelper.Requisition.RequisitionDetail.FirstOrDefault(rec => rec.Id == model.Id).Status = model.Status;
                    SessionHelper.Requisition.RequisitionDetail.FirstOrDefault(rec => rec.Id == model.Id).UpdateBy = model.UpdateBy;
                    SessionHelper.Requisition.RequisitionDetail.FirstOrDefault(rec => rec.Id == model.Id).UpdateDate = model.UpdateDate;
                    SessionHelper.Requisition.RequisitionDetail.FirstOrDefault(rec => rec.Id == model.Id).VendorId = model.VendorId;
                    SessionHelper.Requisition.RequisitionDetail.FirstOrDefault(rec => rec.Id == model.Id).VendorSiteId = model.VendorSiteId;
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_Detail", getRequisitionDetail());
        }

        public ActionResult DeletePartial(RequisitionDetailModel model)
        {
            try
            {
                RequisitionModel requisition = SessionHelper.Requisition;
                RequisitionDetailModel requisitionDetail = requisition.RequisitionDetail.FirstOrDefault(rec => rec.Id == model.Id);
                SessionHelper.Requisition.RequisitionDetail.Remove(requisitionDetail);
            }
            catch (Exception ex)
            {
                ViewData["EditError"] = ex.Message;
            }

            return PartialView("_Detail", getRequisitionDetail());
        }

        public ActionResult ChangeDate(DateTime requisitionDate)
        {
            if (requisitionDate < DateTime.Now.Date)
                return Json("Requisition date cannot be the past date!");

            SessionHelper.Requisition.RequisitionDate = requisitionDate;
            return Json("Success");
        }

        public ActionResult ComboBoxVSitePartial()
        {
            int vendorId = (Request.Params["VendorId"] != null) ? int.Parse(Request.Params["VendorId"]) : -1;
             return PartialView(VendorHelper.GetVendorSiteList(Convert.ToInt64(vendorId)));
        }

        #endregion
    }
}
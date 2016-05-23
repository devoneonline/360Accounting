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
    public class RFQController : Controller
    {
        private IRFQService service;
        private IBuyerService buyerService;

        public RFQController()
        {
            service = IoC.Resolve<IRFQService>("RFQService");
            buyerService = IoC.Resolve<IBuyerService>("BuyerService");
        }

        #region Private Methods
        
        private RFQ getEntityByModel(RFQModel model)
        {
            if (model == null) return null;
            RFQ entity = new RFQ();
            entity.BuyerId = model.BuyerId;
            entity.CloseDate = model.CloseDate;
            entity.Id = model.Id;
            entity.RFQDate = model.RFQDate;
            entity.RFQNo = model.RFQNo;
            entity.SOBId = SessionHelper.SOBId;
            entity.Status = model.Status;
            
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

            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            return entity;
        }

        private RFQDetail getEntityByModel(RFQDetailModel model)
        {
            if (model == null) return null;
            RFQDetail entity = new RFQDetail();

            entity.Id = model.Id;
            entity.ItemId = model.ItemId;
            entity.Quantity = model.Quantity;
            entity.RFQId = model.RFQId;
            entity.TargetPrice = model.TargetPrice;
            
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

            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            return entity;
        }

        private List<RFQModel> getRFQs()
        {
            return service.GetAllRFQs(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId).Select(x => new RFQModel(x, true)).ToList();
        }

        private List<RFQDetailModel> getRFQDetail([Optional]string rfqId)
        {
            if (rfqId == null)
                return SessionHelper.RFQ.RFQDetail.ToList();
            else
            {
                IList<RFQDetailModel> modelList = service.GetAllRFQDetail(Convert.ToInt64(rfqId)).Select(x => new RFQDetailModel(x)).ToList();
                return modelList.ToList();
            }                
        }

        private string generateRFQNo(RFQModel model)
        {
            var currentDocument = service.GetAll(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId).OrderByDescending(rec => rec.Id).FirstOrDefault();
            string newDocNo = "";
            if (currentDocument != null)
            {
                int outVal;
                bool isNumeric = int.TryParse(currentDocument.RFQNo, out outVal);
                if (isNumeric && currentDocument.RFQNo.Length == 8)
                {
                    newDocNo = (int.Parse(currentDocument.RFQNo) + 1).ToString();
                    return newDocNo;
                }
            }

            //Create New DocNum..
            string yearDigit = model.RFQDate.ToString("yy");
            string monthDigit = model.RFQDate.ToString("MM");
            string docNo = int.Parse("1").ToString().PadLeft(4, '0');

            return yearDigit + monthDigit + docNo;
        }

        private void Save(RFQModel model)
        {
            RFQ entity = getEntityByModel(model);

            string result = string.Empty;
            if (entity.IsValid())
            {
                if (model.Id > 0)
                    result = service.Update(entity);
                else
                    result = service.Insert(entity);

                if (!string.IsNullOrEmpty(result))
                {
                    //var savedLines = getDetailByOrderId(Convert.ToInt64(result));
                    IList<RFQDetailModel> savedLines = service.GetAllRFQDetail(Convert.ToInt64(result)).Select(x => new RFQDetailModel(x)).ToList();
                    if (savedLines.Count() > model.RFQDetail.Count())
                    {
                        var tobeDeleted = savedLines.Take(savedLines.Count() - model.RFQDetail.Count());
                        foreach (var item in tobeDeleted)
                        {
                            service.DeleteRFQDetail(item.Id);
                        }
                        savedLines = service.GetAllRFQDetail(Convert.ToInt64(result)).Select(x => new RFQDetailModel(x)).ToList();
                    }

                    foreach (var detail in model.RFQDetail)
                    {
                        RFQDetail detailEntity = getEntityByModel(detail);
                        if (detailEntity.IsValid())
                        {
                            detailEntity.RFQId = Convert.ToInt64(result);
                            if (savedLines.Count() > 0)
                            {
                                detailEntity.Id = savedLines.FirstOrDefault().Id;
                                savedLines.Remove(savedLines.FirstOrDefault(rec => rec.Id == detailEntity.Id));
                                service.UpdateRFQDetail(detailEntity);
                            }
                            else
                                service.InsertRFQDetail(detailEntity);
                        }
                    }
                }
            }
        }

        #endregion

        #region Action Results

        public ActionResult Delete(string id)
        {
            service.Delete(id, AuthenticationHelper.CompanyId.Value);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            RFQModel model = new RFQModel(service.GetSingle(id, AuthenticationHelper.CompanyId.Value));
            model.RFQDetail = service.GetAllRFQDetail(Convert.ToInt64(id)).Select(x => new RFQDetailModel(x)).ToList();
            SessionHelper.RFQ = model;

            model.Buyers = buyerService.GetAll(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, model.RFQDate, model.RFQDate)
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
            if (model.Buyers != null && model.Buyers.Count > 0)
            {
                model.BuyerId = model.BuyerId > 0 ? model.BuyerId : Convert.ToInt64(model.Buyers.FirstOrDefault().Value);
            }

            return View("Edit", model);
        }

        public ActionResult Save(string rfqNo, DateTime rfqDate, DateTime? closeDate, long buyerId, string status)
        {
            if (SessionHelper.RFQ != null)
            {
                if (SessionHelper.RFQ.RFQDetail.Count == 0)
                    return Json("No detail information available to save!");

                SessionHelper.RFQ.CompanyId = AuthenticationHelper.CompanyId.Value;
                SessionHelper.RFQ.CloseDate = closeDate;
                SessionHelper.RFQ.RFQDate = rfqDate;
                SessionHelper.RFQ.RFQNo = rfqNo;
                SessionHelper.RFQ.BuyerId = buyerId;
                SessionHelper.RFQ.Status = status;

                if (SessionHelper.RFQ.RFQNo == "New")
                    SessionHelper.RFQ.RFQNo = generateRFQNo(SessionHelper.RFQ);

                Save(SessionHelper.RFQ);
                SessionHelper.RFQ = null;
                return Json("Saved Successfully");
            }

            return Json("No information available to save!");
        }

        public ActionResult DeleteInLine(OrderDetailModel model)
        {
            try
            {
                RFQModel rfq = SessionHelper.RFQ;
                RFQDetailModel rfqDetail = rfq.RFQDetail.FirstOrDefault(x => x.Id == model.Id);
                SessionHelper.RFQ.RFQDetail.Remove(rfqDetail);
            }
            catch (Exception ex)
            {
                ViewData["EditError"] = ex.Message;
            }

            return PartialView("_Detail", getRFQDetail());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateInLine(RFQDetailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    RFQModel rfq = SessionHelper.RFQ;
                    rfq.RFQDetail.FirstOrDefault(x => x.Id == model.Id).ItemId = model.ItemId;
                    rfq.RFQDetail.FirstOrDefault(x => x.Id == model.Id).Quantity = model.Quantity;
                    rfq.RFQDetail.FirstOrDefault(x => x.Id == model.Id).TargetPrice = model.TargetPrice;
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_Detail", getRFQDetail());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewInLine(RFQDetailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (SessionHelper.RFQ != null)
                    {
                        if (SessionHelper.RFQ.RFQDetail != null && SessionHelper.RFQ.RFQDetail.Count() > 0)
                            model.Id = SessionHelper.RFQ.RFQDetail.LastOrDefault().Id + 1;
                        else
                            model.Id = 1;
                    }
                    else
                        model.Id = 1;

                    RFQModel rfq = SessionHelper.RFQ;
                    rfq.RFQDetail.Add(model);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_Detail", getRFQDetail());
        }

        public ActionResult DetailPartial()
        {
            return PartialView("_Detail", getRFQDetail());
        }

        public ActionResult CheckDate(DateTime rfqDate)
        {
            if (rfqDate < DateTime.Now.Date)
                return Json("RFQ Date cannot be the past date!.");

            RFQModel rfq = new RFQModel();


            rfq.Buyers = buyerService.GetAll(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, rfq.RFQDate, rfq.RFQDate)
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
            if (rfq.Buyers != null && rfq.Buyers.Count > 0)
            {
                rfq.BuyerId = rfq.BuyerId > 0 ? rfq.BuyerId : Convert.ToInt64(rfq.Buyers.FirstOrDefault().Value);
            }

            return Json(rfq);
        }

        public ActionResult Create()
        {
            RFQModel model = SessionHelper.RFQ;
            if (model == null)
            {
                model = new RFQModel
                {
                    CompanyId = AuthenticationHelper.CompanyId.Value,
                    RFQDate = DateTime.Now,
                    RFQDetail = new List<RFQDetailModel>(),
                    RFQNo = "New"
                };
                SessionHelper.RFQ = model;
            }

            model.Buyers = buyerService.GetAll(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, model.RFQDate, model.RFQDate)
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
            if (model.Buyers != null && model.Buyers.Count > 0)
            {
                model.BuyerId = model.BuyerId > 0 ? model.BuyerId : Convert.ToInt64(model.Buyers.FirstOrDefault().Value);
            }

            return View("Edit", model);
        }

        public ActionResult ListPartial()
        {
            return PartialView("_List", getRFQs());
        }

        public ActionResult Index()
        {
            SessionHelper.RFQ = null;
            return View();
        }

        #endregion
    }
}
using _360Accounting.Core;
using _360Accounting.Web;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _360Accounting.Web.Reports;
using _360Accounting.Core.Entities;
using DevExpress.Web.Mvc;
using _360Accounting.Common;
using System.Web.Security;
using _360Accounting.Core.IService;

namespace _360Accounting.Web.Controllers
{
    [Authorize]
    public class MoveOrderController : BaseController
    {
        public MoveOrderController()
        {

        }
        
        public ActionResult Delete(string id)
        {
            try
            {
                MoveOrderHelper.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { message = ex.Message });
            }
        }

        public ActionResult Edit(string id)
        {
            MoveOrderModel model = MoveOrderHelper.GetMoveOrder(id);
            
            model.MoveOrderDetail = MoveOrderHelper.GetMoveOrderLines(id).ToList();
            model.SOBId = SessionHelper.SOBId;
            model.CompanyId = AuthenticationHelper.CompanyId.Value;
            SessionHelper.MoveOrder = model;

            return View("Create", model);
        }

        public ActionResult Index(MoveOrderListModel model, string message="")
        {
            ViewBag.ErrorMessage = message;
            SessionHelper.Item = null;
            model.SOBId = SessionHelper.SOBId;
            return View(model);
        }

        public ActionResult MoveOrderPartial(MoveOrderListModel model)
        {
            return PartialView("_List", MoveOrderHelper.GetMoveOrders(model.SOBId));
        }

        public ActionResult Create()
        {
            MoveOrderModel model = SessionHelper.MoveOrder;
            if (model == null)
            {
                model = new MoveOrderModel
                {
                    SOBId = SessionHelper.SOBId,
                    DateRequired = DateTime.Now,
                    MoveOrderDate = DateTime.Now,
                    MoveOrderNo = "New"
                };
                model.CompanyId = AuthenticationHelper.CompanyId.Value;
                ViewBag.SOBName = SetOfBookHelper.GetSetOfBook(SessionHelper.SOBId.ToString()).Name;

                SessionHelper.MoveOrder = model;
                if (SessionHelper.MoveOrder.MoveOrderDetail == null)
                    SessionHelper.MoveOrder.MoveOrderDetail = new List<MoveOrderDetailModel>();
            }
            
            return View(model);
        }

        public ActionResult CreatePartial()
        {
            return PartialView("createPartial", MoveOrderHelper.GetMoveOrderLines());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewPartial(MoveOrderDetailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool validated = false;
                    //Daterequired validation..
                    //Warehouse validation..
                    //Locator validation..
                    if (SessionHelper.MoveOrder != null)
                    {
                        if (SessionHelper.MoveOrder.MoveOrderDetail.Any(rec => rec.LotNo == model.LotNo && rec.ItemId == model.ItemId))
                        {
                            ViewData["EditError"] = "Lot Number must be unique";
                            return PartialView("createPartial", MoveOrderHelper.GetMoveOrderLines());
                        }
                        if (SessionHelper.MoveOrder.MoveOrderDetail.Any(rec => rec.LotNo == model.LotNo && rec.SerialNo == model.SerialNo))
                        {
                            ViewData["EditError"] = "Serial Number must be unique";
                            return PartialView("createPartial", MoveOrderHelper.GetMoveOrderLines());
                        }
                    }
                    if (LotNumberHelper.CheckLotNumAvailability(model.LotNo, model.ItemId, SessionHelper.SOBId).Any())
                    {
                        ViewData["EditError"] = "Lot Number must be unique";
                        return PartialView("createPartial", MoveOrderHelper.GetMoveOrderLines());
                    }
                    //if (LotNumberHelper.CheckSerialNumAvailability(model.LotNo, model.SerialNo).Any())
                    //{
                    //    ViewData["EditError"] = "Serial Number must be unique";
                    //    return PartialView("createPartial", MoveOrderHelper.GetMoveOrderLines());
                    //}
                    if (SessionHelper.MoveOrder != null)
                    {
                        model.Id = SessionHelper.MoveOrder.MoveOrderDetail.Count() + 1;
                        validated = true;
                    }
                    else
                        model.Id = 1;

                    if (validated)
                        MoveOrderHelper.InsertMoveOrderDetail(model);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("createPartial", MoveOrderHelper.GetMoveOrderLines());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdatePartial(MoveOrderDetailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Daterequired validation..
                    //Warehouse validation..
                    //Locator validation..
                    if (SessionHelper.MoveOrder != null)
                    {
                        if (SessionHelper.MoveOrder.MoveOrderDetail.Any(rec => rec.LotNo == model.LotNo && rec.ItemId == model.ItemId))
                        {
                            ViewData["EditError"] = "Lot Number must be unique";
                            return PartialView("createPartial", MoveOrderHelper.GetMoveOrderLines());
                        }
                        if (SessionHelper.MoveOrder.MoveOrderDetail.Any(rec => rec.LotNo == model.LotNo && rec.SerialNo == model.SerialNo))
                        {
                            ViewData["EditError"] = "Serial Number must be unique";
                            return PartialView("createPartial", MoveOrderHelper.GetMoveOrderLines());
                        }
                    }
                    if (LotNumberHelper.CheckLotNumAvailability(model.LotNo, model.ItemId, SessionHelper.SOBId).Any())
                    {
                        ViewData["EditError"] = "Lot Number must be unique";
                        return PartialView("createPartial", MoveOrderHelper.GetMoveOrderLines());
                    }
                    //if (LotNumberHelper.CheckSerialNumAvailability(model.LotNo, model.SerialNo).Any())
                    //{
                    //    ViewData["EditError"] = "Serial Number must be unique";
                    //    return PartialView("createPartial", MoveOrderHelper.GetMoveOrderLines());
                    //}
                    MoveOrderHelper.UpdateMoveOrderDetail(model);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("createPartial", MoveOrderHelper.GetMoveOrderLines());
        }

        public ActionResult DeletePartial(MoveOrderDetailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MoveOrderModel moveOrder = SessionHelper.MoveOrder;
                    MoveOrderHelper.DeleteMoveOrderDetail(model);
                    SessionHelper.MoveOrder = moveOrder;
                    IList<MoveOrderDetailModel> moveOrderDetail = MoveOrderHelper.GetMoveOrderLines();
                    return PartialView("createPartial", moveOrderDetail);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("createPartial");
        }

        public ActionResult Save(MoveOrderModel model)
        {
            string message = "";
            try
            {
                bool saved = false;
                if (SessionHelper.MoveOrder != null)
                {
                    SessionHelper.MoveOrder.DateRequired = model.DateRequired;
                    SessionHelper.MoveOrder.Description = model.Description;
                    SessionHelper.MoveOrder.Id = model.Id;
                    SessionHelper.MoveOrder.MoveOrderDate = model.MoveOrderDate;
                    if (model.Id > 0)
                        SessionHelper.MoveOrder.MoveOrderNo = model.MoveOrderNo;
                    else
                        SessionHelper.MoveOrder.MoveOrderNo = MoveOrderHelper.GetDocNo(model.CompanyId, model.SOBId);

                    MoveOrderHelper.Save(SessionHelper.MoveOrder);
                    SessionHelper.MoveOrder = null;
                    saved = true;

                }
                else
                    message = "No Move Order information available!";
                return Json(new { success = saved, message = message });
            }
            catch (Exception e)
            {
                message = e.Message;
                return Json(new { success = false, message = message });
            }
        }
    }
}
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
using _360Accounting.Web.Helpers;

namespace _360Accounting.Web.Controllers
{
    [Authorize]
    public class MiscellaneousTransactionController : Controller
    {
        private ICodeCombinitionService codeCombinitionService;

        public MiscellaneousTransactionController()
        {
            codeCombinitionService = IoC.Resolve<ICodeCombinitionService>("CodeCombinitionService");
        }

        #region Private Methods

        private List<SelectListItem> getCodeCombinationList(long sobId)
        {
            List<SelectListItem> list = codeCombinitionService.GetAll(AuthenticationHelper.User.CompanyId, sobId)
                .Select(x => new SelectListItem
                {
                    Text = Utility.Stringize(".", x.Segment1, x.Segment2, x.Segment3, x.Segment4, x.Segment5, x.Segment6, x.Segment7, x.Segment8),
                    Value = x.Id.ToString()
                }).ToList();
            return list;
        }

        #endregion

        public ActionResult Delete(string id)
        {
            MiscellaneousTransactionHelper.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id, long sobId)
        {
            MiscellaneousTransactionModel model = MiscellaneousTransactionHelper.GetMiscellaneousTransaction(id);
            SessionHelper.MiscellaneousTransaction = model;
            SessionHelper.SOBId = model.SOBId;

            model.MiscellaneousTransactionDetail = MiscellaneousTransactionHelper.GetMiscellaneousTransactionDetail(model.SOBId, model.TransactionType, model.CodeCombinationId, model.TransactionDate).ToList();
            model.SOBId = sobId;
            model.CompanyId = AuthenticationHelper.User.CompanyId;

            model.CodeCombination = getCodeCombinationList(model.SOBId);
            SessionHelper.MiscellaneousTransaction = model;

            return View("Create", model);
        }

        public ActionResult Index(MiscellaneousTransactionListModel model)
        {
            SessionHelper.MiscellaneousTransaction = null;
            if (model.SetOfBooks == null)
            {
                model.SetOfBooks = SetOfBookHelper.GetSetOfBooks()
                    .Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();
                model.SOBId = model.SetOfBooks.Any() ?
                    Convert.ToInt32(model.SetOfBooks.First().Value) : 0;
            }
            return View(model);
        }

        public ActionResult MiscellaneousTransactionPartial(MiscellaneousTransactionListModel model)
        {
            SessionHelper.SOBId = model.SOBId;
            return PartialView("_List", MiscellaneousTransactionHelper.GetMiscellaneousTransactions(model.SOBId));
        }

        public ActionResult Create(long sobId)
        {
            SessionHelper.SOBId = sobId;

            MiscellaneousTransactionModel model = SessionHelper.MiscellaneousTransaction;
            if (model == null)
            {
                model = new MiscellaneousTransactionModel();
                model = new MiscellaneousTransactionModel
                {
                    SOBId = sobId,
                    CodeCombination = getCodeCombinationList(sobId),
                    CompanyId = AuthenticationHelper.User.CompanyId,
                    TransactionDate = DateTime.Now
                };
                model.CodeCombinationId = model.CodeCombination != null && model.CodeCombination.Count > 0 ? Convert.ToInt64(model.CodeCombination.FirstOrDefault().Value) : 0;
                model.CodeCombinationString = model.CodeCombination != null && model.CodeCombination.Count > 0 ? model.CodeCombination.FirstOrDefault().Text : "";
                ViewBag.SOBName = SetOfBookHelper.GetSetOfBook(sobId.ToString()).Name;

                SessionHelper.MiscellaneousTransaction = model;
                if (SessionHelper.MiscellaneousTransaction.MiscellaneousTransactionDetail == null)
                    SessionHelper.MiscellaneousTransaction.MiscellaneousTransactionDetail = new List<MiscellaneousTransactionDetailModel>();
            }
            else
                model.CodeCombination = getCodeCombinationList(sobId);
            
            return View(model);
        }
        
        public ActionResult CreatePartial()
        {
            return PartialView("createPartial", MiscellaneousTransactionHelper.GetMiscellaneousTransactionDetail(SessionHelper.SOBId, SessionHelper.MiscellaneousTransaction.TransactionType, SessionHelper.MiscellaneousTransaction.CodeCombinationId, SessionHelper.MiscellaneousTransaction.TransactionDate));
        }

        public ActionResult CreatePartialClient(string transactionType, long codeCombinationId, DateTime transactionDate)
        {
            return PartialView("createPartial", MiscellaneousTransactionHelper.GetMiscellaneousTransactionDetail(SessionHelper.SOBId, transactionType, codeCombinationId, transactionDate));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewPartial(MiscellaneousTransactionDetailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool validated = false;
                    if (SessionHelper.MiscellaneousTransaction != null)
                    {
                        if (SessionHelper.MiscellaneousTransaction.MiscellaneousTransactionDetail.Any(rec => rec.LotNo == model.LotNo && rec.ItemId == model.ItemId))
                        {
                            ViewData["EditError"] = "Lot Number must be unique";
                            return PartialView("createPartial", MiscellaneousTransactionHelper.GetMiscellaneousTransactionDetail(model.SOBId, model.TransactionType, model.CodeCombinationId, model.TransactionDate));
                        }
                        if (SessionHelper.MiscellaneousTransaction.MiscellaneousTransactionDetail.Any(rec => rec.LotNo == model.LotNo && rec.SerialNo == model.SerialNo))
                        {
                            ViewData["EditError"] = "Serial Number must be unique";
                            return PartialView("createPartial", MiscellaneousTransactionHelper.GetMiscellaneousTransactionDetail(model.SOBId, model.TransactionType, model.CodeCombinationId, model.TransactionDate));
                        }
                    }
                    if (LotNumberHelper.CheckLotNumAvailability(model.LotNo, model.ItemId, SessionHelper.SOBId).Any())
                    {
                        ViewData["EditError"] = "Lot Number must be unique";
                        return PartialView("createPartial", MiscellaneousTransactionHelper.GetMiscellaneousTransactionDetail(model.SOBId, model.TransactionType, model.CodeCombinationId, model.TransactionDate));
                    }
                    if (LotNumberHelper.CheckSerialNumAvailability(model.LotNo, model.SerialNo).Any())
                    {
                        ViewData["EditError"] = "Serial Number must be unique";
                        return PartialView("createPartial", MiscellaneousTransactionHelper.GetMiscellaneousTransactionDetail(model.SOBId, model.TransactionType, model.CodeCombinationId, model.TransactionDate));
                    }
                    if (SessionHelper.MiscellaneousTransaction != null)
                    {
                        model.Id = SessionHelper.MiscellaneousTransaction.MiscellaneousTransactionDetail.Count() + 1;
                        validated = true;
                    }
                    else
                        model.Id = 1;

                    if (validated)
                    {
                        MiscellaneousTransactionHelper.InsertMiscellaneousTransactionDetail(model);
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("createPartial", MiscellaneousTransactionHelper.GetMiscellaneousTransactionDetail(model.SOBId, model.TransactionType, model.CodeCombinationId, model.TransactionDate));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdatePartial(MiscellaneousTransactionDetailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (SessionHelper.MiscellaneousTransaction != null)
                    {
                        if (SessionHelper.MiscellaneousTransaction.MiscellaneousTransactionDetail.Any(rec => rec.LotNo == model.LotNo && rec.ItemId == model.ItemId))
                        {
                            ViewData["EditError"] = "Lot Number must be unique";
                            return PartialView("createPartial", MiscellaneousTransactionHelper.GetMiscellaneousTransactionDetail(model.SOBId, model.TransactionType, model.CodeCombinationId, model.TransactionDate));
                        }
                        if (SessionHelper.MiscellaneousTransaction.MiscellaneousTransactionDetail.Any(rec => rec.LotNo == model.LotNo && rec.SerialNo == model.SerialNo))
                        {
                            ViewData["EditError"] = "Serial Number must be unique";
                            return PartialView("createPartial", MiscellaneousTransactionHelper.GetMiscellaneousTransactionDetail(model.SOBId, model.TransactionType, model.CodeCombinationId, model.TransactionDate));
                        }
                    }
                    if (LotNumberHelper.CheckLotNumAvailability(model.LotNo, model.ItemId, SessionHelper.SOBId).Any())
                    {
                        ViewData["EditError"] = "Lot Number must be unique";
                        return PartialView("createPartial", MiscellaneousTransactionHelper.GetMiscellaneousTransactionDetail(model.SOBId, model.TransactionType, model.CodeCombinationId, model.TransactionDate));
                    }
                    if (LotNumberHelper.CheckSerialNumAvailability(model.LotNo, model.SerialNo).Any())
                    {
                        ViewData["EditError"] = "Serial Number must be unique";
                        return PartialView("createPartial", MiscellaneousTransactionHelper.GetMiscellaneousTransactionDetail(model.SOBId, model.TransactionType, model.CodeCombinationId, model.TransactionDate));
                    }
                    MiscellaneousTransactionHelper.UpdateMiscellaneousTransactionDetail(model);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("createPartial", MiscellaneousTransactionHelper.GetMiscellaneousTransactionDetail(model.SOBId, model.TransactionType, model.CodeCombinationId, model.TransactionDate));
        }

        public ActionResult DeletePartial(MiscellaneousTransactionDetailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MiscellaneousTransactionModel MiscellaneousTransaction = SessionHelper.MiscellaneousTransaction;
                    MiscellaneousTransactionHelper.DeleteMiscellaneousTransactionDetail(model);
                    SessionHelper.MiscellaneousTransaction = MiscellaneousTransaction;
                    IList<MiscellaneousTransactionDetailModel> MiscellaneousTransactionDetail = MiscellaneousTransactionHelper.GetMiscellaneousTransactionDetail(model.SOBId, model.TransactionType, model.CodeCombinationId, model.TransactionDate);
                    return PartialView("createPartial", MiscellaneousTransactionDetail);
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

        public ActionResult Save(MiscellaneousTransactionModel model)
        {
            string message = "";
            try
            {
                bool saved = false;
                if (SessionHelper.MiscellaneousTransaction != null)
                {
                    SessionHelper.MiscellaneousTransaction.CodeCombinationId = model.CodeCombinationId;
                    SessionHelper.MiscellaneousTransaction.CompanyId = model.CompanyId;
                    SessionHelper.MiscellaneousTransaction.Id = model.Id;
                    SessionHelper.MiscellaneousTransaction.SOBId = model.SOBId;
                    SessionHelper.MiscellaneousTransaction.TransactionDate = model.TransactionDate;
                    SessionHelper.MiscellaneousTransaction.TransactionType = model.TransactionType;

                    //Setting the parent fields..
                    foreach (var item in SessionHelper.MiscellaneousTransaction.MiscellaneousTransactionDetail)
                    {
                        item.TransactionType = model.TransactionType;
                        item.CodeCombinationId = model.CodeCombinationId;
                        item.CompanyId = AuthenticationHelper.User.CompanyId;
                        item.SOBId = SessionHelper.SOBId;
                        item.TransactionDate = model.TransactionDate;
                    }

                    MiscellaneousTransactionHelper.Save(SessionHelper.MiscellaneousTransaction);
                    SessionHelper.MiscellaneousTransaction = null;
                    saved = true;

                }
                else
                    message = "No Miscellaneous Transaction information available!";
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
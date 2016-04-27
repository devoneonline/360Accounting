using _360Accounting.Common;
using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Controllers
{
    public class RemittanceController : BaseController
    {
        public ActionResult Delete(string remitNo)
        {
            try
            {
                RemittanceHelper.Delete(remitNo);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { message=ex.Message});
            }
        }

        public ActionResult Edit(string remitNo, long bankId, long bankAccountId)
        {
            RemittanceModel model = RemittanceHelper.GetRemittance(remitNo);
            SessionHelper.Bank = BankHelper.GetBank(bankId.ToString());
            SessionHelper.BankAccount = BankHelper.GetBankAccount(bankAccountId);

            model.Remittances = RemittanceHelper.GetRemittanceDetail(remitNo);
            model.SOBId = SessionHelper.SOBId;
            model.BankId = bankId;
            model.BankAccountId = bankAccountId;

            SessionHelper.DocumentDate = model.RemitDate;
            SessionHelper.Remittance = model;
            return View(model);
        }

        public ActionResult SaveRemittance(string remitDate)
        {
            string message = "";
            try
            {
                bool saved = false;
                if (SessionHelper.Remittance != null)
                {
                    SessionHelper.Remittance.RemitDate = Convert.ToDateTime(remitDate);
                    if (SessionHelper.Remittance.RemitNo == "New")
                    {
                        SessionHelper.Remittance.RemitNo = RemittanceHelper.GetRemitNo(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, SessionHelper.Bank.Id, SessionHelper.BankAccount.Id);
                    }

                    RemittanceHelper.Update(SessionHelper.Remittance);
                    SessionHelper.Remittance = null;
                    
                    saved = true;
                    message = "Saved successfully";
                }
                else
                    message = "No voucher information available!";
                return Json(new { success = saved, message = message });
            }
            catch (Exception e)
            {
                message = e.Message;
                return Json(new { success = false, message = message });
            }
        }

        public ActionResult DeletePartial(RemittanceDetailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    RemittanceModel remittance = SessionHelper.Remittance;
                    RemittanceHelper.DeleteRemittanceDetail(model);
                    SessionHelper.Remittance = remittance;
                    IList<RemittanceDetailModel> remittanceDetail = RemittanceHelper.GetRemittanceDetail();
                    return PartialView("_Detail", remittanceDetail);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_Detail");
            //return PartialView("_Detail", InvoiceHelper.GetInvoiceDetail());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdatePartial(RemittanceDetailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    RemittanceHelper.UpdateRemittanceDetail(model);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_Detail", RemittanceHelper.GetRemittanceDetail());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewPartial(RemittanceDetailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //bool validated = false;
                    //if (SessionHelper.Remittance != null)
                    //{
                    //    model.Id = SessionHelper.Remittance.Remittances.Last().Id + 1;
                    //    validated = true;
                    //}
                    //else
                    //    model.Id = 1;

                    //if (validated)
                    //    RemittanceHelper.Insert(model);

                    if (SessionHelper.Remittance != null)
                    {
                        if (SessionHelper.Remittance.Remittances != null && SessionHelper.Remittance.Remittances.Count() > 0)
                            model.Id = SessionHelper.Remittance.Remittances.LastOrDefault().Id + 1;
                        else
                            model.Id = 1;
                    }
                    else
                        model.Id = 1;

                    RemittanceHelper.Insert(model);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_Detail", RemittanceHelper.GetRemittanceDetail());
        }

        public ActionResult DetailPartial()
        {
            return PartialView("_Detail", RemittanceHelper.GetRemittanceDetail());
        }

        public JsonResult CheckDate(DateTime date)
        {
            bool result = true;

            if (SessionHelper.Bank != null)
            {
                if (date < SessionHelper.Bank.StartDate || date > SessionHelper.Bank.EndDate)
                    result = false;
            }

            if (SessionHelper.BankAccount != null)
            {
                if (date < SessionHelper.BankAccount.StartDate || date > SessionHelper.Bank.EndDate)
                    result = false;
            }

            if (SessionHelper.Receipts != null)
            {
                if (date != SessionHelper.Receipts.ReceiptDate)

                    result = false;
            }

            if (result)
                SessionHelper.DocumentDate = date;

            return Json(result);
        }

        public ActionResult Create(long bankId, long bankAccountId)
        {
            SessionHelper.Bank = BankHelper.GetBank(bankId.ToString());
            SessionHelper.BankAccount = BankHelper.GetBankAccount(bankAccountId);
            RemittanceModel model = SessionHelper.Remittance;
            
            if (model == null)
            {
                model = new RemittanceModel
                {
                    BankAccountId = bankAccountId,
                    BankId = bankId,
                    SOBId = SessionHelper.SOBId,
                    RemitNo = "New",
                    RemitDate = Const.CurrentDate,
                    Remittances = new List<RemittanceDetailModel>()
                };
                SessionHelper.DocumentDate = model.RemitDate;
                SessionHelper.Remittance = model;
            }
            return View("Edit", model);
        }

        public JsonResult BankList()
        {
            List<SelectListItem> bankList = BankHelper.GetBankList(SessionHelper.SOBId);
            return Json(bankList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BankAccountList(long bankId)
        {
            List<SelectListItem> bankAccountList = BankHelper.GetBankAccountList(bankId);
            return Json(bankAccountList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListPartial(long bankId, long bankAccountId)
        {
            return PartialView("_List", RemittanceHelper
                .GetRemittances(SessionHelper.SOBId, bankId, bankAccountId));
        }

        public ActionResult EmptyListPartial()
        {
            return PartialView("_List", new List<RemittanceModel>());
        }

        public ActionResult Index(RemittanceListModel model, string message="")
        {
            try
            {
                ViewBag.ErrorMessage = message;
                SessionHelper.Remittance = null;
                model.SOBId = SessionHelper.SOBId;

                if (model.Banks == null)
                {
                    model.Banks = BankHelper.GetBankList(SessionHelper.SOBId);
                    model.BankId = model.Banks.Any() ?
                        Convert.ToInt32(model.Banks.First().Value) : 0;
                }
                else
                    model.Banks = new List<SelectListItem>();

                if (model.BankAccounts == null && model.Banks.Any())
                {
                    model.BankAccounts = BankHelper.GetBankAccountList
                        (Convert.ToInt32(model.Banks.First().Value));
                    model.BankAccountId = model.BankAccounts.Any() ?
                        Convert.ToInt32(model.BankAccounts.First().Value) : 0;
                }
                else
                    model.BankAccounts = new List<SelectListItem>();

                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex);
                return View(model);
            }
        }
    }
}
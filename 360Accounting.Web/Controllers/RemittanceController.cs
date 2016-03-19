using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Controllers
{
    public class RemittanceController : Controller
    {
        public ActionResult Delete(string remitNo)
        {
            RemittanceHelper.Delete(remitNo);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string remitNo, long sobId, long bankId, long bankAccountId)
        {
            RemittanceModel model = RemittanceHelper.GetRemittance(remitNo);
            SessionHelper.SOBId = model.SOBId;
            SessionHelper.Bank = BankHelper.GetBank(bankId.ToString());
            SessionHelper.BankAccount = BankHelper.GetBankAccount(bankAccountId.ToString());

            model.Remittances = RemittanceHelper.GetRemittanceDetail(remitNo);
            model.SOBId = sobId;
            model.BankId = sobId;
            model.BankAccountId = bankAccountId;

            SessionHelper.Remittance = model;
            return View(model);
        }

        public ActionResult SaveInvoice(string remitDate)
        {
            string message = "";
            try
            {
                bool saved = false;
                if (SessionHelper.Remittance != null)
                {
                    SessionHelper.Remittance.RemitDate = Convert.ToDateTime(remitDate);
                    SessionHelper.Remittance.RemitNo = RemittanceHelper.GetRemitNo(AuthenticationHelper.User.CompanyId, SessionHelper.Invoice.SOBId, SessionHelper.Invoice.PeriodId, SessionHelper.Invoice.CurrencyId);

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
            return PartialView("_Detail", InvoiceHelper.GetInvoiceDetail());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewPartial(RemittanceDetailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool validated = false;
                    if (SessionHelper.Remittance != null)
                    {
                        model.Id = SessionHelper.Remittance.Remittances.Count() + 1;
                        validated = true;
                    }
                    else
                        model.Id = 1;

                    if (validated)
                        RemittanceHelper.Insert(model);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_Detail", InvoiceHelper.GetInvoiceDetail());
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

            if (SessionHelper.Receipt != null)
            {
                if (date != SessionHelper.Receipt.ReceiptDate)
                    result = false;
            }

            if (result)
                SessionHelper.DocumentDate = date;

            return Json(result);
        }

        public ActionResult Create(long sobId, long bankId, long bankAccountId)
        {
            SessionHelper.SOBId = sobId;
            SessionHelper.Bank = BankHelper.GetBank(bankId.ToString());
            SessionHelper.BankAccount = BankHelper.GetBankAccount(bankAccountId.ToString());
            RemittanceModel model = SessionHelper.Remittance;
            
            if (model == null)
            {
                List<SelectListItem> Receipts = ReceiptHelper
                    .GetReceiptList(bankId, bankAccountId);

                model = new RemittanceModel
                {
                    BankAccountId = bankAccountId,
                    BankId = bankId,
                    SOBId = sobId,
                    RemitNo = "New",
                    RemitDate = DateTime.Now
                };
                SessionHelper.Remittance = model;
            }
            return View("Edit", model);
        }

        public JsonResult BankList(long sobId)
        {
            List<SelectListItem> bankList = BankHelper.GetBankList(sobId);
            return Json(bankList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BankAccountList(long bankId)
        {
            List<SelectListItem> bankAccountList = BankHelper.GetBankAccountList(bankId);
            return Json(bankAccountList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListPartial(long sobId, long bankId, long bankAccountId)
        {
            SessionHelper.SOBId = sobId;
            return PartialView("_List", RemittanceHelper
                .GetRemittances(sobId, bankId, bankAccountId));
        }

        //Ye action mujhe pasand nai hai... pata nai q bana na par raha hai...{ HMUAUK }
        public ActionResult ListByModelPartial(RemittanceListModel model)
        {
            try
            {
                SessionHelper.SOBId = model.SOBId;
                return PartialView("_List", RemittanceHelper
                    .GetRemittances(model.SOBId, model.BankId,
                    model.BankAccountId));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex);
                return View(model);
            }
            
        }

        public ActionResult Index(RemittanceListModel model)
        {
            try
            {
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

                if (model.Banks == null && model.SetOfBooks.Any())
                {
                    model.Banks = BankHelper.GetBankList(Convert.ToInt32(model.SetOfBooks.First().Value));
                    model.BankId = model.Banks.Any() ?
                        Convert.ToInt32(model.Banks.First().Value) : 0;
                }

                if (model.BankAccounts == null && model.SetOfBooks.Any() && model.Banks.Any())
                {
                    model.BankAccounts = BankHelper.GetBankAccountList
                        (Convert.ToInt32(model.BankAccounts.First().Value));
                    model.BankAccountId = model.BankAccounts.Any() ?
                        Convert.ToInt32(model.BankAccounts.First().Value) : 0;
                }

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
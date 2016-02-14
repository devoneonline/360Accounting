using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Data.Repositories;
using _360Accounting.Service;
using DevEx_360Accounting_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using DevEx._360Accounting.Web.Reports;

namespace DevEx_360Accounting_Web.Controllers
{
    [Authorize]
    public class JournalVoucherController : Controller
    {
        private ICompanyService companyService;
        private IJournalVoucherService service;
        private ISetOfBookService sobService;
        private ICurrencyService currencyService;
        private ICalendarService calendarService;
        private ICodeCombinitionService codeCombinitionService;

        public JournalVoucherController()
        {
            companyService = IoC.Resolve<ICompanyService>("CompanyService");
            service = IoC.Resolve<IJournalVoucherService>("JournalVoucherService");
            sobService = IoC.Resolve<ISetOfBookService>("SetOfBookService");
            currencyService = IoC.Resolve<ICurrencyService>("CurrencyService");
            calendarService = IoC.Resolve<ICalendarService>("CalendarService");
            codeCombinitionService = IoC.Resolve<ICodeCombinitionService>("CodeCombinitionService");
        }

        #region Reports
        private UserwiseEntriesTrialReport CreateUserwiseEntriesTrialReport(long sobId, DateTime fromDate, DateTime toDate, Guid userId)
        {
            //Guid? userId = null;
            ////Get data in the model
            //if (username != null)
            //{
            //    userId = Guid.Parse(Membership.GetUser(username).ProviderUserKey.ToString());
            //}
            List<UserwiseEntriesTrialModel> modelList = mapReportModel(service.UserwiseEntriesTrial(AuthenticationHelper.User.CompanyId, sobId, fromDate, toDate, userId));
            UserwiseEntriesTrialReport report = new UserwiseEntriesTrialReport();
            report.DataSource = modelList;
            report.Parameters["CompanyName"].Value = companyService
                .GetSingle(AuthenticationHelper.User.CompanyId.ToString(),
                AuthenticationHelper.User.CompanyId);
            return report;
        }

        private List<UserwiseEntriesTrialModel> mapReportModel(List<UserwiseEntriesTrial> list)
        {
            List<UserwiseEntriesTrialModel> reportModel = new List<UserwiseEntriesTrialModel>();
            foreach (var record in list)
	        {
                reportModel.Add(new UserwiseEntriesTrialModel
                {
                    DocumentNo = record.DocumentNo,
                    EntryType = record.EntryType,
                    TransactionDate = record.TransactionDate,
                    UserId = record.UserId,
                    UserName = record.UserName
                });
	        }
            return reportModel;
        }

        public ActionResult UserwiseEntriesTrialPartial(long sobId, DateTime fromDate, DateTime toDate, Guid userId)
        {
            return PartialView("_UserwiseEntriesTrialPartial", CreateUserwiseEntriesTrialReport(sobId, fromDate, toDate, userId));
        }

        //[HttpPost]
        //public ActionResult UserwiseEntriesTrial(UserwiseEntriesTrialCriteriaModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        RedirectToAction("UserwiseEntriesTrialPartial", model);
        //    }
        //    return View(model);
        //}

        public ActionResult UserwiseEntriesTrial()
        {
            UserwiseEntriesTrialCriteriaModel model = new UserwiseEntriesTrialCriteriaModel();
            model.SetOfBooks = sobService.GetByCompanyId(AuthenticationHelper.User.CompanyId)
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();

            MembershipUserCollection memCollection = Membership.GetAllUsers();
            foreach (MembershipUser user in memCollection)
            {
                if (model.Users == null)
                {
                    model.Users = new List<SelectListItem>();
                }
                model.Users.Add(new SelectListItem
                {
                    Text = user.UserName,
                    Value = user.ProviderUserKey.ToString()
                });
            }

            return View(model);
        }
        #endregion

        public ActionResult GetJournalVoucherList(string sobId, string periodId, string currencyId)
        {
            JournalVoucherListModel model = new JournalVoucherListModel();
            model.SOBId = Convert.ToInt32(sobId);
            model.PeriodId = Convert.ToInt32(periodId);
            model.CurrencyId = Convert.ToInt32(currencyId);
            model.JournalVouchers = getJournalVouchers(model);
            return PartialView("_List", model);
        }

        [HttpPost]
        public ActionResult Edit(JournalVoucherCreateModel model, string submit)
        {
            if (ModelState.IsValid)
            {
                if (model.GLDate < SessionHelper.Calendar.StartDate || model.GLDate > SessionHelper.Calendar.EndDate)
                {
                    ModelState.AddModelError("Error", "Invalid Effective date");
                }
                else
                {
                    SessionHelper.JournalVoucher = mapModel(model, SessionHelper.JournalVoucher);

                    if (submit == "Save")
                    {
                        long result = saveJournalVoucher(SessionHelper.JournalVoucher);
                        if (result > 0)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("Error", "Unable to save!");
                        }
                    }
                    else
                    {
                        ////model.HeaderId = ?????,
                        ////model.Id = ????,
                        ////model.CodeCombinationId = ????;
                        model.GLLinesDescription = "";
                        model.EnteredDr = 0;
                        model.AccountedDr = 0;
                        model.EnteredCr = 0;
                        model.AccountedCr = 0;
                        model.Qty = 0;
                        model.TaxRateCode = 0;

                        return RedirectToAction("Edit", model);
                        //return View("Edit", model);
                    }
                }
            }
            return View(model);
        }

        public ActionResult Edit(JournalVoucherCreateModel model)
        {
            model.CodeCombinationList = codeCombinitionService.GetAll(AuthenticationHelper.User.CompanyId, model.SOBId, "", false, null, "", "")
                    .Select(x => new SelectListItem
                    {
                        Text = x.CodeCombinitionCode,
                        Value = x.Id.ToString()
                    }).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(JournalVoucherCreateModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.GLDate < SessionHelper.Calendar.StartDate || model.GLDate > SessionHelper.Calendar.EndDate)
                {
                    ModelState.AddModelError("Error", "Invalid Effective date");
                }
                else
                {
                    SessionHelper.JournalVoucher = mapModel(model, SessionHelper.JournalVoucher);

                    ////model.HeaderId = ?????,
                    ////model.Id = ????,
                    ////model.CodeCombinationId = ????;
                    model.GLLinesDescription = "";
                    model.EnteredDr = 0;
                    model.AccountedDr = 0;
                    model.EnteredCr = 0;
                    model.AccountedCr = 0;
                    model.Qty = 0;
                    model.TaxRateCode = 0;

                    return RedirectToAction("Edit", model);
                }
            }
            return View(model);
        }

        public ActionResult Create(long sobId, long periodId, long currencyId)
        {
            if (sobId > 0 && periodId > 0 && currencyId > 0)
            {
                JournalVoucherCreateModel model = new JournalVoucherCreateModel();
                model.SOBId = sobId;
                model.PeriodId = periodId;
                model.CurrencyId = currencyId;
                model.SOBName = sobService.GetSingle(sobId.ToString(), AuthenticationHelper.User.CompanyId).Name;

                SessionHelper.Calendar = new CalendarViewModel(calendarService.GetSingle(periodId.ToString(), AuthenticationHelper.User.CompanyId));
                model.GLDate = SessionHelper.Calendar.StartDate.Value;
                model.ConversionRate = 1;

                model.PeriodName = SessionHelper.Calendar.PeriodName;
                model.CurrencyName = currencyService.GetSingle(currencyId.ToString(), AuthenticationHelper.User.CompanyId).Name;
                model.CodeCombinationList = codeCombinitionService.GetAll(AuthenticationHelper.User.CompanyId, model.SOBId, "", false, null, "", "")
                    .Select(x => new SelectListItem
                    {
                        Text = x.CodeCombinitionCode,
                        Value = x.Id.ToString()
                    }).ToList();

                return View(model);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public JsonResult CurrencyList(string sobId)
        {
            return Json(getCurrencyList(sobId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult PeriodList(string sobId)
        {
            return Json(getPeriodList(sobId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index(JournalVoucherListModel model)
        {
            if (model.SetOfBooks == null)
            {
                model.SetOfBooks = sobService.GetByCompanyId(AuthenticationHelper.User.CompanyId)
                    .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                model.SOBId = model.SetOfBooks.Any() ? Convert.ToInt32(model.SetOfBooks.First().Value) : 0;
            }

            if (model.Periods == null && model.SetOfBooks.Any())
            {
                model.Periods = getPeriodList(model.SetOfBooks.First().Value);
                model.PeriodId = model.Periods.Any() ? Convert.ToInt32(model.Periods.First().Value) : 0;
            }

            if (model.Currencies == null && model.SetOfBooks.Any())
            {
                model.Currencies = getCurrencyList(model.SetOfBooks.First().Value);
                model.CurrencyId = model.Currencies.Any() ? Convert.ToInt32(model.Currencies.First().Value) : 0;
            }

            model.JournalVouchers = getJournalVouchers(model);
            
            return View(model);
        }

        #region Private Methods
        private JournalVoucherViewModel mapModel(JournalVoucherCreateModel model, JournalVoucherViewModel jv)
        {
            jv.CompanyId = AuthenticationHelper.User.CompanyId;
            jv.ConversionRate = model.ConversionRate;
            jv.CurrencyId = model.CurrencyId;
            jv.Description = model.Description;
            jv.DocumentNo = model.DocumentNo;
            jv.GLDate = model.GLDate;
            jv.Id = model.HeaderId;
            jv.JournalName = model.JournalName;
            if (jv.JournalVoucherDetail == null)
            {
                jv.JournalVoucherDetail = new List<JournalVoucherDetailModel>();
            }
            jv.JournalVoucherDetail.Add(new JournalVoucherDetailModel
                {
                    AccountedCr = model.AccountedCr,
                    AccountedDr = model.AccountedDr,
                    CodeCombinationId = model.CodeCombinationId,
                    Description = model.GLLinesDescription,
                    EnteredCr = model.EnteredCr,
                    EnteredDr = model.EnteredDr,
                    HeaderId = model.HeaderId,
                    Id = model.Id,
                    Qty = model.Qty,
                    TaxRateCode = model.TaxRateCode
                });

            jv.PeriodId = model.PeriodId;
            jv.SOBId = model.SOBId;
            return jv;
        }

        private long saveJournalVoucher(JournalVoucherViewModel model)
        {
            JournalVoucher entity = new JournalVoucher();
            entity.CompanyId = AuthenticationHelper.User.CompanyId;
            entity.ConversionRate = model.ConversionRate;
            entity.CreateDate = DateTime.Now;
            entity.CurrencyId = model.CurrencyId;
            entity.Description = model.Description;
            entity.DocumentNo = model.DocumentNo;
            entity.GLDate = model.GLDate;
            entity.Id = model.Id;
            entity.JournalName = model.JournalName;
            entity.PeriodId = model.PeriodId;
            //entity.PostingFlag = codeCombinitionService.GetSingle(model.CodeCombinationId.ToString(), AuthenticationHelper.User.CompanyId).AllowedPosting;
            entity.PostingFlag = true;
            entity.SOBId = model.SOBId;
            entity.UpdateDate = DateTime.Now;

            if (entity.Id == 0)
            {
                entity.Id = Convert.ToInt32(service.Insert(entity));
            }
            else
            {
                entity.Id = Convert.ToInt32(service.Update(entity));
            }

            if (model.JournalVoucherDetail.Any())
            {
                foreach (var detail in model.JournalVoucherDetail)
                {
                    detail.HeaderId = entity.Id;

                    JournalVoucherDetail entityDetail = new JournalVoucherDetail();
                    entityDetail.AccountedCr = detail.AccountedCr;
                    entityDetail.AccountedDr = detail.AccountedDr;
                    entityDetail.CodeCombinationId = detail.CodeCombinationId;
                    entityDetail.CreateDate = DateTime.Now;
                    entityDetail.Description = detail.Description;
                    entityDetail.EnteredCr = detail.EnteredCr;
                    entityDetail.EnteredDr = detail.EnteredDr;
                    entityDetail.HeaderId = detail.HeaderId;
                    entityDetail.Id = detail.Id;
                    entityDetail.Qty = detail.Qty;
                    entityDetail.TaxRateCode = detail.TaxRateCode;
                    entityDetail.UpdateDate = DateTime.Now;

                    if (entityDetail.Id == 0)
                    {
                        entityDetail.Id = Convert.ToInt32(service.Insert(entityDetail));
                    }
                    else
                    {
                        entityDetail.Id = Convert.ToInt32(service.Update(entityDetail));
                    }
                }
            }

            return entity.Id;
        }

        private List<JournalVoucherViewModel> getJournalVouchers(JournalVoucherListModel model)
        {
            List<JournalVoucherViewModel> list = service.GetAll(AuthenticationHelper.User.CompanyId, model.SearchText, true, model.Page, model.SortColumn, model.SortDirection)
                .Select(x => new JournalVoucherViewModel(x)).ToList();
            return list;       
        }

        private List<SelectListItem> getPeriodList(string sobId)
        {
            List<SelectListItem> list = calendarService.GetAll(AuthenticationHelper.User.CompanyId, Convert.ToInt32(sobId))
                    .Select(x => new SelectListItem { Text = x.PeriodName, Value = x.Id.ToString() }).ToList();
            return list;
        }

        private List<SelectListItem> getCurrencyList(string sobId)
        {
            List<SelectListItem> list = currencyService.GetAll(AuthenticationHelper.User.CompanyId, Convert.ToInt32(sobId))
                    .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            return list;
                
        }
        #endregion

        public ActionResult JournalVoucherPartial()
        {
            IEnumerable<JournalVoucherViewModel> list = service.GetAll(AuthenticationHelper.User.CompanyId, "", true, null, "", "")
                .Select(x => new JournalVoucherViewModel(x));
            
            return PartialView("_List");
        }
    }
}
using _360Accounting.Common;
using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Data.Repositories;
using _360Accounting.Service;
using _360Accounting.Web.Models;
using _360Accounting.Web.Mvc;
using _360Accounting.Web.Reports;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace _360Accounting.Web.Controllers
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
        public ActionResult TrialBalancePartialExport(long sobId, long fromCodeCombinationId, long toCodeCombinationId, long periodId)
        {
            return DocumentViewerExtension.ExportTo(CreateTrialBalanceReport(sobId, fromCodeCombinationId, toCodeCombinationId, periodId));
        }

        public ActionResult LedgerPartialExport(long sobId, long fromCodeCombinationId, long toCodeCombinationId, DateTime fromDate, DateTime toDate)
        {
            return DocumentViewerExtension.ExportTo(CreateLedgerReport(sobId, fromCodeCombinationId, toCodeCombinationId, fromDate, toDate));
        }

        public ActionResult AuditTrailPartialExport(long sobId, DateTime fromDate, DateTime toDate)
        {
            return DocumentViewerExtension.ExportTo(CreateAuditTrailReport(sobId, fromDate, toDate), Request);
        }

        public ActionResult UserwiseEntriesTrailPartialExport(long sobId, DateTime fromDate, DateTime toDate, Guid userId)
        {
            return DocumentViewerExtension.ExportTo(CreateUserwiseEntriesTrailReport(sobId, fromDate, toDate, userId), Request);
        }

        private TrialBalanceReport CreateTrialBalanceReport(long sobId, long fromCodeCombinationId, long toCodeCombinationId, long periodId)
        {
            List<TrialBalanceModel> modelList = mapTrialBalanceModel(service.TrialBalance(AuthenticationHelper.User.CompanyId, sobId, fromCodeCombinationId >= toCodeCombinationId ? toCodeCombinationId : fromCodeCombinationId, toCodeCombinationId <= fromCodeCombinationId ? fromCodeCombinationId : toCodeCombinationId, periodId));
            TrialBalanceReport report = new TrialBalanceReport();
            report.Parameters["CompanyName"].Value = companyService
                .GetSingle(AuthenticationHelper.User.CompanyId.ToString(),
                AuthenticationHelper.User.CompanyId).Name;
            report.Parameters["SOBId"].Value = sobId;
            report.Parameters["FromCodeCombinationId"].Value = fromCodeCombinationId;
            report.Parameters["ToCodeCombinationId"].Value = toCodeCombinationId;
            report.Parameters["PeriodId"].Value = periodId;
            report.DataSource = modelList;
            return report;
        }

        private List<TrialBalanceModel> mapTrialBalanceModel(List<TrialBalance> list)
        {
            List<TrialBalanceModel> reportModel = new List<TrialBalanceModel>();
            foreach (var record in list)
            {
                reportModel.Add(new TrialBalanceModel
                {
                    CodeCombination = record.CodeCombination,
                    CodeCombinationName = record.CodeCombinationName,
                    Credit = record.Credit,
                    Debit = record.Debit,
                });
            }

            return reportModel;
        }

        private LedgerReport CreateLedgerReport(long sobId, long fromCodeCombinationId, long toCodeCombinationId, DateTime fromDate, DateTime toDate)
        {
            List<LedgerModel> modelList = mapLedgerModel(service.Ledger(AuthenticationHelper.User.CompanyId, sobId, fromCodeCombinationId >= toCodeCombinationId ? toCodeCombinationId : fromCodeCombinationId, toCodeCombinationId <= fromCodeCombinationId ? fromCodeCombinationId : toCodeCombinationId, fromDate, toDate));
            LedgerReport report = new LedgerReport();
            report.Parameters["CompanyName"].Value = companyService
                .GetSingle(AuthenticationHelper.User.CompanyId.ToString(),
                AuthenticationHelper.User.CompanyId).Name;
            report.Parameters["SOBId"].Value = sobId;
            report.Parameters["FromDate"].Value = fromDate;
            report.Parameters["ToDate"].Value = toDate;
            report.Parameters["FromCodeCombinationId"].Value = fromCodeCombinationId;
            report.Parameters["ToCodeCombinationId"].Value = toCodeCombinationId;
            report.DataSource = modelList;
            return report;
        }

        private List<LedgerModel> mapLedgerModel(List<Ledger> list)
        {
            List<LedgerModel> reportModel = new List<LedgerModel>();
            foreach (var record in list)
            {
                reportModel.Add(new LedgerModel
                {
                    Balance = record.Balance,
                    Credit = record.Credit,
                    Debit = record.Debit,
                    Description = record.Description,
                    Document = record.Document,
                    TransactionDate = record.TransactionDate
                });
            }

            return reportModel;
        }

        private AuditTrailReport CreateAuditTrailReport(long sobId, DateTime fromDate, DateTime toDate)
        {
            List<AuditTrailModel> modelList = mapAuditTrialModel(service.AuditTrail(AuthenticationHelper.User.CompanyId, sobId, fromDate, toDate));
            AuditTrailReport report = new AuditTrailReport();
            report.Parameters["CompanyName"].Value = companyService
                .GetSingle(AuthenticationHelper.User.CompanyId.ToString(),
                AuthenticationHelper.User.CompanyId).Name;
            report.Parameters["SOBId"].Value = sobId;
            report.Parameters["FromDate"].Value = fromDate;
            report.Parameters["ToDate"].Value = toDate;
            report.DataSource = modelList;
            return report;
        }

        private List<AuditTrailModel> mapAuditTrialModel(List<AuditTrail> list)
        {
            List<AuditTrailModel> reportModel = new List<AuditTrailModel>();
            foreach (var record in list)
            {
                reportModel.Add(new AuditTrailModel
                {
                    CodeCombination = Utility.Stringize(".", record.CCSegment1,
                    record.CCSegment2, record.CCSegment3, record.CCSegment4,
                    record.CCSegment5, record.CCSegment6, record.CCSegment7,
                    record.CCSegment8),
                    ConversionRate = record.ConversionRate,
                    Credit = record.Credit,
                    CurrencyName = record.CurrencyName,
                    Debit = record.Debit,
                    Description = record.Description,
                    Document = record.Document,
                    LineDescription = record.LineDescription,
                    PeriodName = record.PeriodName,
                    TransactionDate = record.TransactionDate
                });
            }

            return reportModel;
        }

        private UserwiseEntriesTrailReport CreateUserwiseEntriesTrailReport(long sobId, DateTime fromDate, DateTime toDate, Guid userId)
        {
            List<UserwiseEntriesTrailModel> modelList = mapReportModel(service.UserwiseEntriesTrail(AuthenticationHelper.User.CompanyId, sobId, fromDate, toDate, userId));
            UserwiseEntriesTrailReport report = new UserwiseEntriesTrailReport();
            report.Parameters["CompanyName"].Value = companyService
                .GetSingle(AuthenticationHelper.User.CompanyId.ToString(),
                AuthenticationHelper.User.CompanyId).Name;
            report.Parameters["SOBId"].Value = sobId;
            report.Parameters["FromDate"].Value = fromDate;
            report.Parameters["ToDate"].Value = toDate;
            report.Parameters["UserId"].Value = userId;
            report.DataSource = modelList;
            return report;
        }

        private List<UserwiseEntriesTrailModel> mapReportModel(List<UserwiseEntriesTrail> list)
        {
            List<UserwiseEntriesTrailModel> reportModel = new List<UserwiseEntriesTrailModel>();
            foreach (var record in list)
            {
                reportModel.Add(new UserwiseEntriesTrailModel
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

        public ActionResult TrialBalancePartial(long sobId, long fromCodeCombinationId, long toCodeCombinationId, long periodId)
        {
            return PartialView("_TrialBalancePartial", CreateTrialBalanceReport(sobId, fromCodeCombinationId, toCodeCombinationId, periodId));
        }

        public ActionResult LedgerPartial(long sobId, long fromCodeCombinationId, long toCodeCombinationId, DateTime fromDate, DateTime toDate)
        {
            return PartialView("_LedgerPartial", CreateLedgerReport(sobId, fromCodeCombinationId, toCodeCombinationId, fromDate, toDate));
        }

        public ActionResult AuditTrailPartial(long sobId, DateTime fromDate, DateTime toDate)
        {
            return PartialView("_AuditTrailPartial", CreateAuditTrailReport(sobId, fromDate, toDate));
        }

        public ActionResult UserwiseEntriesTrailPartial(long sobId, DateTime fromDate, DateTime toDate, Guid userId)
        {
            return PartialView("_UserwiseEntriesTrailPartial", CreateUserwiseEntriesTrailReport(sobId, fromDate, toDate, userId));
        }

        public ActionResult TrialBalanceReport(long sobId, long fromCodeCombinationId, long toCodeCombinationId, long periodId)
        {
            return View(CreateTrialBalanceReport(sobId, fromCodeCombinationId, toCodeCombinationId, periodId));
        }

        public ActionResult LedgerReport(long sobId, long fromCodeCombinationId, long toCodeCombinationId, DateTime fromDate, DateTime toDate)
        {
            return View(CreateLedgerReport(sobId, fromCodeCombinationId, toCodeCombinationId, fromDate, toDate));
        }

        public ActionResult AuditTrailReport(long sobId, DateTime fromDate, DateTime toDate)
        {
            return View(CreateAuditTrailReport(sobId, fromDate, toDate));
        }

        public ActionResult UserwiseEntriesTrailReport(long sobId, DateTime fromDate, DateTime toDate, Guid userId)
        {
            return View(CreateUserwiseEntriesTrailReport(sobId, fromDate, toDate, userId));
        }

        public JsonResult CodeCombinationList(long sobId)
        {
            return Json(getCodeCombinationList(sobId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult TrialBalance()
        {
            TrialBalanceCriteriaModel model = new TrialBalanceCriteriaModel();
            model.SetOfBooks = sobService.GetByCompanyId(AuthenticationHelper.User.CompanyId)
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
            model.CodeCombinations = getCodeCombinationList(Convert.ToInt32(model.SetOfBooks.First().Value));
            model.Periods = getPeriodList(model.SetOfBooks.First().Value);
            return View(model);
        }

        public ActionResult Ledger()
        {
            LedgerCriteriaModel model = new LedgerCriteriaModel();
            model.SetOfBooks = sobService.GetByCompanyId(AuthenticationHelper.User.CompanyId)
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();

            model.CodeCombinations = getCodeCombinationList(Convert.ToInt32(model.SetOfBooks.First().Value));
            return View(model);
        }

        public ActionResult AuditTrail()
        {
            AuditTrailCriteriaModel model = new AuditTrailCriteriaModel();
            model.SetOfBooks = sobService.GetByCompanyId(AuthenticationHelper.User.CompanyId)
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
            return View(model);
        }

        public ActionResult UserwiseEntriesTrail()
        {
            UserwiseEntriesTrailCriteriaModel model = new UserwiseEntriesTrailCriteriaModel();
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
                model.GLDate = SessionHelper.Calendar.StartDate;
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

        public ActionResult JournalVoucherPartial()
        {
            IEnumerable<JournalVoucherViewModel> list = service.GetAll(AuthenticationHelper.User.CompanyId, "", true, null, "", "")
                .Select(x => new JournalVoucherViewModel(x));

            return PartialView("_List");
        }
    }
}
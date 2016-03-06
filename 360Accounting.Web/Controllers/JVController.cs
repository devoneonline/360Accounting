using _360Accounting.Core;
using _360Accounting.Web.Mvc;
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

namespace _360Accounting.Web.Controllers
{
    public class JVController : Controller
    {
        //private IGLHeaderService service;
        //private IGLLineService lineService;
        private ISetOfBookService sobService;
        private ICalendarService calendarService;
        private ICurrencyService currencyService;
        private ICompanyService companyService;
        private IGLHeaderService service;     //for reports of jv
        private ICodeCombinitionService codeCombinitionService;

        public JVController()
        {
            sobService = IoC.Resolve<ISetOfBookService>("SetOfBookService");
            calendarService = IoC.Resolve<ICalendarService>("CalendarService");
            currencyService = IoC.Resolve<ICurrencyService>("CurrencyService");
            companyService = IoC.Resolve<ICompanyService>("CompanyService");
            service = IoC.Resolve<IGLHeaderService>("GLHeaderService");
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
            model.FromDate = Const.FromDate;
            model.ToDate = Const.ToDate;
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
            model.FromDate = Const.FromDate;
            model.ToDate = Const.ToDate;
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

            model.FromDate = Const.FromDate;
            model.ToDate = Const.ToDate;

            return View(model);
        }
        #endregion

        public ActionResult Delete(string id)
        {
            DataProvider.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            GLHeaderModel model = DataProvider.GetGLHeaders(id);
            SessionHelper.SOBId = model.SOBId;
            model.GlLines = DataProvider.GetGLLines(id);
            SessionHelper.JV = model;
            return View("Create", model);
        }

        public ActionResult Index(JournalVoucherListModel model)
        {
            SessionHelper.JV = null;
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
            return View(model);
        }

        public ActionResult JVPartialWithModel(JournalVoucherListModel model)
        {
            SessionHelper.SOBId = model.SOBId;
            return PartialView("_List", DataProvider
                .GetGLHeaders(model.SOBId, model.PeriodId,
                model.CurrencyId));
        }

        public ActionResult JVPartial(long sobId, long periodId, long currencyId)
        {
            SessionHelper.SOBId = sobId;
            return PartialView("_List", DataProvider
                .GetGLHeaders(sobId, periodId, currencyId));
        }

        public ActionResult Create(long sobId, long periodId, long currencyId)
        {
            SessionHelper.SOBId = sobId;

            GLHeaderModel model = SessionHelper.JV;
            if (model == null)
            {
                model = new GLHeaderModel();
                model.CompanyId = AuthenticationHelper.User.CompanyId;
                model.SOBId = sobId;
                model.PeriodId = periodId;
                model.CurrencyId = currencyId;
                model.GlLines = DataProvider.GetGLLines();
                SessionHelper.JV = model;
                model.DocumentNo = DataProvider.GetDocNo(AuthenticationHelper.User.CompanyId, periodId, sobId, currencyId);
            }
            return View(model);
        }

        public ActionResult CreatePartial()
        {
            return PartialView("createPartial", DataProvider.GetGLLines());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewPartial(GLLinesModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    DataProvider.Insert(model);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("createPartial", DataProvider.GetGLLines());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdatePartial(GLLinesModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    DataProvider.UpdateGLLine(model);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("createPartial", DataProvider.GetGLLines());
        }

        public ActionResult DeletePartial(GLLinesModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    GLHeaderModel header = SessionHelper.JV;
                    header.GlLines.Remove(model);
                    SessionHelper.JV = header;
                    IList<GLLinesModel> glLines = DataProvider.GetGLLines();
                    return PartialView("createPartial", glLines);
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
        
        public ActionResult SaveVoucher(string journalName, string glDate, string cRate, string descr)
        {
            try
            {
                DataProvider.Update(journalName, glDate, cRate, descr);
                SessionHelper.JV = null;
                return Json("Success");
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
                return Json("Failure");
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
    }
}
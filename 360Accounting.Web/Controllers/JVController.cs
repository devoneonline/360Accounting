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

namespace _360Accounting.Web.Controllers
{
    [Authorize]
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
            List<SelectListItem> list = codeCombinitionService.GetAll(AuthenticationHelper.CompanyId.Value, sobId)
                .Select(x => new SelectListItem
                {
                    Text = Utility.Stringize(".", x.Segment1, x.Segment2, x.Segment3, x.Segment4, x.Segment5, x.Segment6, x.Segment7, x.Segment8),
                    Value = x.Id.ToString()
                }).ToList();
            return list;
        }

        //private List<SelectListItem> getCurrencyList(string sobId)
        //{
        //    List<SelectListItem> list = currencyService.GetAll(AuthenticationHelper.User.CompanyId, Convert.ToInt32(sobId))
        //            .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
        //    return list;

        //}
        #endregion

        #region Reports
        public ActionResult TrialBalancePartialExport(long fromCodeCombinationId, long toCodeCombinationId, long periodId)
        {
            return DocumentViewerExtension.ExportTo(CreateTrialBalanceReport(fromCodeCombinationId, toCodeCombinationId, periodId));
        }

        public ActionResult LedgerPartialExport(long fromCodeCombinationId, long toCodeCombinationId, DateTime fromDate, DateTime toDate)
        {
            return DocumentViewerExtension.ExportTo(CreateLedgerReport(fromCodeCombinationId, toCodeCombinationId, fromDate, toDate));
        }

        public ActionResult AuditTrailPartialExport(DateTime fromDate, DateTime toDate)
        {
            return DocumentViewerExtension.ExportTo(CreateAuditTrailReport(fromDate, toDate), Request);
        }

        public ActionResult UserwiseEntriesTrailPartialExport(DateTime fromDate, DateTime toDate, Guid userId)
        {
            return DocumentViewerExtension.ExportTo(CreateUserwiseEntriesTrailReport(fromDate, toDate, userId), Request);
        }

        private TrialBalanceReport CreateTrialBalanceReport(long fromCodeCombinationId, long toCodeCombinationId, long periodId)
        {
            List<TrialBalanceModel> modelList = mapTrialBalanceModel(service.TrialBalance(AuthenticationHelper.User.CompanyId, SessionHelper.SOBId, fromCodeCombinationId >= toCodeCombinationId ? toCodeCombinationId : fromCodeCombinationId, toCodeCombinationId <= fromCodeCombinationId ? fromCodeCombinationId : toCodeCombinationId, periodId));
            TrialBalanceReport report = new TrialBalanceReport();
            report.Parameters["CompanyName"].Value = companyService
                .GetSingle(AuthenticationHelper.CompanyId.Value.ToString(),
                AuthenticationHelper.CompanyId.Value).Name;
            report.Parameters["SOBId"].Value = SessionHelper.SOBId;
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

        private LedgerReport CreateLedgerReport(long fromCodeCombinationId, long toCodeCombinationId, DateTime fromDate, DateTime toDate)
        {
            List<LedgerModel> modelList = mapLedgerModel(service.Ledger(AuthenticationHelper.User.CompanyId, SessionHelper.SOBId, fromCodeCombinationId >= toCodeCombinationId ? toCodeCombinationId : fromCodeCombinationId, toCodeCombinationId <= fromCodeCombinationId ? fromCodeCombinationId : toCodeCombinationId, fromDate, toDate));
            LedgerReport report = new LedgerReport();
            report.Parameters["CompanyName"].Value = companyService
                .GetSingle(AuthenticationHelper.CompanyId.Value.ToString(),
                AuthenticationHelper.CompanyId.Value).Name;
            report.Parameters["SOBId"].Value = SessionHelper.SOBId;
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

        private AuditTrailReport CreateAuditTrailReport(DateTime fromDate, DateTime toDate)
        {
            List<AuditTrailModel> modelList = mapAuditTrialModel(service.AuditTrail(AuthenticationHelper.User.CompanyId, SessionHelper.SOBId, fromDate, toDate));
            AuditTrailReport report = new AuditTrailReport();
            report.Parameters["CompanyName"].Value = companyService
                .GetSingle(AuthenticationHelper.CompanyId.Value.ToString(),
                AuthenticationHelper.CompanyId.Value).Name;
            report.Parameters["SOBId"].Value = SessionHelper.SOBId;
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

        private UserwiseEntriesTrailReport CreateUserwiseEntriesTrailReport(DateTime fromDate, DateTime toDate, Guid userId)
        {
            List<UserwiseEntriesTrailModel> modelList = mapReportModel(service.UserwiseEntriesTrail(AuthenticationHelper.User.CompanyId, SessionHelper.SOBId, fromDate, toDate, userId));
            UserwiseEntriesTrailReport report = new UserwiseEntriesTrailReport();
            report.Parameters["CompanyName"].Value = companyService
                .GetSingle(AuthenticationHelper.CompanyId.Value.ToString(),
                AuthenticationHelper.CompanyId.Value).Name;
            report.Parameters["SOBId"].Value = SessionHelper.SOBId;
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

        public ActionResult TrialBalancePartial(long fromCodeCombinationId, long toCodeCombinationId, long periodId)
        {
            return PartialView("_TrialBalancePartial", CreateTrialBalanceReport(fromCodeCombinationId, toCodeCombinationId, periodId));
        }

        public ActionResult LedgerPartial(long fromCodeCombinationId, long toCodeCombinationId, DateTime fromDate, DateTime toDate)
        {
            return PartialView("_LedgerPartial", CreateLedgerReport(fromCodeCombinationId, toCodeCombinationId, fromDate, toDate));
        }

        public ActionResult AuditTrailPartial(DateTime fromDate, DateTime toDate)
        {
            return PartialView("_AuditTrailPartial", CreateAuditTrailReport(fromDate, toDate));
        }

        public ActionResult UserwiseEntriesTrailPartial(DateTime fromDate, DateTime toDate, Guid userId)
        {
            return PartialView("_UserwiseEntriesTrailPartial", CreateUserwiseEntriesTrailReport(fromDate, toDate, userId));
        }

        public ActionResult TrialBalanceReport(long fromCodeCombinationId, long toCodeCombinationId, long periodId)
        {
            return View(CreateTrialBalanceReport(fromCodeCombinationId, toCodeCombinationId, periodId));
        }

        public ActionResult LedgerReport(long fromCodeCombinationId, long toCodeCombinationId, DateTime fromDate, DateTime toDate)
        {
            return View(CreateLedgerReport(fromCodeCombinationId, toCodeCombinationId, fromDate, toDate));
        }

        public ActionResult AuditTrailReport(DateTime fromDate, DateTime toDate)
        {
            return View(CreateAuditTrailReport(fromDate, toDate));
        }

        public ActionResult UserwiseEntriesTrailReport(DateTime fromDate, DateTime toDate, Guid userId)
        {
            return View(CreateUserwiseEntriesTrailReport(fromDate, toDate, userId));
        }

        public JsonResult CodeCombinationList()
        {
            return Json(getCodeCombinationList(SessionHelper.SOBId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult TrialBalance()
        {
            TrialBalanceCriteriaModel model = new TrialBalanceCriteriaModel();
            model.CodeCombinations = getCodeCombinationList(Convert.ToInt32(SessionHelper.SOBId));
            //model.Periods = getPeriodList(model.SetOfBooks.First().Value);

            model.Periods = CalendarHelper.GetCalendars(Convert.ToInt32(SessionHelper.SOBId))
                    .Select(x => new SelectListItem
                    {
                        Text = x.PeriodName,
                        Value = x.Id.ToString()
                    }).ToList();

            return View(model);
        }

        public ActionResult Ledger()
        {
            LedgerCriteriaModel model = new LedgerCriteriaModel();

            model.CodeCombinations = getCodeCombinationList(Convert.ToInt32(SessionHelper.SOBId));
            model.FromDate = Const.StartDate;
            model.ToDate = Const.EndDate;
            return View(model);
        }

        public ActionResult AuditTrail()
        {
            AuditTrailCriteriaModel model = new AuditTrailCriteriaModel();
            
            model.FromDate = Const.StartDate;
            model.ToDate = Const.EndDate;
            return View(model);
        }

        public ActionResult UserwiseEntriesTrail()
        {
            UserwiseEntriesTrailCriteriaModel model = new UserwiseEntriesTrailCriteriaModel();
           
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

            model.FromDate = Const.StartDate;
            model.ToDate = Const.EndDate;

            return View(model);
        }
        #endregion

        public ActionResult Delete(string id)
        {
            JVHelper.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id, long currencyId, long periodId)
        {
            GLHeaderModel model = JVHelper.GetGLHeaders(id);
            SessionHelper.Calendar = new CalendarViewModel(calendarService.GetSingle(periodId.ToString(), AuthenticationHelper.CompanyId.Value));
            SessionHelper.PrecisionLimit = currencyService.GetSingle(currencyId.ToString(), AuthenticationHelper.CompanyId.Value).Precision;

            ViewBag.SOBName = SetOfBookHelper.GetSetOfBook(SessionHelper.SOBId.ToString()).Name;
            ViewBag.PeriodName = SessionHelper.Calendar.PeriodName;
            ViewBag.CurrencyName = CurrencyHelper.GetCurrency(currencyId.ToString()).Name;

            model.GlLines = JVHelper.GetGLLines(id);
            model.CurrencyId = currencyId;
            model.SOBId = SessionHelper.SOBId;
            model.PeriodId = periodId;
            SessionHelper.JV = model;
            return View("Create", model);
        }

        public ActionResult Index(JournalVoucherListModel model)
        {
            SessionHelper.JV = null;
            model.SOBId = SessionHelper.SOBId;

            if (model.Periods == null)
            {
                model.Periods = CalendarHelper.GetCalendars(SessionHelper.SOBId)
                    .Select(x => new SelectListItem
                    {
                        Text = x.PeriodName,
                        Value = x.Id.ToString()
                    }).ToList();
                model.PeriodId = model.Periods.Any() ? Convert.ToInt32(model.Periods.First().Value) : 0;
            }
            else if (model.Periods == null)
            {
                model.Periods = new List<SelectListItem>();
            }

            if (model.Currencies == null)
            {
                model.Currencies = CurrencyHelper.GetCurrencies(SessionHelper.SOBId)
                    .Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();
                model.CurrencyId = model.Currencies.Any() ? Convert.ToInt32(model.Currencies.First().Value) : 0;                
            }
            else if (model.Currencies == null)
            {
                model.Currencies = new List<SelectListItem>();
            }

            return View(model);
        }

        public ActionResult ListPartial(long periodId, long currencyId)
        {
            return PartialView("_List", JVHelper
                .GetGLHeaders(SessionHelper.SOBId, periodId, currencyId));
        }

        public ActionResult EmptyListPartial()
        {
            return PartialView("_List", new List<GLHeaderModel>());
        }

        public ActionResult Create(long periodId, long currencyId)
        {
            SessionHelper.Calendar = CalendarHelper.GetCalendar(periodId.ToString());
            SessionHelper.PrecisionLimit = CurrencyHelper.GetCurrency(currencyId.ToString()).Precision;

            ViewBag.SOBName = SetOfBookHelper.GetSetOfBook(SessionHelper.SOBId.ToString()).Name;
            ViewBag.PeriodName = SessionHelper.Calendar.PeriodName;
            ViewBag.CurrencyName = CurrencyHelper.GetCurrency(currencyId.ToString()).Name;

            GLHeaderModel model = SessionHelper.JV;
            if (model == null)
            {
                model = new GLHeaderModel
                {
                    CompanyId = AuthenticationHelper.CompanyId.Value,
                    SOBId = SessionHelper.SOBId,
                    PeriodId = periodId,
                    CurrencyId = currencyId,
                    GlLines = new List<GLLinesModel>(),
                    DocumentNo = "New",
                    GLDate = SessionHelper.Calendar.StartDate,
                    ConversionRate = 1
                };
                SessionHelper.JV = model;
            }
            return View(model);
        }

        public JsonResult CheckGLDate(DateTime glDate, long periodId)
        {
            bool returnData = true;
            if (periodId > 0)
            {
                if (SessionHelper.Calendar != null)
                {
                    if (glDate < SessionHelper.Calendar.StartDate || glDate > SessionHelper.Calendar.EndDate)
                        returnData = false;
                    else
                        returnData = true;
                }
            }
            return Json(returnData);
        }

        public ActionResult CreatePartial()
        {
            return PartialView("createPartial", JVHelper.GetGLLines());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewPartial(GLLinesModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool validated = false;
                    if (model.EnteredCr > 0 && model.EnteredDr > 0)
                    {
                        ViewData["EditError"] = "Both debit and credit can not be entered in one entry.";
                        return PartialView("createPartial", JVHelper.GetGLLines());
                    }
                    if (SessionHelper.JV.GlLines.Count != 0)
                    {
                        if (SessionHelper.JV.GlLines.Any(rec => rec.CodeCombinationId == model.CodeCombinationId))
                            ViewData["EditError"] = "Duplicate accounts can not be added.";
                        else
                        {
                            validated = true;
                            model.Id = SessionHelper.JV.GlLines.Last().Id + 1;
                        }
                    }
                    else
                    {
                        model.Id = 1; 
                        validated = true;
                    }                        

                    if (validated)
                        JVHelper.Insert(model);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("createPartial", JVHelper.GetGLLines());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdatePartial(GLLinesModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    JVHelper.UpdateGLLine(model);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("createPartial", JVHelper.GetGLLines());
        }

        public ActionResult DeletePartial(GLLinesModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    GLHeaderModel header = SessionHelper.JV;
                    JVHelper.DeleteGLLine(model);
                    SessionHelper.JV = header;
                    IList<GLLinesModel> glLines = JVHelper.GetGLLines();
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
            string message = "";
            try
            {
                bool saved = false;
                if (SessionHelper.JV != null)
                {
                    if (SessionHelper.JV.GlLines.Count == 0)
                    {
                        message = "No Voucher Detail information available!";
                    }
                    else if (SessionHelper.JV.GlLines.Sum(cri => cri.EnteredDr) == SessionHelper.JV.GlLines.Sum(cri => cri.EnteredCr))
                    {
                        SessionHelper.JV.JournalName = journalName;
                        SessionHelper.JV.GLDate = Convert.ToDateTime(glDate);
                        SessionHelper.JV.ConversionRate = Convert.ToDecimal(cRate);
                        SessionHelper.JV.Description = descr;
                        if (SessionHelper.JV.DocumentNo == "New")
                        {
                            SessionHelper.JV.DocumentNo = JVHelper.GetDocNo(AuthenticationHelper.CompanyId.Value, SessionHelper.JV.PeriodId, SessionHelper.JV.SOBId, SessionHelper.JV.CurrencyId);
                        }

                        JVHelper.Update(SessionHelper.JV);
                        SessionHelper.JV = null;
                        saved = true;
                    }
                    else
                        message = "The sum of Debit and Credit should be equal.";
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

        public JsonResult CurrencyList()
        {
            List<SelectListItem> list = CurrencyHelper.GetCurrencies(SessionHelper.SOBId)
                    .Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PeriodList()
        {
            List<SelectListItem> periodList = CalendarHelper.GetCalendars(SessionHelper.SOBId)
                    .Select(x => new SelectListItem
                    {
                        Text = x.PeriodName,
                        Value = x.Id.ToString()
                    }).ToList();
            return Json(periodList, JsonRequestBehavior.AllowGet);
        }
    }
}
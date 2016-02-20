using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Data.Repositories;
using _360Accounting.Service;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Controllers
{
    [Authorize]
    public class CodeCombinitionController : Controller
    {
        private ICodeCombinitionService service;
        private ISetOfBookService sobService;
        private IAccountService accountService;
        private IAccountValueService accountValueService;

        public CodeCombinitionController()
        {
            service = IoC.Resolve<ICodeCombinitionService>("CodeCombinitionService");
            sobService = IoC.Resolve<ISetOfBookService>("SetOfBookService");
            accountService = IoC.Resolve<IAccountService>("AccountService");
            accountValueService = IoC.Resolve<IAccountValueService>("AccountValueService");
        }

        public ActionResult Index(CodeCombinitionListModel model)
        {
            if (model.SetOfBooks == null)
            {
                model.SetOfBooks = sobService
                    .GetByCompanyId(AuthenticationHelper.User.CompanyId)
                    .Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString(),
                        Selected = x.Id == model.SOBId ? true : false
                    }).ToList();
            }

            model.CodeCombinitions = service.GetAll(AuthenticationHelper.User.CompanyId, model.SOBId != 0 ? model.SOBId : Convert.ToInt64(model.SetOfBooks.First().Value), model.SearchText, true, model.Page, model.SortColumn, model.SortDirection)
                .Select(x => new CodeCombinitionViewModel(x)).ToList();
            return View(model);
        }

        public ActionResult Create(long sobId)
        {
            CodeCombinitionCreateViewModel model =  new CodeCombinitionCreateViewModel();
            model.SegmentList = getSegmentList(sobId);
            model.SOBId = sobId;
            return PartialView("_Edit", model);
        }
        
        public ActionResult Edit(long id, long sobId)
        {
            CodeCombinitionCreateViewModel model = new CodeCombinitionCreateViewModel(service.GetSingle(id.ToString(),AuthenticationHelper.User.CompanyId));
            model.SegmentList = getSegmentList(sobId);
            model.SOBId = sobId;
            return PartialView("_Edit", model);
        }

        public ActionResult UpdateCodeCombinition(long id, long sobId, string segmentValues, bool allowPosting, string startDate, string endDate)
        {
            var segmentList = segmentValues.Split(new char[] { '±' }).ToList<string>();
            CodeCombinitionCreateViewModel model = new CodeCombinitionCreateViewModel();

            model.Segment1 = segmentList[0].Substring(2);
            model.Segment2 = segmentList[1].Substring(2);
            model.Segment3 = segmentList[2].Substring(2);
            model.Segment4 = segmentList[3].Substring(2);
            model.Segment5 = segmentList[4].Substring(2);
            model.Segment6 = segmentList[5].Substring(2);
            model.Segment7 = segmentList[6].Substring(2);
            model.Segment8 = segmentList[7].Substring(2);
            model.AllowedPosting = allowPosting;
            model.CompanyId = AuthenticationHelper.User.CompanyId;
            model.EndDate = endDate == "" ? null : (DateTime?)Convert.ToDateTime(endDate);
            model.StartDate = startDate == "" ? null : (DateTime?)Convert.ToDateTime(startDate);
            model.Id = id;
            model.SOBId = sobId;

            if (model.Id > 0)
            {
                string result = service.Update(mapModel(model));
            }
            else
            {
                string result = service.Insert(mapModel(model));
            }
            return Json("Success");
        }


        public ActionResult Delete(string id)
        {
            service.Delete(id,AuthenticationHelper.User.CompanyId);
            return RedirectToAction("Index");
        }

        public ActionResult GetCodeCombinitionList(long sobId)
        {
            CodeCombinitionListModel model = new CodeCombinitionListModel();
            model.CodeCombinitions = service.GetAll(AuthenticationHelper.User.CompanyId, sobId, model.SearchText, true, model.Page, model.SortColumn, model.SortDirection)
                .Select(x => new CodeCombinitionViewModel(x)).ToList();
            return PartialView("_List", model);
        }

        public ActionResult Test(string task)
        {
            CodeCombinitionCreateViewModel model = new CodeCombinitionCreateViewModel();
            model.SOBId = 18;
            model.SegmentList = getSegmentList(model.SOBId);

            if (task == "Update")
            {
                model.Id = 1;
            }

            model.Segment1 = "00003";     ////To be decided
            model.Segment2 = "0002";     ////To be decided
            model.Segment3 = "0001";     ////To be decided
            ////model.Segment4 = "";     ////To be decided
            ////model.Segment5 = "";     ////To be decided
            ////model.Segment6 = "";     ////To be decided
            ////model.Segment7 = "";     ////To be decided
            ////model.Segment8 = "";     ////To be decided            
            model.StartDate = new DateTime(2016, 1, 1);
            model.EndDate = new DateTime(2016, 12, 31);
            model.AllowedPosting = true;

            if (ModelState.IsValid)
            {
                model.CompanyId = AuthenticationHelper.User.CompanyId;
                if (model.Id > 0)
                {
                    string result = service.Update(mapModel(model));
                }
                else
                {
                    string result = service.Insert(mapModel(model));
                }

                return RedirectToAction("Index", model);
            }

            return View(model);
        }

        #region Private Methods
        private CodeCombinition mapModel(CodeCombinitionCreateViewModel model)
        {
            return new CodeCombinition
            {
                AllowedPosting = model.AllowedPosting,
                CompanyId = model.CompanyId,
                CreateDate = DateTime.Now,
                EndDate = model.EndDate,
                Id = model.Id,
                Segment1 = model.Segment1,
                Segment2 = model.Segment2,
                Segment3 = model.Segment3,
                Segment4 = model.Segment4,
                Segment5 = model.Segment5,
                Segment6 = model.Segment6,
                Segment7 = model.Segment7,
                Segment8 = model.Segment8,
                SOBId = model.SOBId,
                StartDate = model.StartDate,
                UpdateDate = DateTime.Now
            };
        }

        private List<Segment> getSegmentList(long sobId)
        {
            List<Segment> segmentList = new List<Segment>();
            Account account = accountService.GetAccountBySOBId(sobId.ToString(), AuthenticationHelper.User.CompanyId);
            if (account != null)
            {
                if (account.SegmentEnabled1 == true)
                {
                    segmentList.Add(new Segment
                    {
                        SegmentCount = 1,
                        SegmentName = account.SegmentName1,
                        SegmentValueList = accountValueService.GetAccountValuesBySegment(account.SegmentName1, account.Id)
                        .Select(x => new SelectListItem
                        {
                            Value = x.Value,
                            Text = x.Value
                        }).ToList()
                    });
                }

                if (account.SegmentEnabled2 == true)
                {
                    segmentList.Add(new Segment
                    {
                        SegmentCount = 2,
                        SegmentName = account.SegmentName2,
                        SegmentValueList = accountValueService
                        .GetAccountValuesBySegment(account.SegmentName2, account.Id)
                        .Select(x => new SelectListItem
                        {
                            Value = x.Value,
                            Text = x.Value
                        }).ToList()
                    });
                }

                if (account.SegmentEnabled3 == true)
                {
                    segmentList.Add(new Segment
                    {
                        SegmentCount = 3,
                        SegmentName = account.SegmentName3,
                        SegmentValueList = accountValueService
                        .GetAccountValuesBySegment(account.SegmentName3, account.Id)
                        .Select(x => new SelectListItem
                        {
                            Value = x.Value,
                            Text = x.Value
                        }).ToList()
                    });
                }

                if (account.SegmentEnabled4 == true)
                {
                    segmentList.Add(new Segment
                    {
                        SegmentCount = 4,
                        SegmentName = account.SegmentName4,
                        SegmentValueList = accountValueService
                        .GetAccountValuesBySegment(account.SegmentName4, account.Id)
                        .Select(x => new SelectListItem
                        {
                            Value = x.Value,
                            Text = x.Value
                        }).ToList()
                    });
                }

                if (account.SegmentEnabled5 == true)
                {
                    segmentList.Add(new Segment
                    {
                        SegmentCount = 5,
                        SegmentName = account.SegmentName5,
                        SegmentValueList = accountValueService
                        .GetAccountValuesBySegment(account.SegmentName5, account.Id)
                        .Select(x => new SelectListItem
                        {
                            Value = x.Value,
                            Text = x.Value
                        }).ToList()
                    });
                }

                if (account.SegmentEnabled6 == true)
                {
                    segmentList.Add(new Segment
                    {
                        SegmentCount = 6,
                        SegmentName = account.SegmentName6,
                        SegmentValueList = accountValueService
                        .GetAccountValuesBySegment(account.SegmentName6, account.Id)
                        .Select(x => new SelectListItem
                        {
                            Value = x.Value,
                            Text = x.Value
                        }).ToList()
                    });
                }

                if (account.SegmentEnabled7 == true)
                {
                    segmentList.Add(new Segment
                    {
                        SegmentCount = 7,
                        SegmentName = account.SegmentName7,
                        SegmentValueList = accountValueService
                        .GetAccountValuesBySegment(account.SegmentName7, account.Id)
                        .Select(x => new SelectListItem
                        {
                            Value = x.Value,
                            Text = x.Value
                        }).ToList()
                    });
                }

                if (account.SegmentEnabled8 == true)
                {
                    segmentList.Add(new Segment
                    {
                        SegmentCount = 8,
                        SegmentName = account.SegmentName8,
                        SegmentValueList = accountValueService
                        .GetAccountValuesBySegment(account.SegmentName8, account.Id)
                        .Select(x => new SelectListItem
                        {
                            Value = x.Value,
                            Text = x.Value
                        }).ToList()
                    });
                }
            }

            return segmentList;
        }
        #endregion
    }
}
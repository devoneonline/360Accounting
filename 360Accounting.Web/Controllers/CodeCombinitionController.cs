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
            service = new CodeCombinitionService(new CodeCombinitionRepository());
            sobService = new SetOfBookService(new SetOfBookRepository());
            accountService = new AccountService(new AccountRepository());
            accountValueService = new AccountValueService(new AccountValueRepository());
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
                        Value = x.Id.ToString()
                    }).ToList();
            }

            model.CodeCombinitions = service.GetAll(model.SearchText, true, model.Page, model.SortColumn, model.SortDirection)
                .Select(x => new CodeCombinitionViewModel(x)).ToList();
            return View(model);
        }

        public ActionResult Create(long sobId)
        {
            CodeCombinitionCreateViewModel model = 
                new CodeCombinitionCreateViewModel();

            model.SegmentList = getSegmentList(sobId);

            return PartialView("_Edit", model);
        }
        
        public ActionResult Edit(long id)
        {
            CodeCombinitionCreateViewModel model =
                new CodeCombinitionCreateViewModel(service
                    .GetSingle(id.ToString()));
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(CodeCombinitionCreateViewModel model)
        {
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

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Delete(string id)
        {
            service.Delete(id);
            return RedirectToAction("Index");
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
            Segment segment = new Segment();
            List<Segment> segmentList = new List<Segment>();
            Account account = accountService.GetAccountBySOBId(sobId.ToString());
            if (account != null)
            {
                if (account.SegmentEnabled1 == true)
                {
                    segment.SegmentCount = 1;
                    segment.SegmentName = account.SegmentName1;
                    segment.SegmentValueList = accountValueService
                        .GetBySegment(account.SegmentName1, account.Id)
                        .Select(x => new SelectListItem 
                        {
                            Value = x.Value,
                            Text = x.Value
                        }).ToList();
                    segmentList.Add(segment);
                }

                if (account.SegmentEnabled2 == true)
                {
                    segment.SegmentCount = 2;
                    segment.SegmentName = account.SegmentName2;
                    segment.SegmentValueList = accountValueService
                        .GetBySegment(account.SegmentName2, account.Id)
                        .Select(x => new SelectListItem
                        {
                            Value = x.Value,
                            Text = x.Value
                        }).ToList();
                    segmentList.Add(segment);
                }

                if (account.SegmentEnabled3 == true)
                {
                    segment.SegmentCount = 3;
                    segment.SegmentName = account.SegmentName3;
                    segment.SegmentValueList = accountValueService
                        .GetBySegment(account.SegmentName3, account.Id)
                        .Select(x => new SelectListItem
                        {
                            Value = x.Value,
                            Text = x.Value
                        }).ToList();
                    segmentList.Add(segment);
                }

                if (account.SegmentEnabled4 == true)
                {
                    segment.SegmentCount = 4;
                    segment.SegmentName = account.SegmentName4;
                    segment.SegmentValueList = accountValueService
                        .GetBySegment(account.SegmentName4, account.Id)
                        .Select(x => new SelectListItem
                        {
                            Value = x.Value,
                            Text = x.Value
                        }).ToList();
                    segmentList.Add(segment);
                }

                if (account.SegmentEnabled5 == true)
                {
                    segment.SegmentCount = 5;
                    segment.SegmentName = account.SegmentName5;
                    segment.SegmentValueList = accountValueService
                        .GetBySegment(account.SegmentName5, account.Id)
                        .Select(x => new SelectListItem
                        {
                            Value = x.Value,
                            Text = x.Value
                        }).ToList();
                    segmentList.Add(segment);
                }

                if (account.SegmentEnabled6 == true)
                {
                    segment.SegmentCount = 6;
                    segment.SegmentName = account.SegmentName6;
                    segment.SegmentValueList = accountValueService
                        .GetBySegment(account.SegmentName6, account.Id)
                        .Select(x => new SelectListItem
                        {
                            Value = x.Value,
                            Text = x.Value
                        }).ToList();
                    segmentList.Add(segment);
                }

                if (account.SegmentEnabled7 == true)
                {
                    segment.SegmentCount = 7;
                    segment.SegmentName = account.SegmentName7;
                    segment.SegmentValueList = accountValueService
                        .GetBySegment(account.SegmentName7, account.Id)
                        .Select(x => new SelectListItem
                        {
                            Value = x.Value,
                            Text = x.Value
                        }).ToList();
                    segmentList.Add(segment);
                }

                if (account.SegmentEnabled8 == true)
                {
                    segment.SegmentCount = 8;
                    segment.SegmentName = account.SegmentName8;
                    segment.SegmentValueList = accountValueService
                        .GetBySegment(account.SegmentName8, account.Id)
                        .Select(x => new SelectListItem
                        {
                            Value = x.Value,
                            Text = x.Value
                        }).ToList();
                    segmentList.Add(segment);
                }
            }

            return segmentList;
        }
        #endregion
    }
}
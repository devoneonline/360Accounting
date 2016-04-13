using _360Accounting.Common;
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
        public ActionResult Index(long id, CodeCombinitionListModel model)
        {
            model.SOBId = id;
            model.CodeCombinitions = CodeCombinationHelper.GetCodeCombinations(model);
            return View(model);
        }

        public ActionResult Create(string sobId)
        {
            CodeCombinitionCreateViewModel model =  new CodeCombinitionCreateViewModel();
            model.SegmentList = AccountHelper.GetSegmentListForCodeCombination(sobId);
            model.SOBId = Convert.ToInt64(sobId);
            return PartialView("_Create", model);
        }

        public ActionResult LookupAccountCode(string sobId)
        {
            IEnumerable<Segment> model = AccountHelper.GetSegmentListForCodeCombination(SessionHelper.SOBId.ToString(), true);
            return PartialView("_LookupAccountCode", model);
        }

        public ActionResult Edit(string id, string sobId)
        {
            CodeCombinitionCreateViewModel model = CodeCombinationHelper.GetCodeCombination(id);
            model.SegmentList = AccountHelper.GetSegmentListForCodeCombination(sobId);
            model.SOBId = Convert.ToInt32(sobId);
            return PartialView("_Edit", model);
        }

        public JsonResult GetLookupCode(string value)
        {
            return Json(AccountHelper.GetAccountIdBySegments(value));
        }

        public ActionResult UpdateCodeCombinition(long id, long sobId, string segmentValues, bool allowPosting, string startDate, string endDate)
        {
            try
            {
                var segmentList = segmentValues.Split(new char[] { '±' }).ToList<string>();
                CodeCombinitionCreateViewModel model = new CodeCombinitionCreateViewModel();
                for (var i = 1; i <= segmentList.Count; i++)
                {
                    string segmentValue = segmentList[i - 1];
                    if (string.IsNullOrEmpty(segmentValue))
                        continue;
                    else
                        segmentValue = segmentValue.Substring(2);
                    switch (i)
                    {
                        case 1:
                            model.Segment1 = segmentValue;
                            break;
                        case 2:
                            model.Segment2 = segmentValue;
                            break;
                        case 3:
                            model.Segment3 = segmentValue;
                            break;
                        case 4:
                            model.Segment4 = segmentValue;
                            break;
                        case 5:
                            model.Segment5 = segmentValue;
                            break;
                        case 6:
                            model.Segment6 = segmentValue;
                            break;
                        case 7:
                            model.Segment7 = segmentValue;
                            break;
                        case 8:
                            model.Segment8 = segmentValue;
                            break;
                    }
                }

                model.AllowedPosting = allowPosting;
                model.CompanyId = AuthenticationHelper.User.CompanyId;
                model.EndDate = endDate == "" ? null : (DateTime?)Convert.ToDateTime(endDate);
                model.StartDate = startDate == "" ? null : (DateTime?)Convert.ToDateTime(startDate);
                model.Id = id;
                model.SOBId = sobId;

                string result = CodeCombinationHelper.SaveCodeCombination(model);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

            return Json("Success");
        }

        public ActionResult Delete(string id, long sobId)
        {
            CodeCombinationHelper.Delete(id);
            return RedirectToAction("Index", new { id = sobId });
        }

        public ActionResult GetCodeCombinitionList(long sobId)
        {
            CodeCombinitionListModel model = new CodeCombinitionListModel();
            model.CodeCombinitions = CodeCombinationHelper.GetCodeCombinations(model);
            return PartialView("_List", model);
        }

    }
}
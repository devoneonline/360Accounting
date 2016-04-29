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
    public class CodeCombinitionController : BaseController
    {
        public ActionResult Index(long id, CodeCombinitionListModel model, string message="")
        {
            model.SOBId = id;
            model.CodeCombinitions = CodeCombinationHelper.GetCodeCombinations(model);
            return View(model);
        }

        public ActionResult Create()
        {
            CodeCombinitionCreateViewModel model =  new CodeCombinitionCreateViewModel();
            model.SegmentList = AccountHelper.GetSegmentListForCodeCombination(SessionHelper.SOBId.ToString());
            model.SOBId = Convert.ToInt64(SessionHelper.SOBId.ToString());
            return PartialView("_Create", model);
        }

        public ActionResult LookupAccountCode()
        {
            IEnumerable<Segment> model = AccountHelper.GetSegmentListForCodeCombination(SessionHelper.SOBId.ToString(), true);
            return PartialView("_LookupAccountCode", model);
        }

        public ActionResult Edit(string id)
        {
            CodeCombinitionCreateViewModel model = CodeCombinationHelper.GetCodeCombination(id);
            model.SegmentList = AccountHelper.GetSegmentListForCodeCombination(SessionHelper.SOBId.ToString());
            model.SOBId = Convert.ToInt32(SessionHelper.SOBId.ToString());
            return PartialView("_Edit", model);
        }

        public JsonResult GetLookupCode(string value)
        {
            return Json(AccountHelper.GetAccountIdBySegments(value));
        }

        public ActionResult UpdateCodeCombinition(long id, string segmentValues, bool allowPosting, string startDate, string endDate)
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
                model.CompanyId = AuthenticationHelper.CompanyId.Value;
                model.EndDate = endDate == "" ? null : (DateTime?)Convert.ToDateTime(endDate);
                model.StartDate = startDate == "" ? null : (DateTime?)Convert.ToDateTime(startDate);
                model.Id = id;
                model.SOBId = SessionHelper.SOBId;

                string result = CodeCombinationHelper.SaveCodeCombination(model);
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(string id)
        {
            try
            {
                CodeCombinationHelper.Delete(id);
                return RedirectToAction("Index", new { id = SessionHelper.SOBId });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { id = SessionHelper.SOBId, message=ex.Message });
            }
        }

        public ActionResult GetCodeCombinitionList()
        {
            CodeCombinitionListModel model = new CodeCombinitionListModel();
            model.SOBId = SessionHelper.SOBId;
            model.CodeCombinitions = CodeCombinationHelper.GetCodeCombinations(model);
            return PartialView("_List", model);
        }

    }
}
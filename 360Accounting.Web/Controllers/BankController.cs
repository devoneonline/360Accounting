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
    [Authorize]
    public class BankController : Controller
    {
        private IBankService service;
        private ISetOfBookService sobService;

        #region Private Methods
        private Bank mapModel(BankModel model)
        {
            return new Bank
            {
                Remarks = model.Remarks,
                SOBId = model.SOBId,
                CreateDate = DateTime.Now,
                BankName = model.BankName,
                EndDate = model.EndDate,
                Id = model.Id,
                StartDate = model.StartDate,
                UpdateDate = DateTime.Now
            };
        }
        #endregion

        public BankController()
        {
            service = IoC.Resolve<IBankService>("BankService");
            sobService = IoC.Resolve<ISetOfBookService>("SetOfBookService");
        }

        public ActionResult Index()
        {
            BankListModel model = new BankListModel();
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

            model.SOBId = Convert.ToInt64(model.SetOfBooks.FirstOrDefault().Value);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BankModel model)
        {
            if (ModelState.IsValid)
            {
                string result = service.Update(mapModel(model));
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(string id)
        {
            BankModel model = new BankModel(service.GetSingle(id, AuthenticationHelper.User.CompanyId));
            return View("Create", model);
        }

        public ActionResult Delete(string id)
        {
            service.Delete(id, AuthenticationHelper.User.CompanyId);
            return RedirectToAction("Index");
        }

        public ActionResult BankListPartial(long sobId)
        {
            IEnumerable<BankModel> list = service
                .GetBySOBId(sobId)
                .Select(a => new BankModel(a)).ToList();
            return PartialView("_List", list);
        }

        [HttpPost]
        public ActionResult Create(BankModel model)
        {
            if (ModelState.IsValid)
            {
                string result = "";
                if (model.Id > 0)
                    result = service.Update(mapModel(model));
                else
                    result = service.Insert(mapModel(model));

                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Create(long sobId)
        {
            BankModel bank = new BankModel();
            bank.SOBId = sobId;
            return View(bank);
        }

        public JsonResult CheckStartDate(DateTime startDate, long bankId)
        {
            bool returnData = true;
            if (bankId > 0)
            {
                BankModel bank = BankHelper.GetBank(bankId.ToString());
                if (startDate >= bank.StartDate)
                    returnData = true;
                else
                    returnData = false;
            }
            return Json(returnData);
        }

        public JsonResult CheckEndDate(DateTime endDate, long bankId)
        {
            bool returnData = true;
            if (bankId > 0)
            {
                BankModel bank = BankHelper.GetBank(bankId.ToString());
                if (endDate <= bank.EndDate)
                    returnData = true;
                else
                    returnData = false;
            }
            return Json(returnData);
        }
    }
}
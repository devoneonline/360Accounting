using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Data.Repositories;
using _360Accounting.Service;
using DevEx_360Accounting_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DevEx_360Accounting_Web.Controllers
{
    [Authorize]
    public class SetOfBookController : Controller
    {
        private ISetOfBookService service;

        public SetOfBookController()
        {
            service = IoC.Resolve<ISetOfBookService>("SetOfBookService");
        }

        public ActionResult Index()
        {
            SetOfBookListModel model = new SetOfBookListModel();
            var list = service.GetByCompanyId(AuthenticationHelper.User.CompanyId);
            model.SetOfBooks = list.Select(a => new SetOfBookModel(a)).ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            var model = new SetOfBookModel();
            model.CompanyId = AuthenticationHelper.User.CompanyId;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(SetOfBookModel model)
        {
            if (ModelState.IsValid)
            {
                SetOfBook duplicateRecord = service.GetSetOfBook(AuthenticationHelper.User.CompanyId, model.Name);
                if (duplicateRecord == null)
                {
                    string result = service.Insert(MapModel(model));
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Error", "Set of Book Already exists.");
                }
            }

            return View(model);
        }

        public ActionResult Edit(string id)
        {
            SetOfBookModel model = new SetOfBookModel(service.GetSingle(id,AuthenticationHelper.User.CompanyId));
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(SetOfBookModel model)
        {
            if (ModelState.IsValid)
            {
                string result = service.Update(MapModel(model));
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Delete(string id)
        {
            service.Delete(id,AuthenticationHelper.User.CompanyId);
            return RedirectToAction("Index");
        }

        private SetOfBook MapModel(SetOfBookModel model)            ////TODO: this should be done in service will discuss later - FK
        {
            return new SetOfBook
            {
                Id = model.Id,
                CompanyId = model.CompanyId,
                Name = model.Name
            };
        } 
    }
}
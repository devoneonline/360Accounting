using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Data.Repositories;
using _360Accounting.Service;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Controllers
{
    [Authorize]
    public class SetOfBookController : Controller
    {
        public ActionResult SetOfBookPartial()
        {
            return PartialView("_List", SetOfBookHelper.GetSetOfBooks());
        }

        public ActionResult Index(string message = "")
        {
            ViewBag.ErrorMessage = message;
            SetOfBookListModel model = new SetOfBookListModel();
            model.SetOfBooks = SetOfBookHelper.GetSetOfBooks();
            return View(model);
        }

        public ActionResult Create()
        {
            var model = new SetOfBookModel();
            model.CompanyId = AuthenticationHelper.CompanyId.Value;
            ViewBag.Title = "Create Book";
            return View("Edit", model);
        }

        public ActionResult Edit(string id)
        {
            ViewBag.Title = "Edit Book";
            return View(SetOfBookHelper.GetSetOfBook(id));
        }

        [HttpPost]
        public ActionResult Edit(SetOfBookModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    string result = SetOfBookHelper.Update(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    if (SetOfBookHelper.GetSetOfBookByName(model.Name) == null)
                    {
                        string result = SetOfBookHelper.Insert(model);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Set of Book Already exists.");
                    }
                }
            }

            return View(model);
        }

        public ActionResult Delete(string id)
        {
            try
            {
                SetOfBookHelper.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { message = ex.GetBaseException().Message });
            }
        }
    }
}
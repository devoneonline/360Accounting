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
using _360Accounting.Core.IService;
using _360Accounting.Web.Helpers;

namespace _360Accounting.Web.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        public ItemController()
        {

        }
        
        public ActionResult Delete(string id)
        {
            PaymentHelper.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id, long sobId)
        {
            ItemModel model = ItemHelper.GetItem(id);
            SessionHelper.SOBId = model.SOBId;

            if (model.COGSCodeCombination == null)
            {
                model.COGSCodeCombination = CodeCombinationHelper.GetCodeCombinations(sobId, AuthenticationHelper.User.CompanyId)
                    .Select(x => new SelectListItem
                    {
                        Text = Utility.Stringize(".", x.Segment1, x.Segment2, x.Segment3, x.Segment4, x.Segment5, x.Segment6, x.Segment7, x.Segment8),
                        Value = x.Id.ToString()
                    }).ToList();
                model.COGSCodeCombinationId = model.COGSCodeCombination.Any() ?
                    Convert.ToInt32(model.COGSCodeCombination.First().Value) : 0;
            }

            if (model.SalesCodeCombination == null)
            {
                model.SalesCodeCombination = CodeCombinationHelper.GetCodeCombinations(sobId, AuthenticationHelper.User.CompanyId)
                    .Select(x => new SelectListItem
                    {
                        Text = Utility.Stringize(".", x.Segment1, x.Segment2, x.Segment3, x.Segment4, x.Segment5, x.Segment6, x.Segment7, x.Segment8),
                        Value = x.Id.ToString()
                    }).ToList();
                model.SalesCodeCombinationId = model.SalesCodeCombination.Any() ?
                    Convert.ToInt32(model.SalesCodeCombination.First().Value) : 0;
            }

            model.ItemWarehouses = ItemHelper.GetItemWarehouses(id).ToList();
            model.SOBId = sobId;
            SessionHelper.Item = model;

            return View("Create", model);
        }

        public ActionResult Index(ItemListModel model)
        {
            SessionHelper.Item = null;
            if (model.SetOfBooks == null)
            {
                model.SetOfBooks = SetOfBookHelper.GetSetOfBooks()
                    .Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();
                model.SOBId = model.SetOfBooks.Any() ?
                    Convert.ToInt32(model.SetOfBooks.First().Value) : 0;
            }
            return View(model);
        }

        public ActionResult ItemPartial(ItemListModel model)
        {
            SessionHelper.SOBId = model.SOBId;
            return PartialView("_List", ItemHelper.GetItems(model.SOBId));
        }

        public ActionResult Create(long sobId)
        {
            SessionHelper.SOBId = sobId;

            ItemModel model = SessionHelper.Item;
            if (model == null)
            {
                model = new ItemModel
                {
                    SOBId = sobId
                };
                if (model.COGSCodeCombination == null)
                {
                    model.COGSCodeCombination = CodeCombinationHelper.GetCodeCombinations(sobId, AuthenticationHelper.User.CompanyId)
                        .Select(x => new SelectListItem
                        {
                            Text = Utility.Stringize(".", x.Segment1, x.Segment2, x.Segment3, x.Segment4, x.Segment5, x.Segment6, x.Segment7, x.Segment8),
                            Value = x.Id.ToString()
                        }).ToList();
                    model.COGSCodeCombinationId = model.COGSCodeCombination.Any() ?
                        Convert.ToInt32(model.COGSCodeCombination.First().Value) : 0;
                }

                if (model.SalesCodeCombination == null)
                {
                    model.SalesCodeCombination = CodeCombinationHelper.GetCodeCombinations(sobId, AuthenticationHelper.User.CompanyId)
                        .Select(x => new SelectListItem
                        {
                            Text = Utility.Stringize(".", x.Segment1, x.Segment2, x.Segment3, x.Segment4, x.Segment5, x.Segment6, x.Segment7, x.Segment8),
                            Value = x.Id.ToString()
                        }).ToList();
                    model.SalesCodeCombinationId = model.SalesCodeCombination.Any() ?
                        Convert.ToInt32(model.SalesCodeCombination.First().Value) : 0;
                }

                ViewBag.SOBName = SetOfBookHelper.GetSetOfBook(sobId.ToString()).Name;

                SessionHelper.Item = model;
                if (SessionHelper.Item.ItemWarehouses == null)
                    SessionHelper.Item.ItemWarehouses = new List<ItemWarehouseModel>();
            }
            
            return View(model);
        }

        public ActionResult CreatePartial()
        {
            return PartialView("createPartial", ItemHelper.GetItemWarehouses());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewPartial(ItemWarehouseModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool validated = false;
                    
                    if (SessionHelper.Item != null)
                    {
                        model.Id = SessionHelper.Item.ItemWarehouses.Count() + 1;
                        validated = true;
                    }
                    else
                        model.Id = 1;

                    if (validated)
                        ItemHelper.InsertItemDetail(model);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("createPartial", ItemHelper.GetItemWarehouses());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdatePartial(ItemWarehouseModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ItemHelper.UpdateItemDetail(model);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("createPartial", ItemHelper.GetItemWarehouses());
        }

        public ActionResult DeletePartial(ItemWarehouseModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ItemModel header = SessionHelper.Item;
                    ItemHelper.DeleteItemDetail(model);
                    SessionHelper.Item = header;
                    IList<ItemWarehouseModel> itemWarehouses = ItemHelper.GetItemWarehouses();
                    return PartialView("createPartial", itemWarehouses);
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

        public ActionResult Save(ItemModel model)
        {
            string message = "";
            try
            {
                bool saved = false;
                if (SessionHelper.Item != null)
                {
                    SessionHelper.Item.COGSCodeCombinationId = model.COGSCodeCombinationId;
                    SessionHelper.Item.CompanyId = model.CompanyId;
                    SessionHelper.Item.DefaultBuyer = model.DefaultBuyer;
                    SessionHelper.Item.Description = model.Description;
                    SessionHelper.Item.Id = model.Id;
                    SessionHelper.Item.ItemCode = model.ItemCode;
                    SessionHelper.Item.ItemName = model.ItemName;
                    SessionHelper.Item.LotControl = model.LotControl;
                    SessionHelper.Item.Orderable = model.Orderable;
                    SessionHelper.Item.Purchaseable = model.Purchaseable;
                    SessionHelper.Item.ReceiptRouting = model.ReceiptRouting;
                    SessionHelper.Item.SalesCodeCombinationId = model.SalesCodeCombinationId;
                    SessionHelper.Item.SerialControl = model.SerialControl;
                    SessionHelper.Item.Shipable = model.Shipable;
                    SessionHelper.Item.SOBId = model.SOBId;
                    SessionHelper.Item.Status = model.Status;

                    ItemHelper.Save(SessionHelper.Item);
                    SessionHelper.Item = null;
                    saved = true;

                }
                else
                    message = "No Item information available!";
                return Json(new { success = saved, message = message });
            }
            catch (Exception e)
            {
                message = e.Message;
                return Json(new { success = false, message = message });
            }
        }
    }
}
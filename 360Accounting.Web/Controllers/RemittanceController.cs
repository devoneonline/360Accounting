using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Controllers
{
    public class RemittanceController : Controller
    {
        // GET: Remittance
        public ActionResult Index(RemittanceListModel model)
        {
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

            if (model.Banks == null && model.SetOfBooks.Any())
            {
                model.Banks = BankHelper.GetBanks(Convert.ToInt32(model.SetOfBooks.First().Value));
                model.BankId = model.Banks.Any() ?
                    Convert.ToInt32(model.Banks.First().Value) : 0;
            }

            if (model.BankAccounts == null && model.SetOfBooks.Any() && model.Banks.Any())
            {
                model.BankAccounts = BankHelper.GetBankAccounts
                    (Convert.ToInt32(model.SetOfBooks.First().Value),
                    Convert.ToInt32(model.BankAccounts.First().Value));
                model.BankAccountId = model.BankAccounts.Any() ?
                    Convert.ToInt32(model.BankAccounts.First().Value) : 0;
            }




            return View();
        }
    }
}
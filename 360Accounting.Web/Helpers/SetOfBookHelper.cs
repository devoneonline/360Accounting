using _360Accounting.Core;
using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web
{
    public static class SetOfBookHelper
    {
        private static ISetOfBookService service;

        static SetOfBookHelper()
        {
            service = IoC.Resolve<ISetOfBookService>("SetOfBookService");
        }

        public static List<SelectListItem> GetSetOfBook()
        {
            return service.GetByCompanyId(AuthenticationHelper.User.CompanyId)
                .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
        }


    }
}
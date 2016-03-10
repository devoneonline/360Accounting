using _360Accounting.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web
{
    public static class CodeCombinationHelper
    {
        private static ICodeCombinitionService service;

        static CodeCombinationHelper()
        {
            service = IoC.Resolve<ICodeCombinitionService>("CodeCombinitionService");
        }

        public static IList<SelectListItem> GetAccounts(long sobId)
        {
            if (SessionHelper.Calendar != null)
            {
                return service.GetAll(AuthenticationHelper.User.CompanyId, sobId, "", false, null, "", "")
                    .Where(rec => rec.StartDate >= SessionHelper.Calendar.StartDate && rec.EndDate <= SessionHelper.Calendar.EndDate)
                        .Select(x => new SelectListItem
                        {
                            Text = x.CodeCombinitionCode,
                            Value = x.Id.ToString()
                        }).ToList();
            }
            else
            {
                return service.GetAll(AuthenticationHelper.User.CompanyId, sobId, "", false, null, "", "")
                        .Select(x => new SelectListItem
                        {
                            Text = x.CodeCombinitionCode,
                            Value = x.Id.ToString()
                        }).ToList();
            }
        }
    }
}
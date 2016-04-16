using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _360Accounting.Web
{
    public  class UserSetofBookHelper
    {
        private static IUserSetofBookService service;

        static UserSetofBookHelper()
        {
            service = IoC.Resolve<IUserSetofBookService>("UserSetofBookService");
        }

        private static UserSetofBook getEntityByModel(UserSetofBookModel model)
        {
            if (model == null) return null;

            return new UserSetofBook
            {
                CompanyId = model.CompanyId,
                Id = model.Id,
                UserId = model.UserId,
                SOBId = model.SOBId,
            };
        }

        public static string Save(UserSetofBookModel model)
        {
            if (model.Id > 0)
            {
                return service.Update(getEntityByModel(model));
            }
            else
            {
                return service.Insert(getEntityByModel(model));
            }
        }

        public static List<UserSetofBookModel> GetAllUserSetofBook(long companyId)
        {
            return service.GetAll(companyId).Select(x => new UserSetofBookModel(x)).ToList();
        }

        public static UserSetofBookModel GetDefaultSOB()
        {
            return new UserSetofBookModel(service.GetSingle(AuthenticationHelper.UserId.ToString(), AuthenticationHelper.CompanyId.Value));
        }

        public static void DeleteCompany(string id)
        {
            service.Delete(id, AuthenticationHelper.CompanyId.Value);
        }
    }
}
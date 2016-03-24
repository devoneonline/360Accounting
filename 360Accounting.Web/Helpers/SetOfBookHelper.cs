using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Web.Models;
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

        public static List<SetOfBookModel> GetSetOfBooks()
        {
            return service.GetByCompanyId(AuthenticationHelper.User.CompanyId)
                .Select(x => new SetOfBookModel(x)).ToList();                
        }

        public static SetOfBookModel GetSetOfBook(string id)
        {
            SetOfBookModel model = new SetOfBookModel(service.GetSingle(id, AuthenticationHelper.User.CompanyId));
            return model;                
        }

        public static SetOfBook GetSetOfBookByName(string sobName)
        {
            return service.GetSetOfBook(AuthenticationHelper.User.CompanyId, sobName);                
        }

        public static string Insert(SetOfBookModel model)
        {
            return service.Insert(Mappers.GetEntityByModel(model));
        }

        public static string Update(SetOfBookModel model)
        {
            return service.Update(Mappers.GetEntityByModel(model));
        }

        public static void Delete(string id)
        {
            service.Delete(id, AuthenticationHelper.User.CompanyId);
        }

        public static List<SelectListItem> GetSetOfBookList()
        {
            List<SelectListItem> list = GetSetOfBooks().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
            return list;
        }
    }
}
using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Data.Repositories;
using _360Accounting.Service;
using _360Accounting.Web.Controllers;
using _360Accounting.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _360Accounting.Test
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void CreateRolesTest()
        {
            Assert.IsFalse(UserHelper.CreateRole("SuperAdmin"));
            Assert.IsFalse(UserHelper.CreateRole("User"));
            Assert.IsTrue(UserHelper.CreateRole("CompanyAdmin"));
        }

        [TestMethod]
        public void CreateUserTest()
        {
            UserController uc = new UserController();
            UserCreateModel model = new UserCreateModel();
            model.UserName = "softone";
            model.Password = "softone";
            model.FirstName = "Faisal";
            model.LastName = "Khanani";
            model.PhoneNumber = "03332111353";
            model.Email = "faisal.khanani.75@gmail.com";
            model.RoleName = "SuperAdmin";
            var result = UserHelper.CreateUser(model);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void FeatureListTest()
        {
            //IFeatureService service = new FeatureService(new FeatureRepository());
            //List<Feature> result = service.GetAll(1).ToList();
            //Assert.IsTrue(result.Count > 0);
        }


        [TestMethod]
        public void FeatureListByUserTest()
        {
            //IFeatureService service = new FeatureService(new FeatureRepository());
            //List<Feature> result = service.GetMenuItemsByUserId(Guid.Parse("58F351C9-8A17-4A14-B83F-72B1518ED50B")).ToList();
            //Assert.IsTrue(result.Count() > 0);
        }
    }
}

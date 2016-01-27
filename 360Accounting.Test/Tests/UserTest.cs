using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Data.Repositories;
using _360Accounting.Service;
using _360Accounting.Web.Controllers;
using _360Accounting.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            UserController uc = new UserController();
            Assert.IsFalse(uc.CreateRole("SuperAdmin"));
            Assert.IsTrue(uc.CreateRole("User"));
        }

        [TestMethod]
        public void CreateUserTest()
        {
            UserController uc = new UserController();
            UserCreateModel model = new UserCreateModel();
            model.Credentials = new LoginViewModel { UserName = "softone", Password = "Admin123!@#$", RememberMe = true };
            model.FirstName = "Faisal";
            model.LastName = "Khanani";
            model.PhoneNumber = "03332111353";
            model.Email = "faisal.khanani.75@gmail.com";
            model.PasswordQuestion = "What is the name of your first school?";
            model.PasswordAnswer = "KMA";
            model.RoleName = "SuperAdmin";
            var result = uc.CreateUser(model);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void FeatureListTest()
        {
            IFeatureService service = new FeatureService(new FeatureRepository());
            List<Feature> result = service.GetAll().ToList();
            Assert.IsTrue(result.Count > 0);
        }
    }
}

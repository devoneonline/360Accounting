using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Data.Repositories;
using _360Accounting.Service;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Controllers
{
    public class UserController : AsyncController
    {
        private IFeatureService service;

        public UserController()
        {
            ////service = IoC.Resolve<IFeatureService>("FeatureService");
            service = new FeatureService(new FeatureRepository());
        }

        public ActionResult Index()
        {
            int count = 0;
            IEnumerable<FeatureViewModel> featureList = service.GetAll().Select(x => new FeatureViewModel(x));
            return View(featureList);
        }
    }
}
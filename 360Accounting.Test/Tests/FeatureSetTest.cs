using _360Accounting.Common;
using _360Accounting.Core.Entities;
using _360Accounting.Data.Repositories;
using _360Accounting.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace _360Accounting.Test
{
    [TestClass]
    public class FeatureSetTest
    {
        [TestMethod]
        public void InsertFeatureSetTest()
        {
            FeatureSetService service = new FeatureSetService(new FeatureSetRepository());
            FeatureSet entity = new FeatureSet();
            entity.Name = "Featureset - GL";
            entity.AccessType = FeatureAccessType.company.ToString();
            entity.CreateDate = DateTime.Now;
            var result = service.Insert(entity);
            Assert.IsTrue(Convert.ToInt64(result) > 0);
        }

        [TestMethod]
        public void UpdateFeatureSetTest()
        {
            FeatureSetService service = new FeatureSetService(new FeatureSetRepository());
            FeatureSet entity = service.GetSingle("1",1);
            Assert.IsNotNull(entity);

            entity.Name = "Featureset: GL";
            entity.UpdateBy = Guid.NewGuid();
            entity.UpdateDate = DateTime.Now;
            var result = service.Update(entity);
            Assert.IsTrue(Convert.ToInt64(result) > 0);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _360Accounting.Web.Models
{
    public class FeatureViewModel
    {
        public FeatureViewModel(Core.Entities.Feature x)
        {
            this.Id = x.Id;
            this.Name = x.Name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
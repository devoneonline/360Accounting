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
            this.ParentId = x.ParentId;

            if (x.Features != null)
            {
                this.Features = x.Features.Select(f => new FeatureViewModel(f)).ToList();
            }
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public long? ParentId { get; set; }

        public List<FeatureViewModel> Features { get; set; }
    }
}
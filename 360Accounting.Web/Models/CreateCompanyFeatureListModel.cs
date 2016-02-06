using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Models
{
    public class CreateCompanyFeatureListModel
    {
        public List<SelectListItem> CompanyList { get; set; }

        public long Id { get; set; }

        [Required]
        [Display(Name="Select Company")]
        public long CompanyId { get; set; }

        [Required]
        [Display(Name="Feature Set Name")]
        public string Name { get; set; }

        public List<FeatureViewModel> FeatureList { get; set; }

        public string[] SelectedFeatures { get; set; }

    }
}
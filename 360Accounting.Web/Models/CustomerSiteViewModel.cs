﻿using _360Accounting.Common;
using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Models
{
    public class CustomerSiteViewModel
    {
        public CustomerSiteViewModel(CustomerSiteView entity)
        {
            if (entity != null)
            {
                this.CustomerId = entity.CustomerId;
                this.CodeCombinationId = entity.CodeCombinationId;
                this.SiteAddress = entity.SiteAddress;
                this.SiteContact = entity.SiteContact;
                this.SiteName = entity.SiteName;
                this.TaxCodeId = entity.TaxCodeId;
                this.EndDate = entity.EndDate;
                this.Id = entity.Id;
                this.StartDate = entity.StartDate;
                this.CodeCombinationName = entity.CodeCombinationName;
                this.TaxCodeName = entity.TaxCodeName;
            }
        }

        public CustomerSiteViewModel()
        {
            this.StartDate = Const.CurrentDate;
            this.EndDate = Const.ToDate;
        }

        public long Id { get; set; }
        
        public long CustomerId { get; set; }

        [Required]
        [Display(Name="Site Name")]
        [StringLength(30, MinimumLength = 1)]
        public string SiteName { get; set; }

        [Display(Name = "Site Address")]
        public string SiteAddress { get; set; }

        [Display(Name = "Contact")]
        [StringLength(15, MinimumLength = 1)]
        public string SiteContact { get; set; }

        [Display(Name = "Tax Code")]
        public long TaxCodeId { get; set; }

        public string TaxCodeName { get; set; }

        [Display(Name = "Code Combination")]
        public long CodeCombinationId { get; set; }

        public string CodeCombinationName { get; set; }

        public List<SelectListItem> TaxCode { get; set; }

        public List<SelectListItem> CodeCombination { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }
    }
}
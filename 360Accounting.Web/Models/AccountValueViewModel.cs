﻿using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Models
{
    public class AccountValueViewModel
    {
        #region Constructors
        public AccountValueViewModel()
        {
        }

        public AccountValueViewModel(AccountValue bo)
        {
            this.AccountType = bo.AccountType;
            this.ChartId = bo.ChartId;
            this.EndDate = bo.EndDate;
            this.Id = bo.Id;
            this.Levl = bo.Levl;
            this.Segment = bo.Segment;
            this.StartDate = bo.StartDate;
            this.Value = bo.Value;
            this.ValueName = bo.ValueName;
        }
        #endregion

        #region Properties
        public long Id { get; set; }

        public long ChartId { get; set; }

        public string Segment { get; set; }

        [Display(Name = "Set Of Book")]
        public string SetOfBook { get; set; }

        [Required]
        [Display(Name = "Value Name")]
        public string ValueName { get; set; }

        [Required]
        public string Value { get; set; }

        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Account Type")]
        public string AccountType { get; set; }

        public List<SelectListItem> AccountTypes
        {
            get
            {
                List<SelectListItem> lst = new List<SelectListItem>();
                foreach (var value in Enum.GetValues(typeof(Common.AccountTypes)))
                {
                    lst.Add(new SelectListItem { Text = value.ToString(), Value = value.ToString() });
                }
                return lst;
            }
        }

        [Display(Name = "Level")]
        public int? Levl { get; set; }
        #endregion
    }
}
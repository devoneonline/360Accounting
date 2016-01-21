using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Models
{
    public class AccValueCreateModel
    {
        #region Constructors
        
        ////public AccValueCreateModel(AccountValue bo)
        ////{
        ////    this.AccountType = bo.AccountType;

        ////    this.ChartId = bo.ChartId;

        ////    this.EndDate = bo.EndDate;

        ////    this.Id = bo.Id;

        ////    this.Levl = bo.Levl;

        ////    this.StartDate = bo.StartDate;

        ////    this.Value = bo.Value;

        ////    this.ValueName = bo.ValueName;
        ////}
        #endregion

        #region Properties
        ////public long Id { get; set; }

        ////public long ChartId { get; set; }

        public List<SetOfBook> SetOfBooks { get; set; }

        public List<SelectListItem> Segments { get; set; }

        public List<AccountValueViewModel> AccountValues { get; set; }

        ////public string ValueName { get; set; }

        ////public string Value { get; set; }

        ////public DateTime StartDate { get; set; }

        ////public DateTime EndDate { get; set; }

        ////public string AccountType { get; set; }

        ////[Display(Name = "Level")]
        ////public int Levl { get; set; }
        #endregion
    }
}
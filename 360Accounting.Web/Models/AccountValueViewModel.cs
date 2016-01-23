using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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

        public string SetOfBook { get; set; }

        [Required]
        public string ValueName { get; set; }

        [Required]
        public string Value { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string AccountType { get; set; }

        public int? Levl { get; set; }
        #endregion
    }
}
using _360Accounting.Common;
using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Models
{
    public class BankModel
    {
        #region Constructors
        public BankModel()
        {
            this.StartDate = Const.StartDate;
            this.EndDate = Const.EndDate;
        }

        public BankModel(Bank entity)
        {
            this.BankName = entity.BankName;
            this.Id = entity.Id;
            this.EndDate = entity.EndDate;
            this.Remarks = entity.Remarks;
            this.SOBId = entity.SOBId;
            this.StartDate = entity.StartDate;
        }
        #endregion

        #region Properties
        public long Id { get; set; }

        [Display(Name = "Bank Name")]
        public string BankName { get; set; }

        public string Remarks { get; set; }

        public long SOBId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date From")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date To")]
        public DateTime EndDate { get; set; }

        #endregion
    }
}
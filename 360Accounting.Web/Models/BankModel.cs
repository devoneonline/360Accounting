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
    public class BankModel : ModelBase
    {
        #region Constructors
        public BankModel()
        {
            //this.StartDate = Const.StartDate;
            //this.EndDate = Const.EndDate;
        }

        public BankModel(Bank entity)
        {
            this.BankName = entity.BankName;
            this.Id = entity.Id;
            this.EndDate = entity.EndDate;
            this.Remarks = entity.Remarks;
            this.SOBId = entity.SOBId;
            this.StartDate = entity.StartDate;
            this.CreateBy = entity.CreateBy;
            this.CreateDate = entity.CreateDate;
            this.UpdateBy = entity.UpdateBy;
            this.UpdateDate = entity.UpdateDate;
            this.CompanyId = entity.CompanyId;
        }
        #endregion

        #region Properties
        public long Id { get; set; }

        public long CompanyId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Bank name should be atleast 5 & maximum 50 characters long.")]
        [Display(Name = "Bank Name *")]
        public string BankName { get; set; }

        public string Remarks { get; set; }

        public long SOBId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date From")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date To")]
        public DateTime? EndDate { get; set; }

        #endregion
    }
}
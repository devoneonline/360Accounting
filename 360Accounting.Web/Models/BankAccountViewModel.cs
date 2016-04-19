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
    public class BankAccountViewModel : ModelBase
    {
        #region Constructors
        public BankAccountViewModel()
        {
            //this.StartDate = Const.StartDate;
            //this.EndDate = Const.EndDate;
        }

        public BankAccountViewModel(BankAccount entity)
        {
            this.AccountName = entity.AccountName;
            this.AdditionalInformation = entity.AdditionalInformation;
            this.BankId = entity.BankId;
            this.Cash_CCID = entity.Cash_CCID;
            this.Confirm_CCID = entity.Confirm_CCID;
            this.EndDate = entity.EndDate;
            this.StartDate = entity.StartDate;
            this.Id = entity.Id;
            this.RemitCash_CCID = entity.RemitCash_CCID;
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

        public long BankId { get; set; }

        [Required]
        [Display(Name = "Bank Account *")]
        public string AccountName { get; set; }

        [Display(Name = "Information")]
        public string AdditionalInformation { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date From")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date To")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Cash Combinition")]
        public long Cash_CCID { get; set; }

        public string Cash_CCIDString { get; set; }

        [Display(Name = "Remittance Combinition")]
        public long RemitCash_CCID { get; set; }

        public string RemitCash_CCIDString { get; set; }

        [Display(Name = "Confirm Combinition")]
        public long Confirm_CCID { get; set; }

        public string Confirm_CCIDString { get; set; }

        public List<SelectListItem> CodeCombinition { get; set; }

        #endregion
    }
}
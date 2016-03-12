using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Models
{
    public class BankAccountModel
    {
        #region Constructors
        public BankAccountModel()
        {
        }

        public BankAccountModel(BankAccount entity)
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
        }
        #endregion

        #region Properties
        public long Id { get; set; }

        public long BankId { get; set; }

        public string AccountName { get; set; }

        public string AdditionalInformation { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        public long Cash_CCID { get; set; }

        public long RemitCash_CCID { get; set; }

        public long Confirm_CCID { get; set; }

        #endregion
    }
}
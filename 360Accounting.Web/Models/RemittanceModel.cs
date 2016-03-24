using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Models
{
    public class RemittanceListModel
    {
        [Display(Name = "Set Of Book")]
        public List<SelectListItem> SetOfBooks { get; set; }

        [Display(Name = "Bank")]
        public List<SelectListItem> Banks { get; set; }

        [Display(Name = "Bank Account")]
        public List<SelectListItem> BankAccounts { get; set; }

        //public IList<RemittanceModel> Remittances { get; set; }

        public long SOBId { get; set; }
        public long BankId { get; set; }
        public long BankAccountId { get; set; }
    }

    public class RemittanceModel : ModelBase
    {
        #region Properties
        public IList<RemittanceDetailModel> Remittances { get; set; }
        
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public long SOBId { get; set; }
        public long BankId { get; set; }
        public long BankAccountId { get; set; }
        public long ReceiptId { get; set; }
        public string RemitNo { get; set; }
        public DateTime RemitDate { get; set; }
        //public string CustomerName { get; set; }
        //public decimal Amount { get; set; }
        #endregion

        #region Constructors
        public RemittanceModel()
        {
        }

        public RemittanceModel(Remittance entity)
        {
            //this.Amount = ReceiptHelper.
            this.BankAccountId = entity.BankAccountId;
            this.BankId = entity.BankId;
            //this.CustomerName = ReceiptHelper.
            this.Id = entity.Id;
            this.ReceiptId = entity.ReceiptId;
            this.RemitDate = entity.RemitDate;
            this.RemitNo = entity.RemitNo;
            this.SOBId = entity.SOBId;
            this.CompanyId = entity.CompanyId;
            this.CreateBy = entity.CreateBy;
            this.CreateDate = entity.CreateDate;
            this.UpdateBy = entity.UpdateBy;
            this.UpdateDate = entity.UpdateDate;
        }
        #endregion
    }

    public class RemittanceDetailModel : ModelBase
    {
        #region Properties
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public long SOBId { get; set; }
        public long BankId { get; set; }
        public long BankAccountId { get; set; }
        public long ReceiptId { get; set; }
        public string RemitNo { get; set; }
        public DateTime RemitDate { get; set; }
        #endregion 

        #region Constructors
        public RemittanceDetailModel()
        {
        }

        public RemittanceDetailModel(Remittance entity)
        {
            this.BankAccountId = entity.BankAccountId;
            this.BankId = entity.BankId;
            this.Id = entity.Id;
            this.ReceiptId = entity.ReceiptId;
            this.RemitDate = entity.RemitDate;
            this.RemitNo = entity.RemitNo;
            this.SOBId = entity.SOBId;
            this.CompanyId = entity.CompanyId;
            this.CreateBy = entity.CreateBy;
            this.CreateDate = entity.CreateDate;
            this.UpdateBy = entity.UpdateBy;
            this.UpdateDate = entity.UpdateDate;
        }
        #endregion

        
    }
}
using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Models
{
    public class MiscellaneousTransactionListModel
    {
        public long SOBId { get; set; }

        [Display(Name = "Set Of Book")]
        public List<SelectListItem> SetOfBooks { get; set; }

        public IList<MiscellaneousTransactionModel> MiscellaneousTransactions { get; set; }
    }

    public class MiscellaneousTransactionModel : ModelBase
    {
        public MiscellaneousTransactionModel()
        {
 
        }

        public MiscellaneousTransactionModel(MiscellaneousTransaction entity) 
        {
            this.Id = entity.Id;
            this.CompanyId = entity.CompanyId;
            this.CreateBy = entity.CreateBy;
            this.CreateDate = entity.CreateDate;
            this.SOBId = entity.SOBId;
            this.TransactionDate = entity.TransactionDate;
            this.TransactionType = entity.TransactionType;
            this.UpdateBy = entity.UpdateBy;
            this.UpdateDate = entity.UpdateDate;
            this.CodeCombinationId = entity.CodeCombinationId;
        }

        public long Id { get; set; }
        public long CompanyId { get; set; }
        public long SOBId { get; set; }

        [Required]
        [Display(Name = "Account")]
        public long CodeCombinationId { get; set; }
        public string CodeCombinationString { get; set; }

        public List<SelectListItem> CodeCombination { get; set; }
        
        [Required]
        [Display(Name = "Transaction Type")]
        public string TransactionType { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
            ApplyFormatInEditMode = true)]
        [Display(Name = "Transaction Date")]
        public DateTime TransactionDate { get; set; }

        public List<MiscellaneousTransactionDetailModel> MiscellaneousTransactionDetail { get; set; }
    }

    public class MiscellaneousTransactionDetailModel : ModelBase
    {
        public MiscellaneousTransactionDetailModel()
        {

        }

        public MiscellaneousTransactionDetailModel(MiscellaneousTransaction entity) 
        {
            this.Id = entity.Id;
            this.CompanyId = entity.CompanyId;
            this.Cost = entity.Cost;
            this.CreateBy = entity.CreateBy;
            this.CreateDate = entity.CreateDate;
            this.ItemId = entity.ItemId;
            this.LocatorId = entity.LocatorId;
            this.LotNo = entity.LotNo;
            this.Quantity = entity.Quantity;
            this.SerialNo = entity.SerialNo;
            this.SOBId = entity.SOBId;
            this.TransactionDate = entity.TransactionDate;
            this.TransactionType = entity.TransactionType;
            this.UpdateBy = entity.UpdateBy;
            this.UpdateDate = entity.UpdateDate;
            this.WarehouseId = entity.WarehouseId;
            this.CodeCombinationId = entity.CodeCombinationId;
        }

        public long Id { get; set; }
        public long CompanyId { get; set; }
        public long SOBId { get; set; }
        public long CodeCombinationId { get; set; }
        public long ItemId { get; set; }
        public long WarehouseId { get; set; }
        public long LocatorId { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public string LotNo { get; set; }
        public string SerialNo { get; set; }
        public decimal Quantity { get; set; }
        public decimal Cost { get; set; }
    }
}
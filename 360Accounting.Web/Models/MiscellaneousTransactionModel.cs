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
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public long SOBId { get; set; }

        [Required]
        [Display(Name = "Account")]
        public long CodeCombinationId { get; set; }
        public string CodeCombinationString { get; set; }
        
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
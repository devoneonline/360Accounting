using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Models
{
    public class ItemListModel
    {
        public long SOBId { get; set; }
        [Display(Name = "Set Of Books")]
        public List<SelectListItem> SetOfBooks { get; set; }
        public List<ItemModel> Items { get; set; }
    }

    public class ItemModel : ModelBase
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public long SOBId { get; set; }

        [Required]
        [Display(Name = "Sales Code Combination")]
        public long COGSCodeCombinationId { get; set; }

        [Required]
        [Display(Name = "Sales Code Combination")]
        public long SalesCodeCombinationId { get; set; }

        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Name should not exceed 50 characters.")]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "Description should not exceed 255 characters.")]
        public string Description { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Status should not exceed 30 characters.")]
        public string Status { get; set; }

        [Display(Name = "Default Buyer")]
        public string DefaultBuyer { get; set; }

        [Display(Name = "Receipt Routing")]
        public string ReceiptRouting { get; set; }

        [Display(Name = "Lot Control")]
        public bool LotControl { get; set; }

        [Display(Name = "Serial Control")]
        public bool SerialControl { get; set; }

        public bool Purchaseable { get; set; }
        public bool Orderable { get; set; }
        public bool Shipable { get; set; }

        public List<SelectListItem> COGSCodeCombination { get; set; }
        public List<SelectListItem> SalesCodeCombination { get; set; }
        public List<SelectListItem> ReceiptRoutingList
        {
            get
            {
                List<SelectListItem> list = new List<SelectListItem>();
                list.Add(new SelectListItem { Text = "Standard", Value = "Standard" });
                list.Add(new SelectListItem { Text = "Direct", Value = "Direct" });
                return list;
            }
        }
    }

    public class ItemWarehouseModel : ModelBase
    {
        public long Id { get; set; }
        public long SOBId { get; set; }
        public long ItemId { get; set; }

        [Display(Name = "Warehouse Code")]
        public string WarehouseCode { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
    }
}
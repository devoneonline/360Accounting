using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _360Accounting.Web.Models
{
    public class OrderTypeListModel
    {
        public IEnumerable<OrderTypeModel> OrderTypes { get; set; }
    }

    public class OrderTypeModel : ModelBase
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        //public long SOBId { get; set; }

        [Display(Name = "Order Type Name")]
        [Required(ErrorMessage = "Order Type Name is required")]
        [MaxLength(30, ErrorMessage = "Order Type Name Should not exceed 30 characters")]
        public string OrderTypeName { get; set; }

        [Display(Name = "Date To")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateFrom { get; set; }

        [Display(Name = "Date From")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateTo { get; set; }
    }
}
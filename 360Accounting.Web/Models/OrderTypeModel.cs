using _360Accounting.Core.Entities;
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
        public OrderTypeModel()
        { 
        }

        public OrderTypeModel(OrderType entity)
        {
            if (entity != null)
            {
                this.Id = entity.Id;
                this.CompanyId = entity.CompanyId;
                this.OrderTypeName = entity.OrderTypeName;
                this.DateFrom = entity.DateFrom;
                this.DateTo = entity.DateTo;
                this.CreateBy = entity.CreateBy;
                this.CreateDate = entity.CreateDate;
                this.UpdateBy = entity.UpdateBy;
                this.UpdateDate = entity.UpdateDate;
            }
        }
        public long Id { get; set; }
        public long CompanyId { get; set; }
        
        [Display(Name = "Order Type Name *")]
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
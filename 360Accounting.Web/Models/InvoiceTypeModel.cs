using _360Accounting.Common;
using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _360Accounting.Web.Models
{
    public class InvoiceTypeModel : ModelBase
    {
        public long Id { get; set; }

        public long CompanyId { get; set; }

        public string InvoiceType { get; set; }

        public string Meaning { get; set; }

        public string Description { get; set; }

        public long SOBId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date From")]
        public DateTime DateFrom { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date To")]
        public DateTime DateTo { get; set; }

        public InvoiceTypeModel()
        {
            this.DateFrom = Const.StartDate;
            this.DateTo = Const.EndDate;
        }

        public InvoiceTypeModel(InvoiceType entity)
        {
            this.Id = entity.Id;
            this.SOBId = entity.SOBId;
            this.DateFrom = entity.DateFrom;
            this.DateTo = entity.DateTo;
            this.Description = entity.Description;
            this.InvoiceType = entity.Invoicetype;
            this.Meaning = entity.Meaning;
            this.CreateBy = entity.CreateBy;
            this.CreateDate = entity.CreateDate;
            this.UpdateBy = entity.UpdateBy;
            this.UpdateDate = entity.UpdateDate;
        }
    }
}
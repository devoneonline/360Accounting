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
    public class WithholdingModel : ModelBase
    {
        public long Id { get; set; }

        public long CompanyId { get; set; }

        public string WithholdingCode { get; set; }

        public string Description { get; set; }

        public long CodeCombinitionId { get; set; }

        public long VendorId { get; set; }

        public long VendorSiteId { get; set; }

        public decimal Rate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date From")]
        public DateTime DateFrom { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date To")]
        public DateTime DateTo { get; set; }

        public long SOBId { get; set; }

        public WithholdingModel()
        {
            this.DateFrom = Const.StartDate;
            this.DateTo = Const.EndDate;
        }

        public WithholdingModel(Withholding entity)
        {
            this.Id = entity.Id;
            this.SOBId = entity.SOBId;
            this.CodeCombinitionId = entity.CodeCombinitionId;
            this.DateFrom = entity.DateFrom;
            this.DateTo = entity.DateTo;
            this.Description = entity.Description;
            this.Rate = entity.Rate;
            this.VendorId = entity.VendorId;
            this.VendorSiteId = entity.VendorSiteId;
            this.WithholdingCode = entity.Code;
            this.CompanyId = entity.CompanyId;
            this.CreateBy = entity.CreateBy;
            this.CreateDate = entity.CreateDate;
            this.UpdateBy = entity.UpdateBy;
            this.UpdateDate = entity.UpdateDate;
        }
    }
}
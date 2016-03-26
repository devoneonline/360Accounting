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
    public class InvoiceSourceViewModel : ModelBase
    {
        public InvoiceSourceViewModel()
        {
            this.StartDate = Const.StartDate;
            this.EndDate = Const.EndDate;
        }

        public InvoiceSourceViewModel(InvoiceSource entity)
        {
            this.CodeCombinationId = entity.CodeCombinationId;
            this.StartDate = entity.StartDate;
            this.EndDate = entity.EndDate;
            this.Description = entity.Description;
            this.Id = entity.Id;
            this.SOBId = entity.SOBId;
            this.CompanyId = entity.CompanyId;
            this.CreateBy = entity.CreateBy;
            this.CreateDate = entity.CreateDate;
            this.UpdateBy = entity.UpdateBy;
            this.UpdateDate = entity.UpdateDate;
        }

        public long Id { get; set; }
        public long SOBId { get; set; }

        [Display(Name = "Account Code")]
        public long CodeCombinationId { get; set; }

        public string CodeCombinationIdString { get; set; }

        public IList<SelectListItem> CodeCombinations { get; set; }
        //public List<SelectListItem> SetOfBooks { get; set; }
        
        public long CompanyId { get; set; }

        [Display(Name = "Source Name")]
        [StringLength(50, MinimumLength = 1)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
    }

    public class InvoiceSourceListModel
    {
        public long SOBId { get; set; }

        [Display(Name = "Set Of Books")]
        public List<SelectListItem> SetOfBooks { get; set; }
    }
}
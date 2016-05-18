using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _360Accounting.Web.Models
{
    public class BuyerModel : ModelBase
    {
        #region Constructor

        public BuyerModel(Buyer entity)
        {
            this.Name = entity.Name;
            this.EndDate = entity.EndDate;
            this.Id = entity.Id;
            this.CompanyId = entity.CompanyId;
            this.StartDate = entity.StartDate;
            this.CreateBy = entity.CreateBy;
            this.CreateDate = entity.CreateDate;
            this.UpdateBy = entity.UpdateBy;
            this.UpdateDate = entity.UpdateDate;
        }

        public BuyerModel()
        {
        }

        #endregion

        #region Properties

        public long Id { get; set; }

        public long CompanyId { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Buyer name should not exceed 30 characters.")]
        [Display(Name = "Buyer Name *")]
        public string Name { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-mm-dd}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-mm-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        #endregion
    }
}
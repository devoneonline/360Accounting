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
    public class TaxListModel
    {
        public long SOBId { get; set; }
    }

    public class TaxModel : ModelBase
    {
        #region Properties
        public long Id { get; set; }
        public long SOBId { get; set; }
        public long CompanyId { get; set; }

        [Required]
        [Display(Name = "Tax Name")]
        public string TaxName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        public IList<TaxDetailModel> TaxDetails { get; set; }
        #endregion

        #region Constructors
        public TaxModel()
        {
            this.StartDate = Const.StartDate;
            this.EndDate = Const.EndDate;
        }

        public TaxModel(Tax entity)
        {
            this.EndDate = entity.EndDate;
            this.Id = entity.Id;
            this.SOBId = entity.SOBId;
            this.StartDate = entity.StartDate;
            this.TaxName = entity.TaxName;
            this.CompanyId = entity.CompanyId;
            this.CreateBy = entity.CreateBy;
            this.UpdateBy = entity.UpdateBy;
            this.UpdateDate = entity.UpdateDate;
        }
        #endregion
    }

    public class TaxDetailModel : ModelBase
    {
        #region Properties
        public long Id { get; set; }
        public long TaxId { get; set; }
        public long CodeCombinationId { get; set; }
        public decimal Rate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        #endregion

        #region Constructors
        public TaxDetailModel()
        {
            this.Rate = 0;
        }

        public TaxDetailModel(TaxDetail entity)
        {
            this.CodeCombinationId = entity.CodeCombinationId;
            this.EndDate = entity.EndDate;
            this.Id = entity.Id;
            this.Rate = entity.Rate;
            this.StartDate = entity.StartDate;
            this.TaxId = entity.TaxId;
            this.CreateBy = entity.CreateBy;
            this.CreateDate = entity.CreateDate;
            this.UpdateBy = entity.UpdateBy;
            this.UpdateDate = entity.UpdateDate;
        }
        #endregion
    }
}
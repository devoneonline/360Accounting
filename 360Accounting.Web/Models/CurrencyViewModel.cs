using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Models
{
    public class CurrencyViewModel : ModelBase
    {
        #region Constructors
        public CurrencyViewModel()
        {
        }

        public CurrencyViewModel(Currency entity)
        {
            this.Id = entity.Id;
            this.CompanyId = entity.CompanyId;
            this.CurrencyCode = entity.CurrencyCode;
            this.Name = entity.Name;
            this.Precision = entity.Precision;
            this.SOBId = entity.SOBId;
            this.PrecisionName = PrecisionList.Where(rec => rec.Value == entity.Precision.ToString()).FirstOrDefault().Text;
            this.CreateBy = entity.CreateBy;
            this.CreateDate = entity.CreateDate;
            this.UpdateBy = entity.UpdateBy;
            this.UpdateDate = entity.UpdateDate;
        }
        #endregion

        #region Properties
        public long Id { get; set; }

        [Required]
        [Display(Name = "Currency Code")]
        public string CurrencyCode { get; set; }

        [Display(Name = "SOBId")]
        public long SOBId { get; set; }

        [Display(Name = "CompanyId")]
        public long CompanyId { get; set; }

        [Required]
        [Display(Name = "Currency Name")]
        public string Name { get; set; }

        [Display(Name = "Precision")]
        public int Precision { get; set; }

        public string PrecisionName { get; set; }

        public List<SelectListItem> PrecisionList
        {
            get
            {
                List<SelectListItem> list = new List<SelectListItem>();
                list.Add(new SelectListItem { Text = "No Decimal", Value = "0" });
                list.Add(new SelectListItem { Text = "1 Decimal", Value = "1" });
                list.Add(new SelectListItem { Text = "2 Decimal", Value = "2" });
                list.Add(new SelectListItem { Text = "3 Decimal", Value = "3" });
                list.Add(new SelectListItem { Text = "4 Decimal", Value = "4" });
                return list;
            }            
        }
        #endregion
    }
}
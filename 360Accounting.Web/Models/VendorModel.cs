using _360Accounting.Core;
using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Models
{
    public class VendorModel : ModelBase
    {
        #region Constructor

        public VendorModel(Vendor entity)
        {
            this.Address = entity.Address;
            this.ContactNo = entity.ContactNo;
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

        public VendorModel()
        {
        }

        #endregion

        #region Properties

        public long Id { get; set; }

        public long CompanyId { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Vendor name should not exceed 30 characters.")]
        [Display(Name = "Vendor Name *")]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Address { get; set; }

        [MaxLength(15)]
        [Display(Name = "Contact")]
        public string ContactNo { get; set; }

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

    public class VendorSiteModel : ModelBase
    {
        public VendorSiteModel(VendorSite entity)
        {
            if (entity != null)
            {
                this.VendorId = entity.VendorId;
                this.CodeCombinationId = entity.CodeCombinationId;
                this.Address = entity.Address;
                this.Contact = entity.Contact;
                this.Name = entity.Name;
                this.EndDate = entity.EndDate;
                this.Id = entity.Id;
                this.StartDate = entity.StartDate;
                this.CreateBy = entity.CreateBy;
                this.CreateDate = entity.CreateDate;
                this.UpdateBy = entity.UpdateBy;
                this.UpdateDate = entity.UpdateDate;
            }
        }

        public VendorSiteModel(long vendorId)
        {
            this.VendorId = vendorId;
        }
        public VendorSiteModel()
        {
        }

        #region Properties

        public long Id { get; set; }

        public long VendorId { get; set; }

        [Required(ErrorMessage = "Site Name is required.")]
        [Display(Name = "Site Name *")]
        [StringLength(30, MinimumLength = 1)]
        public string Name { get; set; }

        [Display(Name = "Site Address")]
        [StringLength(255, MinimumLength = 0)]
        public string Address { get; set; }

        [Display(Name = "Contact")]
        [StringLength(15, MinimumLength = 1)]
        public string Contact { get; set; }

        //[Display(Name = "Tax Code")]
        //public long? TaxCodeId { get; set; }

        [Required]
        [Display(Name = "Code Combination")]
        public long CodeCombinationId { get; set; }

        [Required(ErrorMessage = "Code Combination is required.")]
        [Display(Name = "Code Combination *")]
        public string CodeCombinationString { get; set; }

        //public List<SelectListItem> TaxCode { get; set; }

        public List<SelectListItem> CodeCombination { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        #endregion

        
    }

    public class VendorSiteViewModel : VendorSiteModel
    {
        #region Constructor

        public VendorSiteViewModel()
        {

        }

        public VendorSiteViewModel(VendorSiteView entity)
        {
            if (entity != null)
            {
                this.VendorId = entity.VendorId;
                this.CodeCombinationId = entity.CodeCombinationId;
                this.Address = entity.Address;
                this.Contact = entity.Contact;
                this.Name = entity.Name;
                this.EndDate = entity.EndDate;
                this.Id = entity.Id;
                this.StartDate = entity.StartDate;
                this.CodeCombinationName = Utility.CodeCombination(entity.CodeCombination, ".");
            }
        }

        #endregion

        #region Properties

        public string CodeCombinationName { get; set; }
        //public string TaxCodeName { get; set; }

        #endregion
    }
}
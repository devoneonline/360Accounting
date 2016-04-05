using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Models
{
    public class LocatorListModel
    {
        [Display(Name = "Set Of Book")]
        public long SOBId { get; set; }
        
        public List<SelectListItem> SetOfBooks { get; set; }
        public List<LocatorModel> Locators { get; set; }
    }

    public class LocatorModel : ModelBase
    {
        #region Constructors
        public LocatorModel()
        {
        }

        public LocatorModel(Locator entity)
        {
            this.CompanyId = entity.CompanyId;
            this.CreateBy = entity.CreateBy;
            this.CreateDate = entity.CreateDate;
            this.Description = entity.Description;
            this.Id = entity.Id;
            this.SOBId = entity.SOBId;
            this.Status = entity.Status;
            this.UpdateBy = entity.UpdateBy;
            this.UpdateDate = entity.UpdateDate;
        }
        #endregion

        #region Properties
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public long SOBId { get; set; }

        public string Description { get; set; }
        public string Status { get; set; }

        public List<LocatorWarehouseModel> LocatorWarehouses { get; set; }
        #endregion
    }

    public class LocatorWarehouseModel : ModelBase
    {
        #region Constructors
        public LocatorWarehouseModel()
        {
        }

        public LocatorWarehouseModel(LocatorWarehouse entity)
        {
            this.CreateBy = entity.CreateBy;
            this.CreateDate = entity.CreateDate;
            this.EndDate = entity.EndDate;
            this.Id = entity.Id;
            this.LocatorId = entity.LocatorId;
            this.SOBId = entity.SOBId;
            this.StartDate = entity.StartDate;
            this.UpdateBy = entity.UpdateBy;
            this.UpdateDate = entity.UpdateDate;
            this.WarehouseId = entity.WarehouseId;
        }
        #endregion

        #region Properties
        public long Id { get; set; }
        public long SOBId { get; set; }
        public long LocatorId { get; set; }
        public long WarehouseId { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        public List<SelectListItem> Warehouses { get; set; }
        #endregion
    }
}
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
    public class WarehouseModel : ModelBase
    {
        public long Id { get; set; }
        
        public long CompanyId { get; set; }

        [Display(Name="Warehouse Name")]
        public string WarehouseName { get; set; }

        public string Status { get; set; }

        public long SOBId { get; set; }
        
        public WarehouseModel()
        {

        }

        public WarehouseModel(Warehouse entity)
        {
            this.Id = entity.Id;
            this.SOBId = entity.SOBId;
            this.Status = entity.Status;
            this.WarehouseName = entity.WarehouseName;
            this.CompanyId = entity.CompanyId;
            this.CreateBy = entity.CreateBy;
            this.CreateDate = entity.CreateDate;
            this.UpdateBy = entity.UpdateBy;
            this.UpdateDate = entity.UpdateDate;
        }
    }
}
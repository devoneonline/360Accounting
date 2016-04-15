using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Models
{
    public class ReceivablePeriodListModel
    {
        public long SOBId { get; set; }

        public IEnumerable<ReceivablePeriodModel> ReceivablePeriods { get; set; }
    }

    public class ReceivablePeriodModel : ModelBase
    {
        public long Id { get; set; }
        
        public long CompanyId { get; set; }

        public long CalendarId { get; set; }

        public string Status { get; set; }

        public long SOBId { get; set; }
        
        public ReceivablePeriodModel()
        {

        }

        public ReceivablePeriodModel(ReceivablePeriod entity)
        {
            if (entity != null)
            {
                this.Id = entity.Id;
                this.SOBId = entity.SOBId;
                this.Status = entity.Status;
                this.CalendarId = entity.CalendarId;
                this.CompanyId = entity.CompanyId;
                this.CreateBy = entity.CreateBy;
                this.CreateDate = entity.CreateDate;
                this.UpdateBy = entity.UpdateBy;
                this.UpdateDate = entity.UpdateDate;
            }            
        }
    }
}
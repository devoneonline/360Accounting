using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _360Accounting.Web.Models
{
    public class PurchasingPeriodListModel
    {
        public long SOBId { get; set; }
        public IEnumerable<PurchasingPeriod> PurchasingPeriods { get; set; }
    }

    public class PurchasingPeriodModel : ModelBase
    {
        #region Properties
        public long Id { get; set; }

        public long CompanyId { get; set; }

        public long CalendarId { get; set; }

        public string Status { get; set; }

        public long SOBId { get; set; }
        #endregion

        #region Constructors
        public PurchasingPeriodModel()
        {
        }

        public PurchasingPeriodModel(PurchasingPeriod entity)
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
        #endregion
    }
}
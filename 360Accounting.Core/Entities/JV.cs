using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class GLHeader : EntityBase
    {
        public long Id { get; set; }
        public long SOBId { get; set; }
        public long CompanyId { get; set; }
        public long PeriodId { get; set; }
        public long CurrencyId { get; set; }
        public string JournalName { get; set; }
        public string Description { get; set; }
        public DateTime GLDate { get; set; }
        public string DocumentNo { get; set; }
        public decimal ConversionRate { get; set; }
    }

    public class GLHeaderView : EntityBase
    {
        public long Id { get; set; }
        public long SOBId { get; set; }
        public long CompanyId { get; set; }
        public long PeriodId { get; set; }
        public long CurrencyId { get; set; }
        public string JournalName { get; set; }
        public string Description { get; set; }
        public DateTime GLDate { get; set; }
        public string DocumentNo { get; set; }
        public decimal ConversionRate { get; set; }

        public string PeriodName { get; set; }
        public string CurrencyName { get; set; }
    }


    public class GLLines : EntityBase
    {
        public long Id { get; set; }
        public long HeaderId { get; set; }
        public long CodeCombinationId { get; set; }
        public decimal EnteredDr { get; set; }
        public decimal EnteredCr { get; set; }
        public decimal AccountedDr { get; set; }
        public decimal AccountedCr { get; set; }
        public decimal Qty { get; set; }
        public string Description { get; set; }
        public long TaxRateCode { get; set; }
    }
}

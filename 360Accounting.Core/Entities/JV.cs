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


    public class GLLines : EntityBase
    {
        public long Id { get; set; }
        public long HeaderId { get; set; }
        public long CodeCombinationId { get; set; }
        public double EnteredDr { get; set; }
        public double EnteredCr { get; set; }
        public double AccountedDr { get; set; }
        public double AccountedCr { get; set; }
        public double Qty { get; set; }
        public string Description { get; set; }
        public long TaxRateCode { get; set; }
    }
}

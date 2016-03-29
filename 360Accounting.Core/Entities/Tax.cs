using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class Tax : EntityBase
    {
        [Key]
        public long Id { get; set; }

        public long CompanyId { get; set; }

        public long SOBId { get; set; }
        
        public string TaxName { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
    }

    public class TaxDetail : EntityBase
    {
        [Key]
        public long Id { get; set; }

        public long TaxId { get; set; }
        
        public long CodeCombinationId { get; set; }
        
        public decimal Rate { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
    }

    //public class TaxDum : EntityBase
    //{
    //    [Key]
    //    public long Id { get; set; }

    //    public string TaxName { get; set; }

    //    public decimal TaxRate { get; set; }

    //    public DateTime StartDate { get; set; }

    //    public DateTime EndDate { get; set; }

    //    public long CodeCombinationId { get; set; }
    //}
}

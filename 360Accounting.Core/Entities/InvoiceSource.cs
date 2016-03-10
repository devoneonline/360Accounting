using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class InvoiceSource : EntityBase
    {
        [Key]
        public long Id { get; set; }
        public long SOBId { get; set; }
        ///TODO: Need to Add Code combination field
        //public long CodeCombinationId { get; set; }
        public long CompanyId { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

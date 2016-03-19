using _360Accounting.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class InvoiceType : EntityBase
    {
        [Key]
        public long Id { get; set; }

        public string Invoicetype { get; set; }

        public string Meaning { get; set; }

        public string Description { get; set; }

        public long SOBId { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }
    }
}

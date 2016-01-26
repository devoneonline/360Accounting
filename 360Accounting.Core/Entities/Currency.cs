using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class Currency : EntityBase
    {
        [Key]
        public string CurrencyCode { get; set; }

        public long SOBId { get; set; }

        public long CompanyId { get; set; }

        public string Name { get; set; }

        public int Precision { get; set; }

        ////public string SOBName { get; set; }
    }
}

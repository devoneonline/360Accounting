using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class AccountView
    {
        public long Id { get; set; }

        public long SOBId { get; set; }

        public string SOBName { get; set; }

        public string Segments { get; set; }

        public string SegmentsLength { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _360Accounting.Web.Models
{
    public class SetOfBookModel
    {
        public long Id { get; set; }

        public long CompanyId { get; set; }

        public string Name { get; set; }
    }
}
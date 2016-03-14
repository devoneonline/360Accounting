using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _360Accounting.Web.Models
{
    public class ModelBase
    {
        public Guid CreateBy { get; set; }

        public DateTime CreateDate { get; set; }

        public Guid? UpdateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _360Accounting.Web.Models
{
    public class CustomerListModel
    {
        public IEnumerable<CustomerModel> Customers { get; set; }
    }
}
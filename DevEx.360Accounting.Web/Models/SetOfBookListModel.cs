using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevEx_360Accounting_Web.Models
{
    public class SetOfBookListModel
    {
        public IEnumerable<SetOfBookModel> SetOfBooks { get; set; }
    }
}
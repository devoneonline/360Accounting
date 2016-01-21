using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _360Accounting.Web.Models
{
    public class AccountListModel
    {
        #region Properties

        public IEnumerable<AccountViewModel> Accounts { get; set; }

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _360Accounting.Web.Models
{
    public class SelectUser
    {
        #region Properties

        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string Role { get; set; }

        public long CompanyId { get; set; }

        public bool Selected { get; set; }

        #endregion
    }
}
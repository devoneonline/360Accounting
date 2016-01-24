using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Models
{


    public class AccValueCreateModel
    {
        #region Constructors

        #endregion

        #region Properties
        public List<SelectListItem> SetOfBooks { get; set; }

        public long SOBId { get; set; }

        public List<SelectListItem> Segments { get; set; }

        public string Segment { get; set; }

        public List<AccountValueViewModel> AccountValues { get; set; }
        #endregion
    }
}
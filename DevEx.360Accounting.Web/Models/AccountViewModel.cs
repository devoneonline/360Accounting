using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DevEx_360Accounting_Web.Models
{
    public class AccountViewModel
    {
        public AccountViewModel()
        {
        }

        public AccountViewModel(AccountView entity)
        {
            this.Id = entity.Id;
            this.SOBId = entity.SOBId;
            this.SOBName = entity.SOBName;
            this.Segments = entity.Segments;
            this.SegmentsLength = entity.SegmentsLength;
        }

        public long Id { get; set; }

        public long SOBId { get; set; }

        public string SOBName { get; set; }

        public string Segments { get; set; }

        public string SegmentsLength { get; set; }
    }
}
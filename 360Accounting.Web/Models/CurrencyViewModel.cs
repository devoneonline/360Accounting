using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _360Accounting.Web.Models
{
    public class CurrencyViewModel
    {
        #region Constructors
        public CurrencyViewModel()
        {
        }

        public CurrencyViewModel(Currency entity)
        {
            this.CompanyId = entity.CompanyId;
            this.CurrencyCode = entity.CurrencyCode;
            this.Name = entity.Name;
            this.Precision = entity.Precision;
            this.SOBId = entity.SOBId;
            this.SOBName = entity.SOBName;
        }
        #endregion

        #region Properties
        public string CurrencyCode { get; set; }

        public long SOBId { get; set; }

        public long CompanyId { get; set; }

        public string Name { get; set; }

        public int Precision { get; set; }

        public string SOBName { get; set; }
        #endregion
    }
}
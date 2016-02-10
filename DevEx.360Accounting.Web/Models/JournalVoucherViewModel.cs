using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevEx_360Accounting_Web.Models
{
    public class JournalVoucherViewModel
    {
        #region Constructors
        public JournalVoucherViewModel()
        {
        }

        public JournalVoucherViewModel(JournalVoucher entity)
        {
            this.CompanyId = entity.CompanyId;
            this.ConversionRate = entity.ConversionRate;
            this.CurrencyId = entity.CurrencyId;
            this.Description = entity.Description;
            this.DocumentNo = entity.DocumentNo;
            this.GLDate = entity.GLDate;
            this.Id = entity.Id;
            this.JournalName = entity.JournalName;
            //this.JournalVoucherDetail = entity.JournalVoucherDetail.Select(x => new JournalVoucherDetailModel(x)).ToList();
            this.PeriodId = entity.PeriodId;
            this.SOBId = entity.SOBId;
        }
        #endregion

        #region Properties
        public long Id { get; set; }

        public long SOBId { get; set; }

        public long CompanyId { get; set; }

        public long PeriodId { get; set; }

        public long CurrencyId { get; set; }

        public string JournalName { get; set; }

        public string Description { get; set; }

        public DateTime GLDate { get; set; }

        public string DocumentNo { get; set; }

        public decimal ConversionRate { get; set; }

        public List<JournalVoucherDetailModel> JournalVoucherDetail { get; set; }
        #endregion
    }

    public class JournalVoucherDetailModel
    {
        #region Constructors
        public JournalVoucherDetailModel()
        {
        }

        public JournalVoucherDetailModel(JournalVoucherDetail entity)
        {
            this.AccountedCr = entity.AccountedCr;
            this.AccountedDr = entity.AccountedDr;
            this.CodeCombinationId = entity.CodeCombinationId;
            this.Description = entity.Description;
            this.EnteredCr = entity.EnteredCr;
            this.EnteredDr = entity.EnteredDr;
            this.HeaderId = entity.HeaderId;
            this.Id = entity.Id;
            this.Qty = entity.Qty;
            this.TaxRateCode = entity.TaxRateCode;
        }
        #endregion

        #region Properties
        public long Id { get; set; }

        public long HeaderId { get; set; }

        public long CodeCombinationId { get; set; }

        public decimal EnteredDr { get; set; }

        public decimal EnteredCr { get; set; }

        public decimal AccountedDr { get; set; }

        public decimal AccountedCr { get; set; }

        public decimal Qty { get; set; }

        public string Description { get; set; }

        public long TaxRateCode { get; set; }
        #endregion 
    }
}
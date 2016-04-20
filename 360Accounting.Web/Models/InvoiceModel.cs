using _360Accounting.Common;
using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Models
{
    public class InvoiceListModel
    {
        [Display(Name = "Period")]
        public List<SelectListItem> Periods { get; set; }

        [Display(Name = "Currency")]
        public List<SelectListItem> Currencies { get; set; }

        public long SOBId { get; set; }
        public long PeriodId { get; set; }
        public long CurrencyId { get; set; }
    }

    public class InvoiceModel : ModelBase
    {
        #region Properties
        [Display(Name = "Customer")]
        public List<SelectListItem> Customers { get; set; }

        [Display(Name = "Site")]
        public List<SelectListItem> CustomerSites { get; set; }

        [Display(Name = "Invoice Type")]
        public List<SelectListItem> InvoiceTypes
        {
            get
            {
                List<SelectListItem> list = new List<SelectListItem>();
                list.Add(new SelectListItem { Text = "Invoice", Value = "Invoice" });
                list.Add(new SelectListItem { Text = "Debit Memo", Value = "Debit Memo" });
                list.Add(new SelectListItem { Text = "Credit Memo", Value = "Credit Memo" });
                return list;
            }
        }

        public long Id { get; set; }
        public long SOBId { get; set; }
        public long PeriodId { get; set; }
        public long CurrencyId { get; set; }
        public long CompanyId { get; set; }
        public long CustomerId { get; set; }
        public long CustomerSiteId { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
            ApplyFormatInEditMode = true)]
        [Display(Name = "Document Date")]
        public DateTime InvoiceDate { get; set; }

        [Display(Name = "Document Type")]
        public string InvoiceType { get; set; }

        [Display(Name = "Document No")]
        public string InvoiceNo { get; set; }

        [Display(Name = "Conversion Rate")]
        public decimal? ConversionRate { get; set; }
        public string Remarks { get; set; }

        ////Experimating with singular detail property...
        public IList<InvoiceDetailModel> InvoiceDetail { get; set; }
        #endregion

        #region Constructors
        public InvoiceModel()
        {
            this.InvoiceDate = Const.CurrentDate;
        }

        public InvoiceModel(Invoice entity)
        {
            this.CompanyId = entity.CompanyId;
            this.ConversionRate = entity.ConversionRate;
            this.CurrencyId = entity.CurrencyId;
            this.CustomerId = entity.CustomerId;
            this.CustomerSiteId = entity.CustomerSiteId;
            this.Id = entity.Id;
            this.InvoiceDate = entity.InvoiceDate;
            this.InvoiceNo = entity.InvoiceNo;
            this.InvoiceType = entity.InvoiceType;
            this.PeriodId = entity.PeriodId;
            this.Remarks = entity.Remarks;
            this.SOBId = entity.SOBId;
            this.CreateBy = entity.CreateBy;
            this.CreateDate = entity.CreateDate;
            this.UpdateBy = entity.UpdateBy;
            this.UpdateDate = entity.UpdateDate;
        }
        #endregion
    }

    public class InvoiceDetailModel : ModelBase
    {
        #region Constructors
        public InvoiceDetailModel()
        {
        }

        public InvoiceDetailModel(InvoiceDetail entity)
        {
            this.Id = entity.Id;
            this.InvoiceId = entity.InvoiceId;
            this.InvoiceSourceId = entity.InvoiceSourceId;
            this.ItemId = entity.ItemId;
            this.Quantity = entity.Quantity;
            this.Rate = entity.Rate;
            this.TaxId = entity.TaxId;
        }
        #endregion

        #region Properties
        public long Id { get; set; }
        public long InvoiceId { get; set; }
        public long? InvoiceSourceId { get; set; }
        public long? ItemId { get; set; }
        public long TaxId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        #endregion
    }
}
using _360Accounting.Core.Entities;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _360Accounting.Web
{
    public static class Mappers
    {
        public static PayableInvoiceDetail GetEntityByModel(PayableInvoiceDetailModel model, int count)
        {
            if (model == null)
                return null;

            PayableInvoiceDetail entity = new PayableInvoiceDetail();
            if (count == 0)
            {
                entity.CreateBy = AuthenticationHelper.UserId;
                entity.CreateDate = DateTime.Now;
            }
            else
            {
                entity.CreateBy = model.CreateBy;
                entity.CreateDate = model.CreateDate;
            }

            entity.Amount = model.Amount;
            entity.CodeCombinationId = model.CodeCombinationId;
            entity.Description = model.Description;
            entity.Id = model.Id;
            entity.InvoiceId = model.InvoiceId;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;

            return entity;
        }

        public static PayableInvoice GetEntityByModel(PayableInvoiceModel model)
        {
            if (model == null)
                return null;

            PayableInvoice entity = new PayableInvoice();
            if (model.Id == 0)
            {
                entity.CreateBy = AuthenticationHelper.UserId;
                entity.CreateDate = DateTime.Now;
                entity.CompanyId = AuthenticationHelper.CompanyId.Value;
            }
            else
            {
                entity.CreateBy = model.CreateBy;
                entity.CreateDate = model.CreateDate;
                entity.CompanyId = model.CompanyId;
            }

            entity.Amount = model.Amount;
            entity.Id = model.Id;
            entity.InvoiceDate = model.InvoiceDate;
            entity.InvoiceNo = model.InvoiceNo;
            entity.InvoiceTypeId = model.InvoiceTypeId;
            entity.PeriodId = model.PeriodId;
            entity.Remarks = model.Remarks;
            entity.SOBId = model.SOBId;
            entity.Status = model.Status;
            entity.UpdateBy = model.UpdateBy;
            entity.UpdateDate = model.UpdateDate;
            entity.VendorId = model.VendorId;
            entity.VendorSiteId = model.VendorSiteId;
            entity.WHTaxId = model.WHTaxId;
            return entity;
        }

        public static Remittance GetEntityByModel(RemittanceDetailModel model)
        {
            if (model == null)
                return null;

            Remittance entity = new Remittance();

            if (model.Id == 0)
            {
                entity.CreateBy = AuthenticationHelper.UserId;
                entity.CreateDate = DateTime.Now;
                entity.CompanyId = AuthenticationHelper.CompanyId.Value;
            }
            else
            {
                entity.CreateBy = model.CreateBy;
                entity.CreateDate = model.CreateDate;
                entity.CompanyId = model.CompanyId;
            }

            entity.BankAccountId = model.BankAccountId;
            entity.BankId = model.BankId;
            entity.Id = model.Id;
            entity.ReceiptId = model.ReceiptId;
            entity.RemitDate = model.RemitDate;
            entity.RemitNo = model.RemitNo;
            entity.SOBId = model.SOBId;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            return entity;
        }
        
        public static InvoiceDetail GetEntityByModel(InvoiceDetailModel model)
        {
            if (model == null) return null;
            InvoiceDetail entity = new InvoiceDetail();

            if (model.Id == 0)
            {
                entity.CreateBy = AuthenticationHelper.UserId;
                entity.CreateDate = DateTime.Now;
            }
            else
            {
                entity.CreateBy = model.CreateBy;
                entity.CreateDate = model.CreateDate;
            }

            entity.Id = model.Id;
            entity.InvoiceId = model.InvoiceId;
            entity.InvoiceSourceId = model.InvoiceSourceId;
            entity.Quantity = model.Quantity;
            entity.Rate = model.Rate;
            entity.TaxId = model.TaxId;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            return entity;
        }

        public static Invoice GetEntityByModel(InvoiceModel model)
        {
            if (model == null) return null;
            Invoice entity = new Invoice();

            if (model.Id == 0)
            {
                entity.CreateBy = AuthenticationHelper.UserId;
                entity.CreateDate = DateTime.Now;
                entity.CompanyId = AuthenticationHelper.CompanyId.Value;
            }
            else
            {
                entity.CreateBy = model.CreateBy;
                entity.CreateDate = model.CreateDate;
                entity.CompanyId = model.CompanyId;
            }
            entity.ConversionRate = model.ConversionRate;
            entity.CurrencyId = model.CurrencyId;
            entity.CustomerId = model.CustomerId;
            entity.CustomerSiteId = model.CustomerSiteId;
            entity.Id = model.Id;
            entity.InvoiceDate = model.InvoiceDate;
            entity.InvoiceNo = model.InvoiceNo;
            entity.InvoiceType = model.InvoiceType;
            entity.PeriodId = model.PeriodId;
            entity.Remarks = model.Remarks;
            entity.SOBId = model.SOBId;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;

            return entity;
        }

        public static TaxDetail GetEntityByModel(TaxDetailModel model)
        {
            if (model == null) return null;
            TaxDetail entity = new TaxDetail();

            if (model.Id == 0)
            {
                entity.CreateBy = AuthenticationHelper.UserId;
                entity.CreateDate = DateTime.Now;
            }
            else
            {
                entity.CreateBy = model.CreateBy;
                entity.CreateDate = model.CreateDate;
            }
            entity.CodeCombinationId = model.CodeCombinationId;
            entity.EndDate = model.EndDate;
            entity.Id = model.Id;
            entity.Rate = model.Rate;
            entity.StartDate = model.StartDate;
            entity.TaxId = model.TaxId;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;

            return entity;
        }

        public static Tax GetEntityByModel(TaxModel model)
        {
            if (model == null) return null;
            Tax entity = new Tax();

            if (model.Id == 0)
            {
                entity.CreateBy = AuthenticationHelper.UserId;
                entity.CreateDate = DateTime.Now;
                entity.CompanyId = AuthenticationHelper.CompanyId.Value;
            }
            else
            {
                entity.CreateBy = model.CreateBy;
                entity.CreateDate = model.CreateDate;
                entity.CompanyId = model.CompanyId;
            }

            entity.EndDate = model.EndDate;
            entity.Id = model.Id;
            entity.SOBId = model.SOBId;
            entity.StartDate = model.StartDate;
            entity.TaxName = model.TaxName;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            
            return entity;
        }

        public static Account GetEntityByModel(AccountCreateViewModel model)
        {
            if (model == null) return null;
            
            Account entity = new Account();
            entity.Id = model.Id;
            entity.SegmentChar1 = model.SegmentChar1;
            entity.SegmentChar2 = model.SegmentChar2;
            entity.SegmentChar3 = model.SegmentChar3;
            entity.SegmentChar4 = model.SegmentChar4;
            entity.SegmentChar5 = model.SegmentChar5;
            entity.SegmentChar6 = model.SegmentChar6;
            entity.SegmentChar7 = model.SegmentChar7;
            entity.SegmentChar8 = model.SegmentChar8;
            entity.SegmentEnabled1 = model.SegmentEnabled1;
            entity.SegmentEnabled2 = model.SegmentEnabled2;
            entity.SegmentEnabled3 = model.SegmentEnabled3;
            entity.SegmentEnabled4 = model.SegmentEnabled4;
            entity.SegmentEnabled5 = model.SegmentEnabled5;
            entity.SegmentEnabled6 = model.SegmentEnabled6;
            entity.SegmentEnabled7 = model.SegmentEnabled7;
            entity.SegmentEnabled8 = model.SegmentEnabled8;
            entity.SegmentName1 = model.SegmentName1;
            entity.SegmentName2 = model.SegmentName2;
            entity.SegmentName3 = model.SegmentName3;
            entity.SegmentName4 = model.SegmentName4;
            entity.SegmentName5 = model.SegmentName5;
            entity.SegmentName6 = model.SegmentName6;
            entity.SegmentName7 = model.SegmentName7;
            entity.SegmentName8 = model.SegmentName8;
            entity.SOBId = model.SOBId;
            if (model.Id == 0)
            {
                entity.CompanyId = AuthenticationHelper.User.CompanyId;
                entity.CreateBy=AuthenticationHelper.UserId;
                entity.CreateDate = DateTime.Now;
            }
            else
            {
                entity.CompanyId = model.CompanyId;
                entity.CreateBy = model.CreateBy;
                entity.CreateDate = model.CreateDate;
            }
            entity.UpdateDate = DateTime.Now;
            return entity;
        }

        public static InvoiceSource GetEntityByModel(InvoiceSourceViewModel model)
        {
            if (model == null) return null;
            InvoiceSource entity = new InvoiceSource();

            entity.CodeCombinationId = model.CodeCombinationId;
            entity.StartDate = model.StartDate;
            entity.EndDate = model.EndDate;
            entity.Description = model.Description;
            entity.Id = model.Id;
            entity.SOBId = model.SOBId;
            if (model.Id == 0)
            {
                entity.CompanyId = AuthenticationHelper.CompanyId.Value;
                entity.CreateBy = AuthenticationHelper.UserId;
                entity.CreateDate = DateTime.Now;
            }
            else
            {
                entity.CompanyId = model.CompanyId;
                entity.CreateBy = model.CreateBy;
                entity.CreateDate = model.CreateDate;
            }
            entity.UpdateDate = DateTime.Now;
            entity.UpdateBy = AuthenticationHelper.UserId;

            return entity;
        }

        public static SetOfBook GetEntityByModel(SetOfBookModel model)
        {
            if (model == null) return null;

            return new SetOfBook
            {
                CompanyId = AuthenticationHelper.User.CompanyId,
                Id = model.Id,
                Name = model.Name
            };
        }

        public static AccountValue GetEntityByModel(AccountValueViewModel model)
        {
            if (model == null) return null;
            AccountValue entity=new AccountValue();

            entity.AccountType = model.AccountType;
            entity.ChartId = model.ChartId;
            entity.EndDate = model.EndDate;
            entity.Id = model.Id;
            entity.Levl = model.Levl;
            entity.Segment = model.Segment;
            entity.StartDate = model.StartDate;
            if (model.Id ==0)
            {
                entity.CreateDate = DateTime.Now;
                entity.CreateBy = AuthenticationHelper.UserId;
                entity.CompanyId = AuthenticationHelper.CompanyId.Value;
            }
            else
            {
                entity.CreateBy = model.CreateBy;
                entity.CreateDate = model.CreateDate;
                entity.CompanyId = model.CompanyId;
            }
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            entity.Value = model.Value;
            entity.ValueName = model.ValueName;

            return entity;
        }

        public static CodeCombinition GetEntityByModel(CodeCombinitionCreateViewModel model)
        {
            if (model == null) return null;

            var entity =new CodeCombinition();

            entity.AllowedPosting = model.AllowedPosting;
            entity.EndDate = model.EndDate;
            entity.Id = model.Id;
            entity.Segment1 = model.Segment1;
            entity.Segment2 = model.Segment2;
            entity.Segment3 = model.Segment3;
            entity.Segment4 = model.Segment4;
            entity.Segment5 = model.Segment5;
            entity.Segment6 = model.Segment6;
            entity.Segment7 = model.Segment7;
            entity.Segment8 = model.Segment8;
            entity.SOBId = model.SOBId;
            entity.StartDate = model.StartDate;

            if (model.Id == 0)
            {
                entity.CompanyId = AuthenticationHelper.User.CompanyId;
                entity.CreateBy = AuthenticationHelper.UserId;
                entity.CreateDate = DateTime.Now;
            }
            else
            {
                entity.CompanyId = model.CompanyId;
                entity.CreateBy = model.CreateBy;
                entity.CreateDate = model.CreateDate;
            }
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;

            return entity;
        }

        public static Calendar GetEntityByModel(CalendarViewModel model)
        {
            if (model == null) return null;

            Calendar entity = new Calendar();

            if (model.Id == 0)
            {
                entity.CreateBy = AuthenticationHelper.UserId;
                entity.CreateDate = DateTime.Now;
                entity.CompanyId = AuthenticationHelper.CompanyId.Value;
            }
            else
            {
                entity.CreateBy = model.CreateBy;
                entity.CreateDate = model.CreateDate;
                entity.CompanyId = model.CompanyId;
            }

            entity.Adjusting = model.Adjusting;
            entity.ClosingStatus = model.ClosingStatus;
            entity.EndDate = model.EndDate;
            entity.Id = model.Id;
            entity.PeriodName = model.PeriodName;
            entity.PeriodQuarter = model.PeriodQuarter;
            entity.PeriodYear = model.PeriodYear;
            entity.SeqNumber = model.SeqNumber;
            entity.SOBId = model.SOBId;
            entity.StartDate = model.StartDate;
            entity.UpdateDate = DateTime.Now;
            entity.UpdateBy = AuthenticationHelper.UserId;
            return entity;
        }

        public static Company GetEntityByModel(CompanyModel model)
        {
            if (model == null) return null;

            return new Company
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        public static Currency GetEntityByModel(CurrencyViewModel model)
        {
            if (model == null) return null;

            Currency entity = new Currency();

            if (model.Id == 0)
            {
                entity.CreateBy = AuthenticationHelper.UserId;
                entity.CreateDate = DateTime.Now;
                entity.CompanyId = AuthenticationHelper.CompanyId.Value;
            }
            else
            {
                entity.CreateBy = model.CreateBy;
                entity.CreateDate = model.CreateDate;
                entity.CompanyId = model.CompanyId;
            }

            entity.CurrencyCode = model.CurrencyCode;
            entity.Id = model.Id;
            entity.Name = model.Name;
            entity.Precision = model.Precision;
            entity.SOBId = model.SOBId;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            return entity;
        }

        public static Customer GetEntityByModel(CustomerModel model)
        {
            if (model == null) return null;

            Customer entity = new Customer();
            if (model.Id == 0)
            {
                entity.CreateBy = AuthenticationHelper.UserId;
                entity.CreateDate = DateTime.Now;
                entity.CompanyId = AuthenticationHelper.CompanyId.Value;
            }
            else
            {
                entity.CreateBy = model.CreateBy;
                entity.CreateDate = model.CreateDate;
                entity.CompanyId = model.CompanyId;
            }

            entity.Address = model.Address;
            entity.ContactNo = model.ContactNo;
            entity.CustomerName = model.CustomerName;
            entity.EndDate = model.EndDate;
            entity.Id = model.Id;
            entity.StartDate = model.StartDate;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;

            return entity;
            
        }

        public static GLHeader GetEntityByModel(GLHeaderModel model)
        {
            if (model == null) return null;

            GLHeader entity = new GLHeader();
            entity.Id = model.Id;
            entity.JournalName = model.JournalName;
            entity.ConversionRate = model.ConversionRate;
            entity.CurrencyId = model.CurrencyId;
            entity.Description = model.Description;
            entity.DocumentNo = model.DocumentNo;
            entity.GLDate = model.GLDate;
            entity.PeriodId = model.PeriodId;
            entity.SOBId = model.SOBId;
            if (model.Id == 0)
            {
                entity.CreateBy = AuthenticationHelper.UserId;
                entity.CreateDate = DateTime.Now;
                entity.CompanyId = AuthenticationHelper.CompanyId.Value;
            }
            else
            {
                entity.CreateBy = model.CreateBy;
                entity.CreateDate = model.CreateDate;
                entity.CompanyId = model.CompanyId;
            }
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            return entity;
        }

        public static GLLines GetEntityByModel(GLLinesModel model, decimal conversionRate)
        {
            if (model == null) return null;

            GLLines entity = new GLLines();
            entity.Id = model.Id;
            entity.HeaderId = model.HeaderId;
            entity.CodeCombinationId = model.CodeCombinationId;
            entity.Description = model.Description;
            entity.EnteredCr = model.EnteredCr;
            entity.EnteredDr = model.EnteredDr;
            entity.AccountedCr = model.EnteredCr * conversionRate;
            entity.AccountedDr = model.EnteredDr * conversionRate;
            entity.Qty = model.Quantity;
            entity.TaxRateCode = model.TaxRateCode;
            if (model.Id == 0)
            {
                entity.CreateBy = AuthenticationHelper.UserId;
                entity.CreateDate = DateTime.Now;
            }
            else
            {
                entity.CreateBy = model.CreateBy;
                entity.CreateDate = model.CreateDate;
            }
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            return entity;
        }

        public static PayablePeriod GetEntityByModel(PayablePeriodModel model)
        {
            if (model == null) return null;

            PayablePeriod entity = new PayablePeriod();

            if (model.Id == 0)
            {
                entity.CreateBy = AuthenticationHelper.UserId;
                entity.CreateDate = DateTime.Now;
                entity.CompanyId = AuthenticationHelper.CompanyId.Value;
            }
            else
            {
                entity.CreateBy = model.CreateBy;
                entity.CreateDate = model.CreateDate;
                entity.CompanyId = model.CompanyId;
            }

            entity.CalendarId = model.CalendarId;
            entity.Id = model.Id;
            entity.SOBId  = model.SOBId;
            entity.Status = model.Status;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            return entity;
        }

        public static Withholding GetEntityByModel(WithholdingModel model)
        {
            if (model == null) return null;

            Withholding entity = new Withholding();

            if (model.Id == 0)
            {
                entity.CreateBy = AuthenticationHelper.UserId;
                entity.CreateDate = DateTime.Now;
                entity.CompanyId = AuthenticationHelper.CompanyId.Value;
            }
            else
            {
                entity.CreateBy = model.CreateBy;
                entity.CreateDate = model.CreateDate;
                entity.CompanyId = model.CompanyId;
            }

            entity.Code = model.WithholdingCode;
            entity.VendorSiteId = model.VendorSiteId;
            entity.VendorId = model.VendorId;
            entity.SOBId = model.SOBId;
            entity.Rate = model.Rate;
            entity.Description = model.Description;
            entity.DateTo = model.DateTo;
            entity.DateFrom = model.DateFrom;
            entity.CodeCombinitionId = model.CodeCombinitionId;
            entity.Id = model.Id;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            return entity;
        }

        public static InvoiceType GetEntityByModel(InvoiceTypeModel model)
        {
            if (model == null) return null;

            InvoiceType entity = new InvoiceType();
            if (model.Id == 0)
            {
                entity.CreateBy = AuthenticationHelper.UserId;
                entity.CreateDate = DateTime.Now;
                entity.CompanyId = AuthenticationHelper.CompanyId.Value;
            }
            else
            {
                entity.CreateBy = model.CreateBy;
                entity.CreateDate = model.CreateDate;
                entity.CompanyId = model.CompanyId;
            }

            entity.Meaning = model.Meaning;
            entity.SOBId = model.SOBId;
            entity.Invoicetype = model.InvoiceType;
            entity.Id = model.Id;
            entity.Description = model.Description;
            entity.DateTo = model.DateTo;
            entity.DateFrom = model.DateFrom;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            return entity;
        }

        public static PaymentHeader GetEntityByModel(PaymentViewModel model)
        {
            if (model == null) return null;

            PaymentHeader entity = new PaymentHeader();

            if (model.Id == 0)
            {
                entity.CreateBy = AuthenticationHelper.UserId;
                entity.CreateDate = DateTime.Now;
                entity.CompanyId = AuthenticationHelper.CompanyId.Value;
            }
            else
            {
                entity.CreateBy = model.CreateBy;
                entity.CreateDate = model.CreateDate;
                entity.CompanyId = AuthenticationHelper.CompanyId.Value; //Not exist.. have to do this..
            }

            entity.Amount = model.Amount;
            entity.BankId = model.BankId;
            entity.Id = model.Id;
            entity.PaymentDate = model.PaymentDate;
            entity.PaymentNo = model.PaymentNo;
            entity.Id = model.PeriodId;
            entity.Status = model.Status;
            entity.SOBId = model.SOBId;
            entity.BankAccountId = model.BankAccountId;
            entity.VendorId = model.VendorId;
            entity.VendorSiteId = model.VendorSiteId;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            return entity;
        }

        public static PaymentInvoiceLines GetEntityByModel(PaymentInvoiceLinesModel model)
        {
            if (model == null) return null;

            PaymentInvoiceLines entity = new PaymentInvoiceLines
            {
                Amount = model.Amount,
                PaymentId = model.PaymentId,
                InvoiceId = model.InvoiceId,
                Id = model.Id
            };
            if (model.Id == 0)
            {
                entity.CreateBy = AuthenticationHelper.UserId;
                entity.CreateDate = DateTime.Now;
            }
            else
            {
                entity.CreateBy = model.CreateBy;
                entity.CreateDate = model.CreateDate;
            }
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            return entity;
        }
    }
}
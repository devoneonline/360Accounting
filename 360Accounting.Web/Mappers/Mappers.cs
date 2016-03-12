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
        public static Account GetEntityByModel(AccountCreateViewModel model)
        {
            if (model == null) return null;
            
            Account entity = new Account();
            entity.CompanyId = AuthenticationHelper.User.CompanyId;
            entity.CreateDate = DateTime.Now;            
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
            entity.UpdateDate = DateTime.Now;
            return entity;
        }

        public static InvoiceSource GetEntityByModel(InvoiceSourceViewModel model)
        {
            if (model == null) return null;

            return new InvoiceSource
            {
                CodeCombinationId = model.CodeCombinationId,
                CompanyId = AuthenticationHelper.User.CompanyId,
                CreateDate = DateTime.Now,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Description = model.Description,
                Id = model.Id,
                SOBId = model.SOBId,
                UpdateDate = DateTime.Now
            };
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

            return new AccountValue
            {
                AccountType = model.AccountType,
                ChartId = model.ChartId,
                CreateDate = DateTime.Now,
                EndDate = model.EndDate,
                Id = model.Id,
                Levl = model.Levl,
                Segment = model.Segment,
                StartDate = model.StartDate,
                UpdateDate = DateTime.Now,
                Value = model.Value,
                ValueName = model.ValueName
            };
        }

        public static CodeCombinition GetEntityByModel(CodeCombinitionCreateViewModel model)
        {
            if (model == null) return null;

            return new CodeCombinition
            {
                AllowedPosting = model.AllowedPosting,
                CompanyId = AuthenticationHelper.User.CompanyId,
                CreateDate = DateTime.Now,
                EndDate = model.EndDate,
                Id = model.Id,
                Segment1 = model.Segment1,
                Segment2 = model.Segment2,
                Segment3 = model.Segment3,
                Segment4 = model.Segment4,
                Segment5 = model.Segment5,
                Segment6 = model.Segment6,
                Segment7 = model.Segment7,
                Segment8 = model.Segment8,
                SOBId = model.SOBId,
                StartDate = model.StartDate,
                UpdateDate = DateTime.Now
            };
        }

        public static Calendar GetEntityByModel(CalendarViewModel model)
        {
            if (model == null) return null;

            return new Calendar
            {
                Adjusting = model.Adjusting,
                ClosingStatus = model.ClosingStatus,
                CompanyId = AuthenticationHelper.User.CompanyId,
                CreateDate = DateTime.Now,
                EndDate = model.EndDate,
                Id = model.Id,
                PeriodName = model.PeriodName,
                PeriodQuarter = model.PeriodQuarter,
                PeriodYear = model.PeriodYear,
                SeqNumber = model.SeqNumber,
                SOBId = model.SOBId,
                StartDate = model.StartDate,
                UpdateDate = DateTime.Now
            };
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

            return new Currency
            {
                CompanyId = AuthenticationHelper.User.CompanyId,
                CreateDate = DateTime.Now,
                CurrencyCode = model.CurrencyCode,
                Id = model.Id,
                Name = model.Name,
                Precision = model.Precision,
                SOBId = model.SOBId,
                UpdateDate = DateTime.Now
            };
        }

        public static Customer GetEntityByModel(CustomerModel model)
        {
            if (model == null) return null;

            return new Customer
            {
                Address = model.Address,
                CompanyId = AuthenticationHelper.User.CompanyId,
                ContactNo = model.ContactNo,
                CreateDate = DateTime.Now,
                CustomerName = model.CustomerName,
                EndDate = model.EndDate,
                Id = model.Id,
                StartDate = model.StartDate,
                UpdateDate = DateTime.Now
            };
        }

        public static GLHeader GetEntityByModel(GLHeaderModel model)
        {
            if (model == null) return null;

            GLHeader entity = new GLHeader();
            entity.Id = model.Id;
            entity.JournalName = model.JournalName;
            entity.CompanyId = model.CompanyId;
            entity.ConversionRate = model.ConversionRate;
            entity.CurrencyId = model.CurrencyId;
            entity.Description = model.Description;
            entity.DocumentNo = model.DocumentNo;
            entity.GLDate = model.GLDate;
            entity.PeriodId = model.PeriodId;
            entity.SOBId = model.SOBId;
            if (model.Id == 0)
            {
                entity.CreateDate = DateTime.Now;
            }
            entity.UpdateBy = entity.CreateBy;
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
                entity.CreateDate = DateTime.Now;
            }
            entity.UpdateBy = entity.CreateBy;
            entity.UpdateDate = DateTime.Now;
            return entity;
        }
    }
}
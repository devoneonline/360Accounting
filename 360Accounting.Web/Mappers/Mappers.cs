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
                //CodeCombinationId = model.CodeCombinationId,
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
    }
}
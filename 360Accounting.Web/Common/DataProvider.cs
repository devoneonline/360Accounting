using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web
{
    public static class DataProvider
    {
        private static IGLHeaderService service;
        private static IGLLineService lineService;
        private static ICodeCombinitionService codeCombinitionService;

        static DataProvider()
        {
            service = IoC.Resolve<IGLHeaderService>("GLHeaderService");
            lineService = IoC.Resolve<IGLLineService>("GLLineService");
            codeCombinitionService = IoC.Resolve<ICodeCombinitionService>("CodeCombinitionService");
        }

        public static IList<GLHeaderModel> GetGLHeaders()
        {
            IList<GLHeader> entityList = service.GetAll(AuthenticationHelper.User.CompanyId).ToList();
            IList<GLHeaderModel> modelList = entityList.Select(x => new GLHeaderModel(x)).ToList();
            return modelList;
        }

        public static IList<GLLinesModel> GetGLLines()
        {
            GLHeaderModel header = AuthenticationHelper.JV;
            IList<GLLinesModel> modelList;
            if (header == null)
            {
                modelList = lineService.GetAll(0).Select(x => new GLLinesModel(x)).ToList();
            }
            else
            {
                modelList = header.GlLines;
            }
            return modelList;
        }

        public static IList<SelectListItem> GetAccounts()
        {

            //TODO: need to set setofbook dynamically

            return codeCombinitionService.GetAll(AuthenticationHelper.User.CompanyId, 6, "", false, null, "", "")   
                    .Select(x => new SelectListItem
                    {
                        Text = x.CodeCombinitionCode,
                        Value = x.Id.ToString()
                    }).ToList();
        }

        public static void Update(string journalName, string glDate, string cRate, string descr)
        {
            bool isNewRecord=true;

            GLHeaderModel header = AuthenticationHelper.JV;
            if (header==null)
            {
                throw new Exception("No voucher information available!");
            }
            header.JournalName = journalName;
            header.GLDate = Convert.ToDateTime(glDate);
            header.ConversionRate = Convert.ToDecimal(cRate);
            header.Description = descr;

            GLHeader entity = GetEntityByModel(header);

            string result = string.Empty;
            if (header.Id>0)    //update
            {
                isNewRecord=false;
               result= service.Update(entity);
            }
            else
            {
                result= service.Insert(entity);
            }

            if (!string.IsNullOrEmpty(result))
            {
                foreach(var line in header.GlLines)
                {
                    GLLines lineEntity = GetEntityByModel(line,header.ConversionRate);
                    lineEntity.HeaderId = Convert.ToInt64(result);
                    if (isNewRecord)
                    {
                        lineService.Insert(lineEntity);
                    }
                    else
                    {
                        lineService.Update(lineEntity);
                    }
                }
            }
        }

        public static void Insert(GLLinesModel model)
        {
            GLHeaderModel header = AuthenticationHelper.JV;
            header.GlLines.Add(model);
        }


        private static GLHeader GetEntityByModel(GLHeaderModel model)
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
            if (model.Id>0)
            {
                entity.CreateDate = DateTime.Now;
            }
            entity.UpdateBy = entity.CreateBy;
            entity.UpdateDate = DateTime.Now;
            return entity;
        }

        private static GLLines GetEntityByModel(GLLinesModel model, decimal conversionRate)
        {
            if (model == null) return null;

            GLLines entity = new GLLines();
            entity.Id = model.Id;
            entity.HeaderId = model.HeaderId;
            entity.CodeCombinationId = model.CodeCombinationId;
            entity.Description = model.Description;
            entity.EnteredCr = model.EnteredCr;
            entity.EnteredDr = model.EnteredDr;
            entity.AccountedCr = model.EnteredCr * Convert.ToDouble(conversionRate);
            entity.AccountedDr = model.EnteredDr * Convert.ToDouble(conversionRate);
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
using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Web.Models;
using _360Accounting.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web
{
    public static class JVHelper
    {
        private static IGLHeaderService service;
        private static IGLLineService lineService;


        static JVHelper()
        {
            service = IoC.Resolve<IGLHeaderService>("GLHeaderService");
            lineService = IoC.Resolve<IGLLineService>("GLLineService");
        }

        public static GLHeaderModel GetVoucher(string id)
        {
            GLHeaderModel jvHeader = new GLHeaderModel(service.GetSingle(id, AuthenticationHelper.User.CompanyId));
            jvHeader.GlLines = getGLLinesByHeaderId(id);

            return jvHeader;
        }

        public static GLHeaderModel GetGLHeaders(string id)
        {
            return new GLHeaderModel(service.GetSingle(id, AuthenticationHelper.User.CompanyId));
        }

        public static IList<GLHeaderModel> GetGLHeaders(long sobId, long periodId, long currencyId)
        {
            IList<GLHeader> entityList = service.GetAll(AuthenticationHelper.User.CompanyId, sobId, periodId, currencyId).ToList();
            IList<GLHeaderModel> modelList = entityList.Select(x => new GLHeaderModel(x)).ToList();
            return modelList;
        }

        public static IList<GLLinesModel> GetGLLines(string headerId)
        {
            return getGLLinesData(headerId);
        }

        public static IList<GLLinesModel> GetGLLines()
        {
            return getGLLinesData();
        }

        public static string GetDocNo(long companyId, long periodId, long sobId, long currencyId)
        {
            var currentDocument = service.GetSingle(companyId, periodId, sobId, currencyId);
            string newDocNo = "";
            if (currentDocument != null)
            {
                int outVal;
                bool isNumeric = int.TryParse(currentDocument.DocumentNo, out outVal);
                if (isNumeric && currentDocument.DocumentNo.Length == 8)
                {
                    newDocNo = (int.Parse(currentDocument.DocumentNo) + 1).ToString();
                    return newDocNo;
                }
            }

            //Create New DocNum..
            string yearDigit = DateTime.Now.ToString("yy");
            string monthDigit = DateTime.Now.ToString("MM");
            string docNo = int.Parse("1").ToString().PadLeft(4, '0');

            return yearDigit + monthDigit + docNo;
        }

        public static void Update(GLHeaderModel jv)
        {
            GLHeader entity = GetEntityByModel(jv);

            string result = string.Empty;
            if (entity.IsValid())
            {
                if (jv.Id > 0)
                    result = service.Update(entity);
                else
                    result = service.Insert(entity);

                if (!string.IsNullOrEmpty(result))
                {
                    var savedLines = getGLLinesByHeaderId(result);
                    if (savedLines.Count() > jv.GlLines.Count())
                    {
                        var tobeDeleted = savedLines.Take(savedLines.Count() - jv.GlLines.Count());
                        foreach (var item in tobeDeleted)
                        {
                            lineService.Delete(item.Id.ToString(), AuthenticationHelper.User.CompanyId);
                        }
                        savedLines = getGLLinesByHeaderId(result);
                    }

                    foreach (var line in jv.GlLines)
                    {
                        GLLines lineEntity = GetEntityByModel(line, jv.ConversionRate);
                        if (lineEntity.IsValid())
                        {
                            lineEntity.HeaderId = Convert.ToInt64(result);
                            if (savedLines.Count() > 0)
                            {
                                lineEntity.Id = savedLines.FirstOrDefault().Id;
                                savedLines.Remove(savedLines.FirstOrDefault(rec => rec.Id == lineEntity.Id));
                                lineService.Update(lineEntity);
                            }
                            else
                                lineService.Insert(lineEntity);
                        }
                    }
                }
            }
        }

        public static void UpdateGLLine(GLLinesModel model)
        {
            GLHeaderModel header = SessionHelper.JV;
            header.GlLines.FirstOrDefault(x => x.Id == model.Id).AccountedCr = model.AccountedCr;
            header.GlLines.FirstOrDefault(x => x.Id == model.Id).AccountedDr = model.AccountedDr;
            header.GlLines.FirstOrDefault(x => x.Id == model.Id).CodeCombinationId = model.CodeCombinationId;
            header.GlLines.FirstOrDefault(x => x.Id == model.Id).Description = model.Description;
            header.GlLines.FirstOrDefault(x => x.Id == model.Id).EnteredCr = model.EnteredCr;
            header.GlLines.FirstOrDefault(x => x.Id == model.Id).EnteredDr = model.EnteredDr;
            header.GlLines.FirstOrDefault(x => x.Id == model.Id).Quantity = model.Quantity;
            header.GlLines.FirstOrDefault(x => x.Id == model.Id).TaxRateCode = model.TaxRateCode;
        }

        public static void DeleteGLLine(GLLinesModel model)
        {
            GLHeaderModel header = SessionHelper.JV;
            GLLinesModel glLine = header.GlLines.FirstOrDefault(x => x.Id == model.Id);
            header.GlLines.Remove(glLine);
        }

        public static void Insert(GLLinesModel model)
        {
            GLHeaderModel header = SessionHelper.JV;
            header.GlLines.Add(model);
        }

        internal static void Delete(string id)
        {
            service.Delete(id, AuthenticationHelper.User.CompanyId);
        }

        #region Private Methods
        private static IList<GLLinesModel> getGLLinesData([Optional]string headerId)
        {
            GLHeaderModel header = SessionHelper.JV;
            IList<GLLinesModel> modelList;
            if (header == null)
                modelList = lineService.GetAll(AuthenticationHelper.User.CompanyId, Convert.ToInt32(headerId)).Select(x => new GLLinesModel(x)).ToList();
            else
                modelList = header.GlLines;

            return modelList;
        }

        private static IList<GLLinesModel> getGLLinesByHeaderId(string headerId)
        {
            return lineService.GetAll(AuthenticationHelper.User.CompanyId, Convert.ToInt32(headerId)).Select(x => new GLLinesModel(x)).ToList();
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
            if (model.Id == 0)
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
        #endregion
    }
}
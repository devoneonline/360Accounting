﻿using _360Accounting.Core;
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
        
        public static void Update(string journalName, string glDate, string cRate, string descr)
        {
            bool isNewRecord = true;

            GLHeaderModel header = SessionHelper.JV;
            if (header == null)
            {
                throw new Exception("No voucher information available!");
            }
            header.JournalName = journalName;
            header.GLDate = Convert.ToDateTime(glDate);
            header.ConversionRate = Convert.ToDecimal(cRate);
            header.Description = descr;
            header.DocumentNo = JVHelper.GetDocNo(AuthenticationHelper.User.CompanyId, header.PeriodId, header.SOBId, header.CurrencyId);

            GLHeader entity = Mappers.GetEntityByModel(header);

            string result = string.Empty;
            if (header.Id > 0)    //update
            {
                isNewRecord = false;
                result = service.Update(entity);
            }
            else
            {
                result = service.Insert(entity);
            }

            if (!string.IsNullOrEmpty(result))
            {
                foreach (var line in header.GlLines)
                {
                    GLLines lineEntity = GetEntityByModel(line, header.ConversionRate);
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

        public static void UpdateGLLine(GLLinesModel model)
        {
            GLHeaderModel header = SessionHelper.JV;
            GLLinesModel glLine = header.GlLines.FirstOrDefault(x => x.Id == model.Id);
            header.GlLines.Remove(glLine);
            header.GlLines.Add(model);
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
            {
                ////Edit mai ye chalta hai
                modelList = lineService.GetAll(AuthenticationHelper.User.CompanyId, Convert.ToInt32(headerId)).Select(x => new GLLinesModel(x)).ToList();
            }
            else
            {
                ////New mode mai ye chalta hai
                modelList = header.GlLines;
                
                
            }
            return modelList;
        }
        #endregion
    }
}
﻿using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevEx_360Accounting_Web.Models
{
    public class CodeCombinitionViewModel
    {
        #region Constructors
        public CodeCombinitionViewModel()
        {
        }

        public CodeCombinitionViewModel(CodeCombinitionView entity)
        {
            this.AllowedPosting = entity.AllowedPosting;
            this.CodeCombinitionCode = entity.CodeCombinitionCode;
            ////this.CodeCombinitionName = entity.CodeCombinitionName;
            this.CompanyId = entity.CompanyId;
            this.EndDate = entity.EndDate;
            this.Id = entity.Id;
            this.SOBId = entity.SOBId;
            this.StartDate = entity.StartDate;
        }
        #endregion

        #region Properties
        public long Id { get; set; }

        public long SOBId { get; set; }

        public string SOBName { get; set; }

        public long CompanyId { get; set; }

        public string CodeCombinitionCode { get; set; }

        public string CodeCombinitionName { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool AllowedPosting { get; set; }
        #endregion
    }
}
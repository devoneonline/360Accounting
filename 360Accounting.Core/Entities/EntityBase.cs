﻿using _360Accounting.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class EntityBase
    {
        public Guid CreateBy
        {
            get;
            set;
        }

        public DateTime CreateDate { get; set; }

        public Guid? UpdateBy
        {
            get;
            set;
        }

        public DateTime? UpdateDate { get; set; }

        public virtual bool IsValid()
        {
            if (this.CreateBy == null)
            {
                ExceptionHelper.AddError("CreateBy", "Should not have null value!");
            }

            if (this.CreateDate == null || this.CreateDate == DateTime.MinValue)
            {
                this.CreateDate = DateTime.Now;
            }

            return true;
        }
    }
}

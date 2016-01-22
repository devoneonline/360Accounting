using _360Accounting.Common;
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
            get { return Guid.Parse("715C35AE-DD65-4B5D-ACB9-4572164A476C"); } ////TODO: Need to change when actual user will be available.
            set { }
        }

        public DateTime CreateDate { get; set; }

        public Guid UpdateBy
        {
            get { return Guid.Parse("715C35AE-DD65-4B5D-ACB9-4572164A476C"); } ////TODO: Need to change when actual user will be available.
            set { }
        }

        public DateTime? UpdateDate { get; set; }

        public virtual bool IsValid()
        {
            if (this.CreateBy == null)
            {
                ExceptionHelper.AddError("CreateBy", "Should not have null value!");
            }

            if (this.CreateDate == null)
            {
                this.CreateDate = DateTime.Now;
            }

            return true;
        }
    }
}

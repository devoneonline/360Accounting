using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class FeatureSetAccess
    {
        [Key]
        public long Id { get; set; }

        public long? CompanyId { get; set; }

        public Guid? UserId { get; set; }

        public long FeatureSetId { get; set; }

        public Guid CreateBy
        {
            get { return Guid.Parse("715C35AE-DD65-4B5D-ACB9-4572164A476C"); } ////TODO: Need to change when actual user will be available.
            set { }
        }

        public DateTime CreateDate { get; set; }

        public virtual bool IsValid()
        {
            if (this.CreateBy == null)
            {
                Common.ExceptionHelper.AddError("CreateBy", "Should not have null value!");
            }

            if (this.CreateDate == null)
            {
                this.CreateDate = DateTime.Now;
            }

            return true;
        }
    }
}

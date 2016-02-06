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

        public Guid CreateBy { get; set; }

        public DateTime CreateDate { get; set; }
    }
}

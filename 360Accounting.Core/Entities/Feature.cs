using _360Accounting.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class Feature : ICore360
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public long? ParentId { get; set; }

        public string Class { get; set; }

        public string Href { get; set; }

        public byte[] LastUpdated { get; set; }

        [ForeignKey("ParentId")]
        public List<Feature> Features { get; set; }

        public bool IsValid()
        {
            if (this.Name.Length > 255)
            {
                ExceptionHelper.AddError("FeatureName:", "Should less than 255 characters!");
                return false;
            }

            return true;
        }
    }
}

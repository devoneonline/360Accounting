using _360Accounting.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class Feature : ICore360
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public string Name { get; set; }

        public byte[] LastUpdated { get; set; }

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

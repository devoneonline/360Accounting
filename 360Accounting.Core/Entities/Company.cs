using _360Accounting.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class Company : ICore360
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsValid()
        {
            if (this.Name.Length > 30)
            {
                ExceptionHelper.AddError(
                    "Company Name: " + this.Name,
                    "Should be less than 30 characters!");
                return false;
            }

            return true;
        }
    }
}

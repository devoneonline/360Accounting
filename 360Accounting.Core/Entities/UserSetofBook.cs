using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class UserSetofBook
    {
        [Key]
        public long Id { get; set; }

        public long CompanyId { get; set; }

        public long SOBId { get; set; }

        public Guid UserId { get; set; }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}

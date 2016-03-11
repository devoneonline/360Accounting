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
    public class BankAccount : EntityBase
    {
        [Key]
        public long Id { get; set; }

        public long BankId { get; set; }

        public string AccountName { get; set; }

        public string AdditionalInformation { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public long Cash_CCID { get; set; }
        
        public long RemitCash_CCID { get; set; }

        public long Confirm_CCID { get; set; }
    }
}

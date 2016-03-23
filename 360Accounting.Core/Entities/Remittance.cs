using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class Remittance : EntityBase
    {
        [Key]
        public long Id { get; set; }
        public long SOBId { get; set; }
        public long BankId { get; set; }
        public long BankAccountId { get; set; }
        public long ReceiptId { get; set; }
        public string RemitNo { get; set; }
        public DateTime RemitDate { get; set; }

        public long CompanyId { get; set; }
    }
}

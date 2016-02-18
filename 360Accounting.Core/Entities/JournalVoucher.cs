using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class JournalVoucher : EntityBase
    {
        [Key]
        public long Id { get; set; }

        public long SOBId { get; set; }

        public long CompanyId { get; set; }

        public long PeriodId { get; set; }

        public string JournalName { get; set; }

        public string Description { get; set; }

        public DateTime GLDate { get; set; }

        public string DocumentNo { get; set; }

        public bool PostingFlag { get; set; }

        public long CurrencyId { get; set; }

        public decimal ConversionRate { get; set; }

        ////public List<JournalVoucherDetail> JournalVoucherDetail { get; set; }
    }

    public class UserwiseEntriesTrail
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public DateTime TransactionDate { get; set; }

        public string DocumentNo { get; set; }

        public string EntryType { get; set; }
    }

    public class AuditTrail
    {
        public DateTime TransactionDate { get; set; }

        public string Document { get; set; }

        public string Description { get; set; }

        public string PeriodName { get; set; }

        public string CurrencyName { get; set; }

        public decimal ConversionRate { get; set; }

        public string LineDescription { get; set; }

        public decimal Debit { get; set; }

        public decimal Credit { get; set; }

        public string CCSegment1 { get; set; }
        public string CCSegment2 { get; set; }
        public string CCSegment3 { get; set; }
        public string CCSegment4 { get; set; }
        public string CCSegment5 { get; set; }
        public string CCSegment6 { get; set; }
        public string CCSegment7 { get; set; }
        public string CCSegment8 { get; set; }
    }

    public class Ledger
    {
        public string CodeCombination { get; set; }

        public string CodeCombinationName { get; set; }

        public DateTime TransactionDate { get; set; }

        public string Document { get; set; }

        public string Description { get; set; }

        public decimal Debit { get; set; }

        public decimal Credit { get; set; }

        public decimal Balance { get; set; }
    }

    public class TrialBalance
    {
        public string CodeCombination { get; set; }

        public string CodeCombinationName { get; set; }

        public decimal Debit { get; set; }

        public decimal Credit { get; set; }
    }
}

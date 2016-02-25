using _360Accounting.Common;
using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Data.Repositories
{
    public class JournalVoucherRepository : Repository, IJournalVoucherRepository
    {
        public List<TrialBalance> TrialBalance(long companyId, long sobId, long fromCodeCombinationId, long toCodeCombinationId, long periodId)
        {
            List<TrialBalance> report = new List<TrialBalance>();
            DateTime fromDate = this.Context.Calendars.First(x => x.Id == periodId &&
                x.CompanyId == companyId && x.SOBId == sobId).EndDate;

            List<CodeCombinition> codeCombinations = this.Context.CodeCombinitions
                .Where(x => x.CompanyId == companyId && x.SOBId == sobId &&
                x.Id >= fromCodeCombinationId && x.Id <= toCodeCombinationId &&
                x.AllowedPosting == true).ToList();

            foreach (CodeCombinition codeCombination in codeCombinations)
            {
                var account = this.Context.Accounts.Where(x => x.CompanyId == codeCombination.CompanyId &&
                    x.SOBId == codeCombination.SOBId)
                    .Select(x => new
                    {
                        ChartId = x.Id,
                        Segment1 = x.SegmentName1,
                        Segment2 = x.SegmentName2,
                        Segment3 = x.SegmentName3,
                        Segment4 = x.SegmentName4,
                        Segment5 = x.SegmentName5,
                        Segment6 = x.SegmentName6,
                        Segment7 = x.SegmentName7,
                        Segment8 = x.SegmentName8
                    });
                decimal balance = getOpeningBalance(fromDate, codeCombination.Id);
                report.Add(new TrialBalance
                {
                    CodeCombination = Utility.Stringize(".", codeCombination.Segment1,
                    codeCombination.Segment2, codeCombination.Segment3, codeCombination.Segment4,
                    codeCombination.Segment5, codeCombination.Segment6, codeCombination.Segment7,
                    codeCombination.Segment8),
                    CodeCombinationName = Utility.Stringize(".", getCodeCombinationValueName(account.Single().ChartId, codeCombination.Segment1, account.Single().Segment1),
                    getCodeCombinationValueName(account.Single().ChartId, codeCombination.Segment2, account.Single().Segment2),
                    getCodeCombinationValueName(account.Single().ChartId, codeCombination.Segment3, account.Single().Segment3),
                    getCodeCombinationValueName(account.Single().ChartId, codeCombination.Segment4, account.Single().Segment4),
                    getCodeCombinationValueName(account.Single().ChartId, codeCombination.Segment5, account.Single().Segment5),
                    getCodeCombinationValueName(account.Single().ChartId, codeCombination.Segment6, account.Single().Segment6),
                    getCodeCombinationValueName(account.Single().ChartId, codeCombination.Segment7, account.Single().Segment7),
                    getCodeCombinationValueName(account.Single().ChartId, codeCombination.Segment8, account.Single().Segment8)),
                    Credit = balance > 0 ? 0 : balance,
                    Debit = balance < 0 ? 0 : balance
                });
            }

            return report;
        }

        private string getCodeCombinationValueName(long chartId, string value, string segment)
        {
            string valueName = "";
            if (value != null)
            {
                valueName = this.Context.AccountValues.FirstOrDefault(x => x.ChartId == chartId && x.Value == value
                    && x.Segment == segment).ValueName;
            }

            return valueName;
        }

        public List<Ledger> Ledger(long companyId, long sobId, long fromCodeCombinationId, long toCodeCombinationId, 
            DateTime fromDate, DateTime toDate)
        {
            decimal openingBalance;
            string codeCombinationCode;
            string codeCombinationName;
            List<Ledger> ledger = new List<Ledger>();

            //1. Get all combinations of this company, sobId
            // between (from - to) having allow posting = 1.
            List<CodeCombinition> codeCombinations = this.Context.CodeCombinitions
                .Where(x => x.CompanyId == companyId && x.SOBId == sobId &&
                x.Id >= fromCodeCombinationId && x.Id <= toCodeCombinationId &&
                x.AllowedPosting == true).ToList();

            //2. Loop of each combination
            foreach (CodeCombinition codeCombination in codeCombinations)
            {
                //To get Code Combination value name
                var account = this.Context.Accounts.Where(x => x.CompanyId == codeCombination.CompanyId &&
                    x.SOBId == codeCombination.SOBId)
                    .Select(x => new
                    {
                        ChartId = x.Id,
                        Segment1 = x.SegmentName1,
                        Segment2 = x.SegmentName2,
                        Segment3 = x.SegmentName3,
                        Segment4 = x.SegmentName4,
                        Segment5 = x.SegmentName5,
                        Segment6 = x.SegmentName6,
                        Segment7 = x.SegmentName7,
                        Segment8 = x.SegmentName8
                    });

                codeCombinationCode = Utility.Stringize(".", 
                    codeCombination.Segment1, 
                    codeCombination.Segment2, 
                    codeCombination.Segment3, 
                    codeCombination.Segment4, 
                    codeCombination.Segment5, 
                    codeCombination.Segment6, 
                    codeCombination.Segment7, 
                    codeCombination.Segment8);

                codeCombinationName = Utility.Stringize(".", getCodeCombinationValueName(account.Single().ChartId, codeCombination.Segment1, account.Single().Segment1),
                    getCodeCombinationValueName(account.Single().ChartId, codeCombination.Segment2, account.Single().Segment2),
                    getCodeCombinationValueName(account.Single().ChartId, codeCombination.Segment3, account.Single().Segment3),
                    getCodeCombinationValueName(account.Single().ChartId, codeCombination.Segment4, account.Single().Segment4),
                    getCodeCombinationValueName(account.Single().ChartId, codeCombination.Segment5, account.Single().Segment5),
                    getCodeCombinationValueName(account.Single().ChartId, codeCombination.Segment6, account.Single().Segment6),
                    getCodeCombinationValueName(account.Single().ChartId, codeCombination.Segment7, account.Single().Segment7),
                    getCodeCombinationValueName(account.Single().ChartId, codeCombination.Segment8, account.Single().Segment8));

                //OPENING BALANCE
                openingBalance = getOpeningBalance(fromDate, codeCombination.Id);

                //SELECT...
                //TransDate
                //Desc. Opening As On
                //SUM(Debit) - SUM(Credit) ==> Balance Dr(+ve), Cr(-ve)
                ledger.Add(new Ledger
                {
                    Balance = openingBalance,
                    CodeCombination = codeCombinationCode,
                    CodeCombinationName = codeCombinationName,
                    Credit = 0,
                    Debit = 0,
                    Description = "Opneing Balance",
                    Document = "",
                    TransactionDate = fromDate
                });
                
                //Read All transactions from glLines
                //of this code combination
                //having glDate between fromDate & toDate
                var ledgerTransactions = (from jv in this.Context.JournalVouchers
                                       join jvd in this.Context.JournalVoucherDetails on jv.Id equals jvd.HeaderId
                                       where jv.GLDate >= fromDate && jv.GLDate <= toDate &&
                                       jvd.CodeCombinationId == codeCombination.Id
                                       select new
                                       {
                                           Credit = jvd.AccountedCr,
                                           Debit = jvd.AccountedDr,
                                           Description = jvd.Description,
                                           Document = jv.DocumentNo,
                                           TransactionDate = jv.GLDate
                                       }).ToList();

                //Select ledgerTransactions thru loop in ledger...
                foreach (var transaction in ledgerTransactions)
                {
                    ledger.Add(new Ledger 
                    {
                        Balance = openingBalance + transaction.Debit - transaction.Credit,
                        CodeCombination = codeCombinationCode,
                        CodeCombinationName = codeCombinationName,
                        Credit = transaction.Credit,
                        Debit = transaction.Debit,
                        Description = transaction.Description,
                        Document = transaction.Document,
                        TransactionDate = transaction.TransactionDate
                    });

                    openingBalance = openingBalance + transaction.Debit - transaction.Credit;
                }
            }

            return ledger;
        }

        private decimal getOpeningBalance(DateTime openingDate, long codeCombinationId)
        {
            //1. Read all the transactions from glLines
            //of this code combination
            //having glDate less than fromDate.
            //FOR OPENING BALANCE
            var openingTransactions = (from jv in this.Context.JournalVouchers
                                       join jvd in this.Context.JournalVoucherDetails on jv.Id equals jvd.HeaderId
                                       where jv.GLDate < openingDate && jvd.CodeCombinationId == codeCombinationId
                                       select new
                                       {
                                           Credit = jvd.AccountedCr,
                                           Debit = jvd.AccountedDr
                                       }).ToList();

            //Make opening balance.
            return openingTransactions.Sum(x => x.Debit - x.Credit);
        }

        public List<AuditTrail> AuditTrail(long companyId, long sobId, DateTime fromDate, DateTime toDate)
        {
            var data = (from a in this.Context.JournalVouchers
                        join b in this.Context.JournalVoucherDetails on a.Id equals b.HeaderId
                        join c in this.Context.CodeCombinitions on b.CodeCombinationId equals c.Id
                        join d in this.Context.Currencies on a.CurrencyId equals d.Id
                        join e in this.Context.Calendars on a.PeriodId equals e.Id
                        where a.CompanyId == companyId && a.SOBId == sobId &&
                        a.GLDate >= fromDate && a.GLDate <= toDate
                        select new AuditTrail
                        {
                            CCSegment1 = c.Segment1,
                            CCSegment2 = c.Segment2,
                            CCSegment3 = c.Segment3,
                            CCSegment4 = c.Segment4,
                            CCSegment5 = c.Segment5,
                            CCSegment6 = c.Segment6,
                            CCSegment7 = c.Segment7,
                            CCSegment8 = c.Segment8,
                            ConversionRate = a.ConversionRate,
                            Credit = b.EnteredCr,
                            CurrencyName = d.CurrencyCode,
                            Debit = b.EnteredDr,
                            Description = a.Description,
                            Document = a.DocumentNo,
                            LineDescription = b.Description,
                            PeriodName = e.PeriodName,
                            TransactionDate = a.GLDate
                        }).ToList();
            return data;
        }

        public List<UserwiseEntriesTrail> UserwiseEntriesTrail(long companyId, long sobId, DateTime fromDate, DateTime toDate, Guid? userId)
        {
            ////Get new Entries
            var newEntries = (from a in this.Context.JournalVouchers
                        join b in this.Context.Users on a.CreateBy equals b.UserId
                        where a.CompanyId == companyId && a.SOBId == sobId &&
                        a.GLDate >= fromDate && a.GLDate <= toDate &&
                        a.CreateDate == a.UpdateDate
                        select new UserwiseEntriesTrail
                        {
                            UserId = a.CreateBy,
                            UserName = b.Username,
                            TransactionDate = a.GLDate,
                            DocumentNo = a.DocumentNo,
                            EntryType = "New"
                        }).ToList();
            
            if (userId != null)
            {
                newEntries = newEntries.Where(x => x.UserId == userId)
                    .Select(x => new UserwiseEntriesTrail
                    {
                        UserId = x.UserId,
                        UserName = x.UserName,
                        TransactionDate = x.TransactionDate,
                        DocumentNo = x.DocumentNo,
                        EntryType = x.EntryType
                    }).ToList();
            }

            ////Get edit Entries
            var editEntries = (from a in this.Context.JournalVouchers
                                   join b in this.Context.Users on a.UpdateBy equals b.UserId
                                   where a.CompanyId == companyId && a.SOBId == sobId &&
                                   a.GLDate >= fromDate && a.GLDate <= toDate &&
                                   a.CreateDate != a.UpdateDate
                                   select new UserwiseEntriesTrail
                                   {
                                        UserId = a.CreateBy,
                                        UserName = b.Username,
                                        TransactionDate = a.GLDate,
                                        DocumentNo = a.DocumentNo,
                                        EntryType = "Edit"
                                   }).ToList();

            if (userId != null)
            {
                editEntries = editEntries.Where(x => x.UserId == userId)
                    .Select(x => new UserwiseEntriesTrail
                    {
                        UserId = x.UserId,
                        UserName = x.UserName,
                        TransactionDate = x.TransactionDate,
                        DocumentNo = x.DocumentNo,
                        EntryType = x.EntryType
                    }).ToList();
            }

            List<UserwiseEntriesTrail> data = new List<UserwiseEntriesTrail>();
            foreach (UserwiseEntriesTrail record in newEntries)
            {
                data.Add(new UserwiseEntriesTrail
                {
                    DocumentNo = record.DocumentNo,
                    EntryType = record.EntryType,
                    TransactionDate = record.TransactionDate,
                    UserId = record.UserId,
                    UserName = record.UserName
                });
            }

            foreach (UserwiseEntriesTrail record in editEntries)
            {
                data.Add(new UserwiseEntriesTrail
                {
                    DocumentNo = record.DocumentNo,
                    EntryType = record.EntryType,
                    TransactionDate = record.TransactionDate,
                    UserId = record.UserId,
                    UserName = record.UserName
                });
            }

            return data;
        }

        public string Insert(JournalVoucherDetail entity)
        {
            this.Context.JournalVoucherDetails.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public IEnumerable<JournalVoucherDetail> GetAll(string headerId)
        {
            IEnumerable<JournalVoucherDetail> voucherDetailList = this.Context.JournalVoucherDetails.Where(x => x.HeaderId == Convert.ToInt32(headerId));
            return voucherDetailList;
        }

        public string Update(JournalVoucherDetail entity)
        {
            this.Context.JournalVoucherDetails.Attach(entity);
            this.Context.Entry(entity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }
        
        public IEnumerable<JournalVoucher> GetAll(long companyId, string searchText, bool paging, int page, string sort, string sortDir)
        {
            IEnumerable<JournalVoucher> voucherList = this.Context.JournalVouchers.Where(x => x.CompanyId == companyId);
            voucherList = sortDir.ToUpper() == "ASC" ?
                voucherList.OrderBy(x => x.SOBId) :
                voucherList.OrderByDescending(x => x.SOBId);

            if (!paging)
            {
                return voucherList;
            }
            else
            {
                var recordCount = voucherList.Count();
                return voucherList.Skip((page - 1) * 20)
                    .Take(20);
            }
        }

        public JournalVoucher GetSingle(string id, long companyId)
        {
            JournalVoucher voucher = this.GetAll(companyId)
                .FirstOrDefault(x => x.Id == Convert.ToInt32(id));
            return voucher;
        }

        public IEnumerable<JournalVoucher> GetAll(long companyId)
        {
            IEnumerable<JournalVoucher> voucherList = this.Context.JournalVouchers.Where(x => x.CompanyId == companyId);
            return voucherList;
        }

        public string Insert(JournalVoucher entity)
        {
            this.Context.JournalVouchers.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(JournalVoucher entity)
        {
            this.Context.JournalVouchers.Attach(entity);
            this.Context.Entry(entity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.JournalVouchers.Remove(this.GetSingle(id, companyId));
            this.Commit();
        }

        public int Count(long companyId)
        {
            return this.GetAll(companyId).Count();
        }

        public int Commit()
        {
            return this.Context.SaveChanges();
        }

        public void DeleteJvDetail(string id)
        {
            this.Context.JournalVoucherDetails.Remove
                (this.Context.JournalVoucherDetails
                .FirstOrDefault(x => x.Id == Convert.ToInt32(id)));
        }
    }
}

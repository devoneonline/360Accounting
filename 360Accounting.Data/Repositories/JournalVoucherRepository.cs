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
        public List<UserwiseEntriesTrial> UserwiseEntriesTrial(long companyId, long sobId, DateTime fromDate, DateTime toDate, Guid? userId)
        {
            ////Get new Entries
            var newEntries = (from a in this.Context.JournalVouchers
                        join b in this.Context.Users on a.CreateBy equals b.UserId
                        where a.CompanyId == companyId && a.SOBId == sobId &&
                        a.GLDate >= fromDate && a.GLDate <= toDate &&
                        a.CreateDate == a.UpdateDate
                        select new UserwiseEntriesTrial
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
                    .Select(x => new UserwiseEntriesTrial
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
                                   select new UserwiseEntriesTrial
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
                    .Select(x => new UserwiseEntriesTrial
                    {
                        UserId = x.UserId,
                        UserName = x.UserName,
                        TransactionDate = x.TransactionDate,
                        DocumentNo = x.DocumentNo,
                        EntryType = x.EntryType
                    }).ToList();
            }

            List<UserwiseEntriesTrial> data = new List<UserwiseEntriesTrial>();
            foreach (UserwiseEntriesTrial record in newEntries)
            {
                data.Add(new UserwiseEntriesTrial
                {
                    DocumentNo = record.DocumentNo,
                    EntryType = record.EntryType,
                    TransactionDate = record.TransactionDate,
                    UserId = record.UserId,
                    UserName = record.UserName
                });
            }

            foreach (UserwiseEntriesTrial record in editEntries)
            {
                data.Add(new UserwiseEntriesTrial
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
    }
}

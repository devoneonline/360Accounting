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
                return voucherList
                    .Select(x => new JournalVoucher
                    {
                        CompanyId = x.CompanyId,
                        ConversionRate = x.ConversionRate,
                        CreateBy = x.CreateBy,
                        CreateDate = x.CreateDate,
                        CurrencyId = x.CurrencyId,
                        Description = x.Description,
                        DocumentNo = x.DocumentNo,
                        GLDate = x.GLDate,
                        Id = x.Id,
                        JournalName = x.JournalName,
                        PeriodId = x.PeriodId,
                        PostingFlag = x.PostingFlag,
                        SOBId = x.SOBId,
                        UpdateBy = x.UpdateBy,
                        UpdateDate = x.UpdateDate
                    }).ToList()
                    .Skip((page - 1) * 20)
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

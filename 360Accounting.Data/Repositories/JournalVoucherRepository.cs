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

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
    public class SetOfBookRepository : Repository, ISetOfBookRepository
    {
        public SetOfBook GetSetOfBook(long companyId, string name)
        {
            SetOfBook sob = this.GetAll(companyId).FirstOrDefault(x => x.Name == name);
            return sob;
        }

        public List<SetOfBook> GetByCompanyId(long companyId)
        {
            List<SetOfBook> list = this.Context.SetOfBooks
                .Where(x => x.CompanyId == companyId).ToList();
            return list;
        }

        public SetOfBook GetSingle(string id, long companyId)
        {
            SetOfBook sob = this.GetAll(companyId).FirstOrDefault(x => x.Id == Convert.ToInt64(id));
            return sob;
        }

        public IEnumerable<SetOfBook> GetAll(long companyId)
        {
            IEnumerable<SetOfBook> sobList = this.Context.SetOfBooks.Where(x=> x.CompanyId == companyId);
            return sobList;
        }

        public string Insert(SetOfBook entity)
        {
            this.Context.SetOfBooks.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(SetOfBook entity)
        {
            this.Context.SetOfBooks.Attach(entity);
            this.Context.Entry(entity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.SetOfBooks.Remove(this.GetSingle(id,companyId));
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

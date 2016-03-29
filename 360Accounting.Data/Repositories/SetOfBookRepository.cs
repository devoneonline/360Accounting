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
            SetOfBook sob = this.Context.SetOfBooks.FirstOrDefault(x => x.Name == name && x.CompanyId == companyId);
            return sob;
        }

        public List<SetOfBook> GetByCompanyId(long companyId)
        {
            List<SetOfBook> list = this.Context.SetOfBooks.Where(x => x.CompanyId == companyId).ToList();
            return list;
        }

        public SetOfBook GetSingle(string id, long companyId)
        {
            long longId=Convert.ToInt64(id);

            SetOfBook sob = this.Context.SetOfBooks.FirstOrDefault(x => x.CompanyId == companyId && x.Id == longId);
            return sob;
        }

        public IEnumerable<SetOfBook> GetAll(long companyId)
        {
            IEnumerable<SetOfBook> sobList = this.Context.SetOfBooks.Where(x => x.CompanyId == companyId);
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
            var orignalEntity = this.Context.SetOfBooks.Find(entity.Id);
            this.Context.Entry(orignalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(orignalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.SetOfBooks.Remove(this.GetSingle(id, companyId));
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

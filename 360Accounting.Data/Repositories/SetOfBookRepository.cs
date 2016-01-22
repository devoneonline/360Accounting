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
        public List<SetOfBook> GetByCompanyId(long companyId)
        {
            List<SetOfBook> list = this.Context.SetOfBooks
                .Where(x => x.CompanyId == companyId).ToList();
            return list;
        }

        public SetOfBook GetSingle(string id)
        {
            SetOfBook sob = this.GetAll().FirstOrDefault(x => x.Id == Convert.ToInt64(id));
            return sob;
        }

        public IEnumerable<SetOfBook> GetAll()
        {
            IEnumerable<SetOfBook> sobList = this.Context.SetOfBooks;
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

        public void Delete(string id)
        {
            this.Context.SetOfBooks.Remove(this.GetSingle(id));
            this.Commit();
        }

        public int Count()
        {
            return this.GetAll().Count();
        }

        public int Commit()
        {
            return this.Context.SaveChanges();
        }
    }
}

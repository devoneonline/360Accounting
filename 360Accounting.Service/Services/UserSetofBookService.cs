using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _360Accounting.Service
{
    public class UserSetofBookService : IUserSetofBookService
    {
        private IUserSetofBookRepository repository;

        public UserSetofBookService(IUserSetofBookRepository repo)
        {
            this.repository = repo;
        }

        public UserSetofBook GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id,companyId);
        }

        public IEnumerable<UserSetofBook> GetAll(long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public string Insert(UserSetofBook entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(UserSetofBook entity)
        {
            return this.repository.Update(entity);
        }

        public void Delete(string id, long companyId)
        {
            this.repository.Delete(id,companyId);
        }

        public int Count(long companyId)
        {
            return this.repository.Count(companyId);
        }
    }
}

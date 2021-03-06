﻿using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace _360Accounting.Data.Repositories
{
    public class UserSetofBookRepository : Repository, IUserSetofBookRepository
    {
        public UserSetofBook GetSingle(string id, long companyId)
        {
            return this.GetAll(companyId).FirstOrDefault(x => x.UserId == Guid.Parse(id));
        }

        public IEnumerable<UserSetofBook> GetAll(long companyId)
        {
            return this.Context.UserSetofBooks.Where(rec => rec.CompanyId == companyId);
        }

        public string Insert(UserSetofBook entity)
        {
            this.Context.UserSetofBooks.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(UserSetofBook entity)
        {
            var originalEntity = this.Context.UserSetofBooks.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.UserSetofBooks.Remove(this.GetSingle(id, companyId));
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
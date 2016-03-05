using _360Accounting.Core;
using _360Accounting.Core.Interfaces;
using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Service
{
    public class GLLineService : IGLLineService
    {
        private IGLLineRepository repository;

        public GLLineService(IGLLineRepository repo)
        {
            this.repository = repo;
        }
        
        public GLLines GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id, companyId);
        }

        /// <summary>
        /// will Get all records based on given header id.
        /// </summary>
        /// <param name="companyId">enter HeaderId here</param>
        /// <returns></returns>
        public IEnumerable<GLLines> GetAll(long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public string Insert(GLLines entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(GLLines entity)
        {
            return this.repository.Update(entity);
        }

        public void Delete(string id, long companyId)
        {
            this.repository.Delete(id, companyId);
        }

        public int Count(long companyId)
        {
            return this.repository.Count(companyId);
        }

        public IList<GLLines> GetAll(long companyId, long headerId)
        {
            return this.repository.GetAll(companyId, headerId);
        }
    }
}

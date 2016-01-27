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
    public class CodeCombinitionRepository : Repository, ICodeCombinitionRepository
    {
        public IEnumerable<CodeCombinitionView> GetAll(long sobId, string searchText, bool paging, int page, string sort, string sortDir)
        {
            IEnumerable<CodeCombinition> codeCombList = 
                this.Context.CodeCombinitions.Where(x => x.SOBId == sobId);
            codeCombList = sortDir.ToUpper() == "ASC" ?
                codeCombList.OrderBy(x => x.SOBId) :
                codeCombList.OrderByDescending(x => x.SOBId);

            if (!paging)
            {
                return codeCombList.Select(x =>
                    GetCodeCombViewByCodeCombEntity(x)).ToList();
            }
            else
            {
                var recordCount = codeCombList.Count();
                return codeCombList.Select(x => GetCodeCombViewByCodeCombEntity(x))
                    .Skip((page - 1) * 20)
                    .Take(20);
            }
        }

        public CodeCombinition GetSingle(string id)
        {
            CodeCombinition codeCombinition = this.GetAll()
                .FirstOrDefault(x => x.Id == Convert.ToInt32(id));
            return codeCombinition;
        }

        public IEnumerable<CodeCombinition> GetAll()
        {
            IEnumerable<CodeCombinition> codeComblist = 
                this.Context.CodeCombinitions;
            return codeComblist;
        }

        public string Insert(CodeCombinition entity)
        {
            this.Context.CodeCombinitions.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(CodeCombinition entity)
        {
            this.Context.CodeCombinitions.Attach(entity);
            this.Context.Entry(entity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id)
        {
            this.Context.CodeCombinitions.Remove(this.GetSingle(id));
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

        #region Private Methods
        private CodeCombinitionView GetCodeCombViewByCodeCombEntity(CodeCombinition entity)
        {
            if (entity == null)
            {
                return null;
            }

            CodeCombinitionView obj = new CodeCombinitionView();
            obj.AllowedPosting = entity.AllowedPosting;
            obj.CodeCombinitionCode = Utility.Stringize(".", entity.Segment1, entity.Segment2, entity.Segment3, entity.Segment4, entity.Segment5, entity.Segment6, entity.Segment7, entity.Segment8);
            obj.CompanyId = entity.CompanyId;
            obj.EndDate = entity.EndDate;
            obj.Id = entity.Id;
            obj.SOBId = entity.SOBId;
            obj.StartDate = entity.StartDate;
            return obj;
        }
        #endregion
    }
}

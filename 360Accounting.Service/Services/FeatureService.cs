using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Service
{
    public class FeatureService : IFeatureService
    {
        private IFeatureRepository repository;
        private IFeatureSetRepository fsRepo;
        private IFeatureSetListRepository fslRepo;
        private IFeatureSetAccessRepository fsaRepo;

        public FeatureService(IFeatureRepository featureRepository, IFeatureSetRepository fsRepo, IFeatureSetListRepository fslRepo,
                    IFeatureSetAccessRepository fsaRepo)
        {
            this.repository = featureRepository;
            this.fsaRepo = fsaRepo;
            this.fslRepo = fslRepo;
            this.fsRepo = fsRepo;
        }

        public Feature GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id,companyId);
        }

        public IEnumerable<Feature> GetAll( long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public string Insert(Feature entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(Feature entity)
        {
            return this.repository.Update(entity);
        }

        public void Delete(string id, long companyId)
        {
            this.repository.Delete(id,companyId);
        }

        public int Count( long companyId)
        {
            return this.repository.Count(companyId);
        }

        public IEnumerable<Feature> GetMenuItemsByUserId(Guid userId)
        {
            return this.repository.GetMenuItemsByUserId(userId);
        }

        public IEnumerable<Feature> GetSuperAdminMenu()
        {
            return this.repository.GetSuperAdminMenu();
        }

        public void InsertCompanyFeatureSet(FeatureSet fs, FeatureSetAccess fsa)
        {
            var result = this.fsRepo.Insert(fs);
            
            long outValue = 0;
            if (long.TryParse(result, out outValue))    //faisal bhai sidha sidha convert.toint32 q nhi kia itni lambi q ki???
            {
                foreach (var f in fs.FeatureSetList)
                {
                    f.FeatureSetId = outValue;
                }
                fslRepo.Insert(fs.FeatureSetList.ToList());
                fsa.FeatureSetId = outValue;
                result = this.fsaRepo.Insert(fsa);
            }
        }

        public void UpdateCompanyFeatureSet(FeatureSet fs, IEnumerable<FeatureSetList> oldFeatureSetList)
        {
            foreach (var featureSetList in fs.FeatureSetList)
            {
                List<FeatureSetList> fls = oldFeatureSetList.Where(x => x.FeatureId == featureSetList.FeatureId && x.FeatureSetId == featureSetList.FeatureSetId)
                    .Select(a => new FeatureSetList()).ToList();

                if (fls.Count() == 0)
                {
                    string result = fslRepo.Insert(featureSetList);
                    featureSetList.Id = Convert.ToInt32(result);
                }
                else
                {
                    featureSetList.Id = fls.First().Id;
                }
            }

            foreach (var featureSetList in oldFeatureSetList)
            {
                List<FeatureSetList> fls = fs.FeatureSetList.Where(x => x.FeatureId == featureSetList.FeatureId && x.FeatureSetId == featureSetList.FeatureSetId)
                    .Select(a => new FeatureSetList()).ToList();

                if (fls.Count() == 0)
                {
                    fls = null;
                    fslRepo.Delete(featureSetList);
                    
                }
            }
            fsRepo.Update(fs);
        }
    }
}
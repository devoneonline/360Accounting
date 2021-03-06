﻿using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface IFeatureRepository : IRepository<Feature>
    {
        List<Feature> GetMenuItemsByUserId(Guid userId);
        List<Feature> GetSuperAdminMenu();
        List<Feature> GetAll(long companyId, string userRole, string accessType);
    }
}

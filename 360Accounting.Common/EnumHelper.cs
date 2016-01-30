using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Common
{
    public enum FeatureAccessType
    {
        company,
        user
    }

    public enum UserRoles
    {
        CompanyAdmin,
        SuperAdmin,
        User
    }

    public enum AccountTypes
    {
        Capital,
        Liabilities,
        Asset,
        Income,
        Expense
    }
}

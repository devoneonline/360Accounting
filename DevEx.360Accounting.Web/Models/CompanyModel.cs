using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DevEx_360Accounting_Web.Models
{
    public class CompanyModel
    {
        public CompanyModel()
        {
        }

        public CompanyModel(Company entity)
        {
            this.Id = entity.Id;
            this.Name = entity.Name;
        }

        public long Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
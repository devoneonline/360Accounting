using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DevEx_360Accounting_Web.Models
{
    public class SetOfBookModel
    {
        public SetOfBookModel()
        {
        }

        public SetOfBookModel(SetOfBook entity)
        {
            this.Id = entity.Id;
            this.CompanyId = entity.CompanyId;
            this.Name = entity.Name;
        }

        public long Id { get; set; }

        public long CompanyId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
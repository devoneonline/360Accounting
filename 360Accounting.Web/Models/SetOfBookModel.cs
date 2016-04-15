using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _360Accounting.Web.Models
{
    public class SetOfBookModel
    {
        public SetOfBookModel()
        {
        }

        public SetOfBookModel(SetOfBook entity)
        {
            if (entity != null)
            {
                this.Id = entity.Id;
                this.CompanyId = entity.CompanyId;
                this.Name = entity.Name;
            }
        }

        public long Id { get; set; }

        public long CompanyId { get; set; }

        [Required]
        [StringLength(255, MinimumLength=1)]
        [Display(Name = "Set of Book Name *")]
        public string Name { get; set; }
    }
}
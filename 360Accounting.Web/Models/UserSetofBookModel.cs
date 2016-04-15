using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Models
{
    public class UserSetofBookModel
    {
        public UserSetofBookModel()
        {
        }

        public UserSetofBookModel(UserSetofBook entity)
        {
            if (entity != null)
            {
                this.CompanyId = entity.CompanyId;
                this.Id = entity.Id;
                this.SOBId = entity.SOBId;
                this.UserId = entity.UserId;
            }
        }

        public long Id { get; set; }

        public long CompanyId { get; set; }

        [Display(Name="Set of Book")]
        public long SOBId { get; set; }

        public Guid UserId { get; set; }

        public List<SelectListItem> SetofBooks { get; set; }
    }
}
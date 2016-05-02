using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Models
{

    public class FeatureSetAccessModel
    {
        public long FeatureSetId { get; set; }
        public string[] SelectedUsers { get; set; }
        public string[] AvailableUsers { get; set; }
        public List<SelectListItem> SelectedUserList { get; set; }
        public List<SelectListItem> AvailableUserList { get; set; }

        public FeatureSetAccessModel(FeatureSetAccessList entity)
        {
            FeatureSetId = entity.FeatureSetId;
            SelectedUserList = entity.SelectedUser.Select(x => new SelectListItem { Text = x.Value, Value = x.Key.ToString() }).ToList(); ;
            AvailableUserList = entity.AvailableUser.Select(x => new SelectListItem { Text = x.Value, Value = x.Key.ToString() }).ToList();

            SelectedUsers = SelectedUserList.Select(x => x.Value).ToArray();
            AvailableUsers = AvailableUserList.Select(x=> x.Value).ToArray();
        }
    }


    public class FeatureViewModel
    {
        public FeatureViewModel(Core.Entities.Feature x)
        {
            this.Id = x.Id;
            this.Name = x.Name;
            this.ParentId = x.ParentId;
            this.Class = x.Class;
            this.Href = x.Href;

            if (x.Features != null)
            {
                this.Features = x.Features.Select(f => new FeatureViewModel(f)).ToList();
            }
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public long? ParentId { get; set; }

        public string Href { get; set; }

        public string Class { get; set; }

        public List<FeatureViewModel> Features { get; set; }
    }


    public class FeatureSetListModel
    {
        private string sortColumn = "Segments";
        private string sortDirection = "ASC";

        #region Properties

        public string SearchText { get; set; }

        public int? Page { get; set; }

        public int TotalRecords { get; set; }

        public string SortColumn
        {
            get { return sortColumn; }
            set { sortColumn = value; }
        }

        public string SortDirection
        {
            get { return sortDirection; }
            set { sortDirection = value; }
        }

        public List<FeatureSetModel> FeatureSet { get; set; }

        #endregion

    }

    public class FeatureSetModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string AccessType { get; set; }

        public FeatureSetModel()
        {

        }

        public FeatureSetModel(FeatureSet x)
        {
            this.Id = x.Id;
            this.Name = x.Name;
            this.AccessType = x.AccessType;
        }
    }
}
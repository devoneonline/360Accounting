using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevEx_360Accounting_Web.Models
{
    public class FeatureViewModel
    {
        public FeatureViewModel(Feature x)
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
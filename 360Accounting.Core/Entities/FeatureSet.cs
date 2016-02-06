using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _360Accounting.Core.Entities
{
    public class FeatureSet : EntityBase
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public string AccessType { get; set; }

        [ForeignKey("FeatureSetId")]
        public IEnumerable<FeatureSetList> FeatureSetList { get; set; }

    }
}

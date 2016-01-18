using System.ComponentModel.DataAnnotations;

namespace _360Accounting.Core.Entities
{
    public class FeatureSetList
    {
        [Key]
        public long Id { get; set; }

        public long FeatureSetId { get; set; }

        public long FeatureId { get; set; }
    }
}

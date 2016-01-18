using System.ComponentModel.DataAnnotations;

namespace _360Accounting.Core.Entities
{
    public class FeatureSet : EntityBase
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public string AccessType { get; set; }
    }
}

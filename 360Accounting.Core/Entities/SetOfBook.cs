using System.ComponentModel.DataAnnotations;

namespace _360Accounting.Core.Entities
{
    public class SetOfBook
    {
        [Key]
        public long Id { get; set; }

        public long CompanyId { get; set; }

        public string Name { get; set; }
    }
}

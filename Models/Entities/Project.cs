using System.ComponentModel.DataAnnotations.Schema;

namespace rendszerfejlesztes_beadando.Models.Entities
{
    public class Project
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int? WorkTime { get; set; }
        public int? Fee { get; set; }
        public int? ComponentsPrices { get; set; }
    }
}

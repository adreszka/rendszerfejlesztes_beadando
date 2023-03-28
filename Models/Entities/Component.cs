using System.ComponentModel.DataAnnotations.Schema;

namespace rendszerfejlesztes_beadando.Models.Entities
{
    public class Component
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int MaxCapacity { get; set; }
    }
}

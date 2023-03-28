using System.ComponentModel.DataAnnotations.Schema;

namespace rendszerfejlesztes_beadando.Models.Entities
{
    public class Status
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace rendszerfejlesztes_beadando.Models.Entities
{
    public class ProjectComponent
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int ComponentId { get; set; }
        public Component Component { get; set; }
        public int Quantity { get; set; }

    }
}

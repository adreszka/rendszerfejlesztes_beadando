using System.ComponentModel.DataAnnotations.Schema;

namespace rendszerfejlesztes_beadando.Models.Entities
{
    public class Log
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
    }
}

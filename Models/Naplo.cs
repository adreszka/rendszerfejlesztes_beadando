using System.ComponentModel.DataAnnotations.Schema;

namespace rendszerfejlesztes_beadando.Models
{
    public class Naplo
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NaploID { get; set; }
        public DateTime Datum { get; set; }
        public Projekt ProjektID { get; set; }
        public Statusz StatuszID { get; set; }
    }
}

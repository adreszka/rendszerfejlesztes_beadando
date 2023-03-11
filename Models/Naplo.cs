using System.ComponentModel.DataAnnotations.Schema;

namespace rendszerfejlesztes_beadando.Models
{
    public class Naplo
    {
        public int NaploID { get; set; }
        public DateTime Datum { get; set; }
        public Projekt ProjektID { get; set; }
        public Statusz StatuszID { get; set; }
    }
}

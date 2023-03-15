using System.ComponentModel.DataAnnotations.Schema;

namespace rendszerfejlesztes_beadando.Models
{
    public class Naplo
    {
        public int ID { get; set; }
        public DateTime Datum { get; set; }
        public int ProjektID { get; set; }
        public Projekt Projekt { get; set; }
        public int StatuszID { get; set; }
        public Statusz Statusz { get; set; }
    }
}

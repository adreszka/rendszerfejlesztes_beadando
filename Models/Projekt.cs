namespace rendszerfejlesztes_beadando.Models
{
    public class Projekt
    {
        public int ProjektID { get; set; }
        public string Helyszin { get; set; }
        public string Leiras { get; set; }
        public Megrendelo MegrendeloID { get; set; }
    }
}

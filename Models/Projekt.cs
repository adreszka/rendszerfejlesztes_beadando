namespace rendszerfejlesztes_beadando.Models
{
    public class Projekt
    {
        public int Id { get; set; }
        public string Helyszin { get; set; }
        public string Leiras { get; set; }
        public int MegrendeloId { get; set; }
        public Megrendelo Megrendelo { get; set; }
    }
}

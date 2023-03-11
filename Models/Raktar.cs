namespace rendszerfejlesztes_beadando.Models
{
    public class Raktar
    {
        public int Azonosito { get; set; } // 1. szám a sor, 2. szám az oszlop, 3. szám a szint
        public Alkatresz AlkatreszID { get; set; }
        public int Darabszam { get; set; }
    }
}

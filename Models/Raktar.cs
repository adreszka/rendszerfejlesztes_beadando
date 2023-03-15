namespace rendszerfejlesztes_beadando.Models
{
    public class Raktar
    {
        public int Id { get; set; } // 1. szám a sor, 2. szám az oszlop, 3. szám a szint
        public int AlkatreszId { get; set; }
        public Alkatresz Alkatresz { get; set; }
        public int Darabszam { get; set; }
    }
}

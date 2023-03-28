namespace rendszerfejlesztes_beadando.Models.Entities
{
    public class Storage
    {
        public int Id { get; set; } // 1. szám a sor, 2. szám az oszlop, 3. szám a szint
        public int ComponentId { get; set; }
        public Component Component { get; set; }
        public int Quantity { get; set; }
    }
}

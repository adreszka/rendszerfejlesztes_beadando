using rendszerfejlesztes_beadando.Models.Entities;

namespace rendszerfejlesztes_beadando.Models
{
    public class MissingComponents
    {
        public string Location { get; set; }
        public List<StoreComponent> MissingCompsFromProjects { get; set; }
        public List<StoreComponent> ReservedComponents { get; set; }
    }
}

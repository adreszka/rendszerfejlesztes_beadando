using rendszerfejlesztes_beadando.Models.Entities;

namespace rendszerfejlesztes_beadando.Models
{
    public class AllInformationAboutTheProject
    {
        public string StatusName { get; set; }
        //public ProjectModel Project { get; set; }
        //public List<StoreComponent> MissingCompsFromProjects { get; set; }
        //public List<StoreComponent> ReservedComponents { get; set; }
        //public CustomerModel Customer { get; set; }
        public List<PathData> ProjectComponents { get; set; }
    }
}

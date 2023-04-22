using rendszerfejlesztes_beadando.Data;
using rendszerfejlesztes_beadando.Models;

namespace rendszerfejlesztes_beadando.Repositories
{
    public class ProjectRepository
    {
        private readonly DataContext _context;
        public ProjectRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> addNewProject(NewProject parameters) 
        {
            
        }
    }
}

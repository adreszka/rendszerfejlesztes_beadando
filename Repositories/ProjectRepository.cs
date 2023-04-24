using Microsoft.EntityFrameworkCore;
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

        public async Task<ProjectStatus> GetProjectsWithStatus(string location)
        {
            var p = await _context.Projects.FirstAsync(p => p.Location == location);
            var log = await _context.Logs.OrderByDescending(l => l.Date).FirstAsync(l => l.ProjectId == p.Id);
            var status = await _context.Statuses.FirstAsync(s => s.Id == log.StatusId);
            var project = new ProjectStatus
            {
                StatusName = status.Name,
            };
            return project;
        }
    }
}

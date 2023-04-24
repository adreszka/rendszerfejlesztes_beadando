using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rendszerfejlesztes_beadando.Data;
using rendszerfejlesztes_beadando.Models;
using rendszerfejlesztes_beadando.Models.Entities;

namespace rendszerfejlesztes_beadando.Repositories
{
    public class ProjectRepository
    {
        private readonly DataContext _context;
        private readonly IMapper mapper;
        public ProjectRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        public async Task<bool> addNewProject(NewProject parameters)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c =>
            c.Name == parameters.Name && c.PhoneNumber == parameters.PhoneNumber);
            if (customer == null)
            {
                var c = new Customer();
                if (parameters.TaxNumber == "")
                {
                    c.Name = parameters.Name;
                    c.PhoneNumber = parameters.PhoneNumber;
                    c.Email = parameters.Email;
                }
                else
                {
                    c.Name = parameters.Name;
                    c.PhoneNumber = parameters.PhoneNumber;
                    c.Email = parameters.Email;
                    c.TaxNumber = parameters.TaxNumber;
                }
                await _context.AddAsync(c);
            }
            await _context.SaveChangesAsync();

            if (customer == null)
            {
                customer = await _context.Customers.FirstAsync(c =>
                c.Name == parameters.Name && c.PhoneNumber == parameters.PhoneNumber);
            }
            var project = new Project
            {
                Location = parameters.Location,
                Description = parameters.Description,
                CustomerId = customer.Id,
            };
            await _context.AddAsync(project);
            await _context.SaveChangesAsync();

            var p = await _context.Projects.FirstAsync(p => p.Location == parameters.Location);
            var statuses = await _context.Statuses.FirstAsync(s => s.Name == "New");
            var log = new Log
            {
                ProjectId = project.Id,
                StatusId = statuses.Id,
            };
            await _context.AddAsync(log);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddWorkTimeAndFee(WorkTimeAndFee timeandfee)
        {
            var projects = await _context.Projects.FirstAsync(p => p.Location == timeandfee.Location);
            projects.WorkTime = timeandfee.Worktime;
            projects.Fee = timeandfee.Fee;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddComponentToProject(AddComponentToProject component)
        {
            var projects = await _context.Projects.FirstAsync(p => p.Location == component.Location);
            var components = await _context.Components.FirstAsync(c => c.Name == component.Name);
            var check = await _context.ProjectsComponents.FirstOrDefaultAsync(h => h.ProjectId == projects.Id && h.ComponentId == components.Id);
            if (check != null)
            {
                check.Quantity += component.Quantity;
            }
            else
            {
                var projectComponents = new ProjectComponentModel
                {
                    ProjectId = projects.Id,
                    ComponentId = components.Id,
                    Quantity = component.Quantity,
                };
                await _context.AddAsync(mapper.Map<ProjectComponent>(projectComponents));
                var statuses = await _context.Statuses.FirstAsync(s => s.Name == "Draft");
                var log = await _context.Logs.FirstOrDefaultAsync(g => g.ProjectId == projects.Id && g.StatusId == statuses.Id);
                if (log == null)
                {
                    var logs = new Log
                    {
                        ProjectId = projects.Id,
                        StatusId = statuses.Id,
                    };
                    await _context.AddAsync(logs);
                }
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Project>> GetProjects()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<StoreComponent>>> GetProjectComponents(string location)
        {
            var project = await _context.Projects.FirstAsync(p => p.Location == location);
            var projectComponents = await _context.ProjectsComponents.Where(pc => pc.ProjectId == project.Id).ToListAsync();
            List<StoreComponent> storeComponents = new List<StoreComponent>();
            foreach (var projectComponent in projectComponents) 
            {
                var component = await _context.Components.FirstAsync(c => c.Id == projectComponent.ComponentId);
                var components = new StoreComponent
                {
                    Name = component.Name,
                    Quantity = projectComponent.Quantity,
                };
                storeComponents.Add(components);
            }
            return storeComponents;
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

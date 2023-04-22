using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rendszerfejlesztes_beadando.Data;
using rendszerfejlesztes_beadando.Models;
using rendszerfejlesztes_beadando.Models.Entities;
using System.Linq;

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
            var customer = _context.Customers.FirstOrDefault(c =>
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
                _context.Add(c);
            }
            await _context.SaveChangesAsync();

            if (customer == null)
            {
                customer = _context.Customers.FirstOrDefault(c =>
                c.Name == parameters.Name && c.PhoneNumber == parameters.PhoneNumber);
            }
            var project = new Project
            {
                Location = parameters.Location,
                Description = parameters.Description,
                CustomerId = customer.Id,
            };
            _context.Add(project);

            var p = _context.Projects.FirstOrDefault(p => p.Location == parameters.Location);
            var statuses = _context.Statuses.FirstOrDefault(s => s.Name == "New");
            var log = new Log
            {
                ProjectId = project.Id,
                StatusId = statuses.Id,
            };
            _context.Add(log);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddWorkTimeAndFee(WorkTimeAndFee timeandfee)
        {
            var projects = _context.Projects.FirstOrDefault(p => p.Location == timeandfee.Location);
            projects.WorkTime = timeandfee.Worktime;
            projects.Fee = timeandfee.Fee;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddComponentToProject(AddComponentToProject component)
        {
            var projects = _context.Projects.FirstOrDefault(p => p.Location == component.Location);
            var components = _context.Components.FirstOrDefault(c => c.Name == component.Name);
            var check = _context.ProjectsComponents.FirstOrDefault(h => h.ProjectId == projects.Id && h.ComponentId == components.Id);
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
                _context.Add(mapper.Map<ProjectComponent>(projectComponents));
                var statuses = _context.Statuses.FirstOrDefault(s => s.Name == "Draft");
                var log = _context.Logs.FirstOrDefault(g => g.ProjectId == projects.Id && g.StatusId == statuses.Id);
                if (log == null)
                {
                    var logs = new Log
                    {
                        ProjectId = projects.Id,
                        StatusId = statuses.Id,
                    };
                    _context.Add(logs);
                }
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Project>> GetProjects()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<ProjectComponent>>> GetProjectComponents()
        {
            return await _context.ProjectsComponents.ToListAsync();
        }
    }
}
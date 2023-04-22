using rendszerfejlesztes_beadando.Data;
using rendszerfejlesztes_beadando.Models;
using rendszerfejlesztes_beadando.Models.Entities;
using System.Linq;

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
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

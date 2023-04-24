using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rendszerfejlesztes_beadando.Data;
using rendszerfejlesztes_beadando.Models;
using rendszerfejlesztes_beadando.Models.Entities;

namespace rendszerfejlesztes_beadando.Repositories
{
    public class ComponentRepository
    {
        private readonly DataContext _context;
        public ComponentRepository(DataContext context)
        {
            _context = context;
        }

        // Megnézi, hogy az adatbázis tartalmazza-e az adott alkatrészt ha igen akkor vissza ad egy hamisat
        // ha pedig nem tartalmazza akkor egy igazat és közben elmenti az adatbázisba
        // Ha true-t ad vissza azt jelenti, hogy sikerült elmenteni az adatbázisba
        // ha pedig false akkor nem
        public async Task<bool> Add(Component parameters)
        {
            var component = await _context.Components.FirstOrDefaultAsync(p => p.Name == parameters.Name);
            if (component != null)
            {
                return false;
            }

            await _context.AddAsync(parameters);
            await _context.SaveChangesAsync();
            return true;
        }

        // Megnézi, hogy az alkatrész szerepel-e az adatbázisba és ha szerepel akkor 
        // megváltoztatja az árát annak az alkatrésznek és elmenti, ha pedig nem akkor
        // dob egy exceptiont
        public async Task<bool> Update(ComponentModel comp)
        {
            var component = await _context.Components.FirstOrDefaultAsync(p => p.Name == comp.Name);
            if (component == null)
            {
                return false;
            }

            component.Price = comp.Price;
            await _context.SaveChangesAsync();
            return true;
        }

        // Visszaadja az összes alkatrészt az adatbázisból lista formájában
        public async Task<IEnumerable<Component>> GetAll() 
        {
            return await _context.Components.ToListAsync();
        }

        public async Task<AvailableComponent> GetAvailableComponent(string componentName)
        {
            int componentQuantityInStorage = 0;
            int reservedComponentQuantity = 0;
            var component = await _context.Components.FirstAsync(p => p.Name == componentName);
            var storage = await _context.Storage.ToListAsync();
            foreach (var row in storage)
            {
                if (row.ComponentId == null)
                {
                    break;
                }
                if (row.ComponentId == component.Id)
                {
                    componentQuantityInStorage += (int)row.Quantity;
                }
            }
            var reserved = await _context.ProjectsComponents.ToListAsync();
            foreach (var r in reserved)
            {
                if (r.ComponentId == component.Id)
                {
                    reservedComponentQuantity += (int)r.Quantity;
                }
            }
            var availableComponent = new AvailableComponent
            {
                Price = component.Price,
                AvailableQuantity = componentQuantityInStorage - reservedComponentQuantity,
            };
            return availableComponent;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using rendszerfejlesztes_beadando.Data;
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
            var component = _context.Components.FirstOrDefault(p => p.Name == parameters.Name);
            if (component != null)
            {
                return false;
            }

            _context.Add(parameters);
            await _context.SaveChangesAsync();
            return true;
        }

        // Megnézi, hogy az alkatrész szerepel-e az adatbázisba és ha szerepel akkor 
        // megváltoztatja az árát annak az alkatrésznek és elmenti, ha pedig nem akkor
        // dob egy exceptiont
        public async Task<bool> Update(string name, int price)
        {
            var component = _context.Components.FirstOrDefault(p => p.Name == name);

            if (component == null)
            {
                return false;
            }

            component.Price = price;
            await _context.SaveChangesAsync();
            return true;
        }

        // Visszaadja az összes alkatrészt az adatbázisból lista formájában
        public async Task<IEnumerable<Component>> GetAll() 
        {
            return await _context.Components.ToListAsync();
        }
    }
}

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
        public async Task Add(Component parameters)
        {
            var component = _context.Components.FirstOrDefault(p => p.Name == parameters.Name);
            if (component != null)
            {
                // jelenleg nem tudom hogyan kéne lekezelni
                throw new Exception();
            }

            _context.Add(parameters);
            await _context.SaveChangesAsync();
        }

        public async Task Update(string name, int price)
        {
            var component = _context.Components.FirstOrDefault(p => p.Name == name);

            if (component == null)
            {
                throw new Exception();
            }

            component.Price = price;
            await _context.SaveChangesAsync();
        }
    }
}

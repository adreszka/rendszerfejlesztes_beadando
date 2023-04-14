using rendszerfejlesztes_beadando.Data;
using rendszerfejlesztes_beadando.Models;

namespace rendszerfejlesztes_beadando.Repositories
{
    public class StorageRepository
    {
        private readonly DataContext _context;
        public StorageRepository(DataContext context)
        {
            _context = context;
        }

        // Eltárolja az adatbázisban az alkatrészt
        public async Task<bool> StoreComponent(StoreComponent parameters)
        {
            var storage = _context.Storage.FirstOrDefault(s =>
            s.Row == parameters.Row && s.Columnn == parameters.Column && s.Level == parameters.Level);
            if (storage == null)
            {
                return false;
            }
            
            var comp = _context.Components.FirstOrDefault(c => c.Name == parameters.Name);
            if (comp == null)
            {
                return false;
            }
            int storedQuantity = 0;
            if (storage.Quantity != null)
            {
                storedQuantity = (int)storage.Quantity;
            }
            if (storedQuantity + parameters.Quantity > comp.MaxCapacity) 
            {
                return false;
            }

            if (storage.ComponentId != null && (storage.ComponentId != comp.Id))
            {
                return false;
            }
            if (storage.ComponentId == null)
            {
                storage.ComponentId = comp.Id;
                storage.Quantity = parameters.Quantity + storedQuantity;
            }
            else 
            {
                storage.Quantity = parameters.Quantity + storedQuantity;
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}

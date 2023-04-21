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
            var comp = _context.Components.FirstOrDefault(c =>
                            c.Name == parameters.Name);
            int storedQuantity;
            bool componentIsStored = false;
            for (int i = 1; i <= 10; i++)
            { 
                for (int j = 1; j <= 4; j++)
                {
                    for (int k = 1; k <= 6; k++)
                    {
                        storedQuantity = 0;
                        var storage = _context.Storage.FirstOrDefault(s =>
                        s.Row == i && s.Columnn == j && s.Level == k);
                        if (storage.ComponentId != comp.Id && storage.ComponentId != null) 
                        {
                            continue;
                        }
                        if (storage.Quantity != null)
                        {
                            storedQuantity = (int)storage.Quantity;
                        }
                        if (storedQuantity + parameters.Quantity > comp.MaxCapacity)
                        {
                            continue;
                        }
                        if (storage.ComponentId == null)
                        {
                            storage.ComponentId = comp.Id;
                            storage.Quantity = storedQuantity + parameters.Quantity;
                            componentIsStored = true;
                        }
                        else 
                        {
                            storage.Quantity = parameters.Quantity + storedQuantity;
                            componentIsStored = true;
                        }
                        if (componentIsStored) break;
                    }
                    if (componentIsStored) break;
                }
                if (componentIsStored) break;
            }

            if (!componentIsStored) 
            {
                return false;
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}

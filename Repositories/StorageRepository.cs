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
        public async Task<int> StoreComponent(StoreComponent parameters)
        {
            var comp = _context.Components.FirstOrDefault(c =>
                            c.Name == parameters.Name);
            int storedQuantity;
            int quantity = parameters.Quantity;
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
                        if (storage.ComponentId == null)
                        {
                            if (storedQuantity + quantity > comp.MaxCapacity && storedQuantity == 0)
                            {
                                storage.ComponentId = comp.Id;
                                storage.Quantity = comp.MaxCapacity;
                                quantity -= comp.MaxCapacity;
                            }
                            else if (storedQuantity + quantity > comp.MaxCapacity && storedQuantity != 0) 
                            {
                                storage.ComponentId = comp.Id;
                                storage.Quantity = comp.MaxCapacity;
                                quantity -= comp.MaxCapacity - storedQuantity;
                            }
                            else
                            {
                                storage.ComponentId = comp.Id;
                                storage.Quantity = storedQuantity + quantity;
                                quantity = 0;
                            }
                        }
                        else 
                        {
                            if (storedQuantity + quantity > comp.MaxCapacity && storedQuantity == 0)
                            {
                                storage.Quantity = comp.MaxCapacity;
                                quantity -= comp.MaxCapacity;
                            }
                            else if (storedQuantity + quantity > comp.MaxCapacity && storedQuantity != 0)
                            {
                                storage.Quantity = comp.MaxCapacity;
                                quantity -= comp.MaxCapacity - storedQuantity;
                            }
                            else
                            {
                                storage.Quantity = quantity + storedQuantity;
                                quantity = 0;
                            }
                        }
                        if (quantity == 0) break;
                    }
                    if (quantity == 0) break;
                }
                if (quantity == 0) break;
            }

            await _context.SaveChangesAsync();

            if (quantity > 0) 
            {
                return quantity;
            }

            return quantity;
        }
    }
}

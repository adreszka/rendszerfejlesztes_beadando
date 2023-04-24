using Microsoft.EntityFrameworkCore;
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
            var comp = await _context.Components.FirstAsync(c =>
                            c.Name == parameters.Name);
            int storedQuantity;
            int quantity = parameters.Quantity;
            var storage = await _context.Storage.ToListAsync();
            foreach (var row in storage)
            {
                storedQuantity = 0;
                if (row.ComponentId != comp.Id && row.ComponentId != null)
                {
                    continue;
                }
                if (row.Quantity != null)
                {
                    storedQuantity = (int)row.Quantity;
                }
                if (row.ComponentId == null)
                {
                    if (storedQuantity + quantity > comp.MaxCapacity && storedQuantity == 0)
                    {
                        row.ComponentId = comp.Id;
                        row.Quantity = comp.MaxCapacity;
                        quantity -= comp.MaxCapacity;
                    }
                    else if (storedQuantity + quantity > comp.MaxCapacity && storedQuantity != 0)
                    {
                        row.ComponentId = comp.Id;
                        row.Quantity = comp.MaxCapacity;
                        quantity -= comp.MaxCapacity - storedQuantity;
                    }
                    else
                    {
                        row.ComponentId = comp.Id;
                        row.Quantity = storedQuantity + quantity;
                        quantity = 0;
                    }
                }
                else
                {
                    if (storedQuantity + quantity > comp.MaxCapacity && storedQuantity == 0)
                    {
                        row.Quantity = comp.MaxCapacity;
                        quantity -= comp.MaxCapacity;
                    }
                    else if (storedQuantity + quantity > comp.MaxCapacity && storedQuantity != 0)
                    {
                        row.Quantity = comp.MaxCapacity;
                        quantity -= comp.MaxCapacity - storedQuantity;
                    }
                    else
                    {
                        row.Quantity = quantity + storedQuantity;
                        quantity = 0;
                    }
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

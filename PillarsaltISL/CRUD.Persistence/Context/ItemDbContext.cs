using CRUD.Model;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Context
{
    public class ItemDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }

        public ItemDbContext(DbContextOptions<ItemDbContext> options) : base(options) { }
    }
}

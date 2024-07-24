using CRUD.Domain.Entities;
using CRUD.Model;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Context
{
    public class ItemDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<ScratchCard> ScratchCards { get; set; }

        public ItemDbContext(DbContextOptions<ItemDbContext> options) : base(options) { }
    }
}

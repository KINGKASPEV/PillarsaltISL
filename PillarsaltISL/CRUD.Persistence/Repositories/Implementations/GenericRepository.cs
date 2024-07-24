using CRUD.Context;
using CRUD.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Persistence.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
       
            private readonly ItemDbContext _context;

            public GenericRepository(ItemDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<T>> GetAll() => await _context.Set<T>().ToListAsync();
            public async Task<T> GetById(int id) => await _context.Set<T>().FindAsync(id);
            public async Task Add(T entity) => await _context.AddAsync(entity);
            public async Task Update(T entity) => _context.Update(entity);
            public async Task Delete(int id)
            {
                var entity = await GetById(id);
                if (entity != null) _context.Remove(entity);
            }
            public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

    }
}

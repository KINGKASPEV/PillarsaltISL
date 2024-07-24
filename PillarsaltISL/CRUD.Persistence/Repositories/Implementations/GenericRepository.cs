using CRUD.Context;
using CRUD.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Persistence.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
       
            private readonly ItemDbContext _context;
            private readonly DbSet<T> _dbSet;

            public GenericRepository(ItemDbContext context)
            {
                _context = context;
                _dbSet = _context.Set<T>();
            }

            public async Task<IEnumerable<T>> GetAll() => await _dbSet.ToListAsync();
            public async Task<T> GetById(int id) => await _dbSet.FindAsync(id);
            public async Task Add(T entity) => await _dbSet.AddAsync(entity);
            public async Task Update(T entity) => _dbSet.Update(entity);
            public async Task Delete(int id)
            {
                var entity = await GetById(id);
                if (entity != null) _dbSet.Remove(entity);
            }
        
    }
}

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Walle.Core.Interfaces.Repositories;
using Walle.Core.SharedKernel;

namespace Walle.Infrastructure.Persistence.Repositories
{
    public class EfRepository<T> : IRepository<T> where T : AggregateRoot
    {
        protected readonly AppDbContext _dbContext;

        public EfRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> SingleAsync(int id)
        {
            return await _dbContext.Set<T>().SingleAsync(e => e.Id == id);
        }

        public async Task<List<T>> ToListAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity).State = EntityState.Modified;
        }

        public async Task RemoveAsync(int id)
        {
            var entity = await _dbContext.Set<T>().SingleAsync(e => e.Id == id);
            _dbContext.Set<T>().Remove(entity);
        }

    }
}

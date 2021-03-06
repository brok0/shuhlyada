using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shukhlyada.Infrastructure.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shukhlyada.Infrastructure.Repositories
{
    public abstract class BaseRepository<T, TKey> : IRepository<T, TKey> where T : class, Shukhlyada.Domain.Abstractions.IEntity<TKey>
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        public abstract IUnitOfWork UnitOfWork { get; }

        public BaseRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }


        public virtual async Task<T> GetByIdAsync(TKey id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public T Insert(T item)
        {
            var insertedItem = _dbSet.Add(item).Entity;
            return insertedItem;
        }

        public void Update(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(T item)
        {
            _dbSet.Remove(item);
        }

        public async Task<IEnumerable<T>> GetAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }

        public async Task<T> GetSingleAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> specification)
        {
            var evaluator = new SpecificationEvaluator(); //SpecificationEvaluator()<T> - old version
            return evaluator.GetQuery(_dbSet, specification);
        }
    }
}

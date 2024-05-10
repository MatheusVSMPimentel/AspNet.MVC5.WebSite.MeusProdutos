using MatheusVSMP.Business.Core.Data;
using MatheusVSMP.Business.Core.Entities;
using MatheusVSMP.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MatheusVSMP.Infra.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly SqlServerContext _db;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository()
        {
            _db = new SqlServerContext();
            _dbSet = _db.Set<TEntity>();
        }
        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<TEntity> ObterPorId(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<List<TEntity>> ObterTodos()
        {
            return await _dbSet.ToListAsync();
        }
        public virtual async Task Adicionar(TEntity entity)
        {
            _dbSet.Add(entity);
            await SaveChanges();
        }

        public async Task Atualizar(TEntity entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            await SaveChanges();
        }
        public virtual async Task Remover(Guid id)
        {
            _db.Entry(new TEntity { Id = id }).State = EntityState.Deleted;
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}

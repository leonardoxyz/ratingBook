using Microsoft.EntityFrameworkCore;
using ratingBook.Core;
using ratingBook.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ratingBook.Repository
{
    public class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        protected readonly DataContext _context;
        protected readonly DbSet<TEntity> _entity;

        public DataContext UnitOfWork => _context;

        public RepositoryBase(DataContext applicationDataContext)
        {
            _context = applicationDataContext;
            _entity = _context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _entity.Add(entity);
        }

        public async Task<IQueryable<TEntity>> GetAll()
        {
            return await Task.FromResult(_entity.AsQueryable());
        }

        public async Task<TEntity> GetById(TKey id)
        {
            return await _entity.FindAsync(id) ?? throw new Exception("Erro, verifique as informações.");
        }

        public void Update(TEntity entity)
        {
            _entity.Update(entity);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

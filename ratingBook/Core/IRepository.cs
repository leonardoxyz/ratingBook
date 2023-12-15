namespace ratingBook.Core
{
    public interface IRepository<TEntity, TKey> : IDisposable where TEntity : class
    {
        void Add(TEntity entity);
        Task<TEntity> GetById(TKey id);
        void Update(TEntity entity);
        Task<IQueryable<TEntity>> GetAll();
    }
}

namespace BusinessLogic.Repositories
{
    public interface IExtendedRepository<TEntity>
    {
        Task<List<TEntity>> ExecuteQuery(IQueryable<TEntity> query);
        IQueryable<TEntity> GetQueryable();
    }
}

namespace BusinessLogic.Repositories
{
    public interface IRepository<TEntity, TCreate, TEdit> 
    {
        Task<TEntity> Create(TCreate newModel);
        Task<TEntity?> GetById(int id);
        Task<List<TEntity>> GetAll();
        Task<TEntity> Update(TEdit updateModel);
        Task<bool> Delete(int id);        
    }
}

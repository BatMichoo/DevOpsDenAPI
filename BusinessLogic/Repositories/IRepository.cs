namespace BusinessLogic.Repositories
{
    public interface IRepository<TModel, TCreate, TEdit> 
    {
        Task<TModel> Create(TCreate newModel);
        Task<TModel?> GetById(int id);
        Task<List<TModel>> GetAll();
        Task<TModel> Update(TEdit updateModel);
        Task<bool> Delete(int id);
    }
}

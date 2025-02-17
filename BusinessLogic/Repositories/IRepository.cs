using BusinessLogic.Models;

namespace BusinessLogic.Repositories
{
    public interface IRepository<T> where T : BaseModel
    {
        T Create(T newModel);
        T GetById(int id);
        List<T> GetAll();
        T Update(T updateModel);
        bool Delete(int id);
    }
}

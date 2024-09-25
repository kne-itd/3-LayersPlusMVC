
namespace BLL
{
    public interface IGenericRepository <T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        bool Insert(T t);
        bool Delete(int id);
        
        bool Update(T t);
    }
}
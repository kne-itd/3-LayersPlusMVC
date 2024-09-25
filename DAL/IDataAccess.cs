using DAL.Models;

namespace DAL
{
    public interface IDataAccess <T> where T : class
    {
        bool Create(T t);
        bool Delete(int id);
        IEnumerable<T> GetAll();
        T GetById(int id);
        bool Update(T t);
    }
}
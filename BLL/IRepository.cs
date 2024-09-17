
namespace BLL
{
    public interface IRepository
    {
        bool Create(Patient patient);
        bool Delete(int id);
        List<Patient> Read();
        bool Update(Patient patient);
    }
}
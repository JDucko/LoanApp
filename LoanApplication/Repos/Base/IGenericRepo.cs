
namespace LoanApplication.Repos.Base;

public interface IGenericRepo<T> where T : class
{
    IEnumerable<T> GetAll();
    Task<IEnumerable<T>> GetAllAsync();
    T GetById(int id);
    Task<T?> GetByIdAsync(int id);
    void Add(in T sender);
    int Save();
    Task<int> SaveAsync();
}

namespace LoanApplication.Repos.Base;

using System.Linq.Expressions;

using LoanApplication.Entities;

public interface IRepoBase<T, TId> where T : class, IEntity<TId>
{
    void Add(T objModel);
    Task<T> AddAsync(T objModel);
    T? GetById(TId id);
    Task<T?> GetByIdAsync(TId id);
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
    IEnumerable<T> GetAll();
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicate);
    void Update(T objModel);
    Task<bool> UpdateAsync(T objModel);
    void Remove(T objModel);
    void Dispose();
}
namespace LoanApplication.Repos.Base;

public class RepoBase<TEntity, TId> : IRepoBase<TEntity, TId> where TEntity : class, IEntity<TId>
{
    protected readonly Context _context;

    public RepoBase(Context context)
    {
        _context = context;
    }

    public void Add(TEntity model)
    {
        _context.Set<TEntity>().Add(model);
        _context.SaveChanges();
    }

    public async Task<TEntity> AddAsync(TEntity model)
    {
        var entry = await _context.Set<TEntity>().AddAsync(model);
        await _context.SaveChangesAsync();
        return entry.Entity;
    }

    public TEntity? GetById(TId id)
    {
        return _context.Set<TEntity>().Find(id);
    }

    public async Task<TEntity?> GetByIdAsync(TId id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await Task.Run(() => _context.Set<TEntity>().Where<TEntity>(predicate));
    }

    public IEnumerable<TEntity> GetAll()
    {
        return _context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await Task.Run(() => _context.Set<TEntity>());
    }

    public void Update(TEntity objModel)
    {
        _context.Entry(objModel).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public async Task<bool> UpdateAsync(TEntity objModel)
    {
        var existing = await _context.Set<TEntity>().FindAsync(objModel.Id);
        if (existing == null) return false;

        _context.Entry(existing).CurrentValues.SetValues(objModel);
        await _context.SaveChangesAsync();
        return true;
    }

    public void Remove(TEntity objModel)
    {
        _context.Set<TEntity>().Remove(objModel);
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
using Microsoft.EntityFrameworkCore;
using Patrimonium.Domain.Entities;
using Patrimonium.Domain.Interfaces;

public abstract class BaseCrudUseCase<T> where T : AuditableEntity
{
    protected readonly IRepository<T> _repo;
    protected readonly IUnitOfWork _uow;

    protected BaseCrudUseCase(IRepository<T> repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public virtual async Task<List<T>> GetAll(Guid userId)
        => (await _repo.GetAllAsync(x => x.UserId == userId)).ToList();

    public virtual async Task<T?> GetById(Guid id)
        => await _repo.GetByIdAsync(id);

    public virtual async Task Update(T entity)
    {
        _repo.Update(entity);
        await _uow.CommitAsync();
    }

    public virtual async Task Delete(Guid id)
    {
        await _repo.SoftDeleteAsync(id);
        await _uow.CommitAsync();
    }
}

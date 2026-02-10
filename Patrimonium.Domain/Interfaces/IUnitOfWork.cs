namespace Patrimonium.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> CommitAsync();
        Task RollbackAsync();
    }
}

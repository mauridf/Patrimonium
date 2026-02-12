namespace Patrimonium.Application.Interfaces
{
    public interface IContractLifecycleUseCase
    {
        Task Activate(Guid id);
        Task Suspend(Guid id);
        Task Cancel(Guid id);
        Task Finish(Guid id);
    }
}

namespace Patrimonium.Application.Interfaces
{
    public interface IBillingGenerationService
    {
        Task GenerateAsync(Guid contractId, DateTime reference);
    }

}

namespace Patrimonium.Application.Interfaces
{
    public interface IAdjustmentEngine
    {
        Task ApplyAdjustmentsAsync(DateTime referenceDate);
    }
}

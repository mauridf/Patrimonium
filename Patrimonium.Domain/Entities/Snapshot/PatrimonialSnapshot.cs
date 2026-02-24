using Microsoft.EntityFrameworkCore;

namespace Patrimonium.Domain.Entities.Snapshot
{
    [Owned]
    public class PatrimonialSnapshot
    {
        public decimal TotalEstimatedValue { get; }
        public decimal TotalInvested { get; }
        public decimal Appreciation { get; }
        public decimal RoiAccumulated { get; }

        private PatrimonialSnapshot() { }

        public PatrimonialSnapshot(
            decimal estimatedValue,
            decimal invested,
            decimal roiAccumulated)
        {
            TotalEstimatedValue = estimatedValue;
            TotalInvested = invested;
            Appreciation = estimatedValue - invested;
            RoiAccumulated = roiAccumulated;
        }
    }
}

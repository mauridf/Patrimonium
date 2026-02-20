namespace Patrimonium.Domain.Entities
{
    public class PatrimonialSnapshot
    {
        public decimal TotalEstimatedValue { get; }
        public decimal TotalInvested { get; }
        public decimal Appreciation { get; }
        public decimal RoiAccumulated { get; }

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

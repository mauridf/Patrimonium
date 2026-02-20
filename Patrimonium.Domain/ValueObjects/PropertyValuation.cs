namespace Patrimonium.Domain.ValueObjects
{
    public class PropertyValuation
    {
        public decimal EstimatedValue { get; private set; }
        public decimal InvestedValue { get; private set; }

        public PropertyValuation(decimal estimated, decimal invested)
        {
            EstimatedValue = estimated;
            InvestedValue = invested;
        }
    }
}

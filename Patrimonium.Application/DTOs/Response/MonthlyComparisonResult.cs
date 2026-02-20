using Patrimonium.Domain.ValueObjects;

namespace Patrimonium.Application.DTOs.Response
{
    public class MonthlyComparisonResult
    {
        public ClosingPeriod PeriodA { get; set; }
        public ClosingPeriod PeriodB { get; set; }

        public decimal IncomeDelta { get; set; }
        public decimal ExpenseDelta { get; set; }
        public decimal NetResultDelta { get; set; }

        public decimal OccupancyDelta { get; set; }
        public decimal RoiDelta { get; set; }

        public string Interpretation { get; set; } = "";
    }
}

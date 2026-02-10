using System.Collections.Generic;

namespace Patrimonium.Application.DTOs.Intelligence
{
    public class IntelligenceSummaryDto
    {
        public PropertyRankDto BestProperty { get; set; } = default!;
        public PropertyRankDto WorstProperty { get; set; } = default!;

        public List<PropertyRankDto> Ranking { get; set; } = new();

        public List<string> FinancialBottlenecks { get; set; } = new();
        public List<string> OperationalBottlenecks { get; set; } = new();

        public List<string> Recommendations { get; set; } = new();
        public int PatrimonialScore { get; set; }
    }
}

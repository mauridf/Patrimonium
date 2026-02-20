namespace Patrimonium.Application.DTOs.Response
{
    public class ProjectionResult
    {
        public IReadOnlyCollection<ProjectionPoint> Projections { get; set; } = [];
    }

    public class ProjectionPoint
    {
        public string Period { get; set; } = "";
        public decimal ProjectedIncome { get; set; }
        public decimal ProjectedExpense { get; set; }
        public decimal ProjectedNet { get; set; }
    }
}

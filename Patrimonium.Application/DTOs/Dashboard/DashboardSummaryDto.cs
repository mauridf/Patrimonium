namespace Patrimonium.Application.DTOs.Dashboard
{
    public class DashboardSummaryDto
    {
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal NetBalance { get; set; }

        public int TotalProperties { get; set; }
        public int OccupiedProperties { get; set; }
        public int VacantProperties { get; set; }

        public int ActiveContracts { get; set; }
        public int ExpiredContracts { get; set; }

        public decimal AverageRent { get; set; }
        public decimal VacancyRate { get; set; }  // %
        public decimal OccupancyRate { get; set; } // %

        public decimal ROI { get; set; } // retorno sobre patrimônio
    }
}

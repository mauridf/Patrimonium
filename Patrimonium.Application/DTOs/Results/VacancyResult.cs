namespace Patrimonium.Application.DTOs.Results
{
    public class VacancyResult
    {
        public Guid PropertyId { get; set; }
        public decimal VacancyRate { get; set; }
        public int DaysVacant { get; set; }
        public decimal RevenueLoss { get; set; }
    }
}

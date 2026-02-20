namespace Patrimonium.Domain.Entities
{
    public class OperationalSnapshot
    {
        public int ActiveProperties { get; }
        public int VacantProperties { get; }
        public int ActiveContracts { get; }
        public int OpenMaintenances { get; }
        public decimal OccupancyRate { get; }
        public decimal VacancyRate { get; }

        public OperationalSnapshot(
            int activeProperties,
            int vacantProperties,
            int activeContracts,
            int openMaintenances)
        {
            ActiveProperties = activeProperties;
            VacantProperties = vacantProperties;
            ActiveContracts = activeContracts;
            OpenMaintenances = openMaintenances;

            var total = activeProperties + vacantProperties;
            OccupancyRate = total == 0 ? 0 : (decimal)activeProperties / total;
            VacancyRate = total == 0 ? 0 : (decimal)vacantProperties / total;
        }
    }
}

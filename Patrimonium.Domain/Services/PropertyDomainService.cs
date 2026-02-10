using Patrimonium.Domain.Entities;

namespace Patrimonium.Domain.Services
{
    public class PropertyDomainService
    {
        public void ValidateCreation(Property property)
        {
            if (property.BuiltAreaM2 <= 0)
                throw new Exception("Área construída inválida");

            if (string.IsNullOrWhiteSpace(property.InternalName))
                throw new Exception("Nome interno é obrigatório");

            if (property.TotalAreaM2 < property.BuiltAreaM2)
                throw new Exception("Área total não pode ser menor que área construída");
        }

        public decimal CalculateROI(decimal netProfit, decimal propertyValue)
        {
            if (propertyValue <= 0) return 0;
            return (netProfit / propertyValue) * 100;
        }
    }
}

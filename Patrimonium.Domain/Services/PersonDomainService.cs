using Patrimonium.Domain.Entities;

namespace Patrimonium.Domain.Services
{
    public class PersonDomainService
    {
        public void Validate(Person p)
        {
            if (string.IsNullOrWhiteSpace(p.Name))
                throw new Exception("Nome obrigatório");

            if (string.IsNullOrWhiteSpace(p.CpfCnpj))
                throw new Exception("CPF/CNPJ obrigatório");
        }
    }
}

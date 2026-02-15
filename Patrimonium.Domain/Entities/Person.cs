using Patrimonium.Domain.Enums;

namespace Patrimonium.Domain.Entities
{
    public class Person : BaseEntity
    {
        public Guid UserId { get; private set; }

        public string Name { get; private set; } = null!;
        public PersonType Type { get; private set; }

        public string? CpfCnpj { get; private set; }
        public string? Email { get; private set; }
        public string? Phone { get; private set; }

        public string? Profession { get; private set; }
        public decimal? IncomeEstimation { get; private set; }
        public int? ScoreInternal { get; private set; }

        public string? Notes { get; private set; }

        protected Person() { }

        public Person(
            Guid userId,
            string name,
            PersonType type,
            string? cpfCnpj,
            string? email,
            string? phone,
            string? profession,
            decimal? incomeEstimation,
            int? scoreInternal,
            string? notes
        )
        {
            UserId = userId;
            Name = name;
            Type = type;
            CpfCnpj = cpfCnpj;
            Email = email;
            Phone = phone;
            Profession = profession;
            IncomeEstimation = incomeEstimation;
            ScoreInternal = scoreInternal;
            Notes = notes;
        }

        public void UpdateScore(int score)
        {
            ScoreInternal = score;
            MarkUpdated();
        }

        public void UpdateNotes(string? notes)
        {
            Notes = notes;
            MarkUpdated();
        }
    }
}

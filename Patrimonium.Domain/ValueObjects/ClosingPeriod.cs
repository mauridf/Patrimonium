using Patrimonium.Domain.Exceptions;

namespace Patrimonium.Domain.ValueObjects
{
    public sealed class ClosingPeriod
    {
        public int Year { get; private set; }
        public int Month { get; private set; }

        private ClosingPeriod() { }

        public ClosingPeriod(int year, int month)
        {
            if (month < 1 || month > 12)
                throw new DomainException("Mês inválido.");

            Year = year;
            Month = month;
        }

        public override string ToString() => $"{Year}-{Month:D2}";
    }
}

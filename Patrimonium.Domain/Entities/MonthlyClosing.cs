using Patrimonium.Domain.Enums;
using Patrimonium.Domain.ValueObjects;

namespace Patrimonium.Domain.Entities
{
    public class MonthlyClosing
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }

        public ClosingPeriod Period { get; private set; }
        public ClosingStatus Status { get; private set; }

        public int Version { get; private set; }

        public DateTime GeneratedAt { get; private set; }
        public DateTime? LockedAt { get; private set; }
        public DateTime? ReopenedAt { get; private set; }

        public string IntegrityHash { get; private set; }

        public MonthlySnapshot Snapshot { get; private set; }

        private MonthlyClosing() { }

        public MonthlyClosing(Guid userId, ClosingPeriod period, MonthlySnapshot snapshot, string hash)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Period = period;
            Snapshot = snapshot;
            IntegrityHash = hash;

            Status = ClosingStatus.Generated;
            Version = 1;
            GeneratedAt = DateTime.UtcNow;
        }

        public void Lock()
        {
            if (Status == ClosingStatus.Locked)
                throw new DomainException("Fechamento já está bloqueado.");

            Status = ClosingStatus.Locked;
            LockedAt = DateTime.UtcNow;
        }

        public MonthlyClosing CreateNewVersion(MonthlySnapshot newSnapshot, string newHash)
        {
            if (Status != ClosingStatus.Locked)
                throw new DomainException("Somente fechamentos bloqueados podem gerar nova versão.");

            return new MonthlyClosing(
                UserId,
                Period,
                newSnapshot,
                newHash
            )
            {
                Version = this.Version + 1
            };
        }
    }
}

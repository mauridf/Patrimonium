namespace Patrimonium.Domain.Entities
{
    public class User : AuditableEntity
    {
        public string Nome { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string? Phone { get; set; }
        public bool IsActive { get; set; }

        // Navegação
        public ICollection<Property> Properties { get; set; } = new List<Property>();
    }
}

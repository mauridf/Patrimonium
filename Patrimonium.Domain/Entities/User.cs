using System;

namespace Patrimonium.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public string PasswordHash { get; private set; } = null!;
        public string? Phone { get; private set; }

        public bool IsActive { get; private set; } = true;
        public bool EmailConfirmed { get; private set; } = false;

        protected User() { } // EF

        public User(string name, string email, string passwordHash, string? phone = null)
        {
            Name = name;
            Email = email.ToLower();
            PasswordHash = passwordHash;
            Phone = phone;
            IsActive = true;
            EmailConfirmed = false;
        }

        public void ConfirmEmail()
        {
            EmailConfirmed = true;
            MarkUpdated();
        }

        public void Deactivate()
        {
            IsActive = false;
            MarkUpdated();
        }

        public void ChangePassword(string newHash)
        {
            PasswordHash = newHash;
            MarkUpdated();
        }

        public void UpdateProfile(string name, string? phone)
        {
            Name = name;
            Phone = phone;
            MarkUpdated();
        }
    }
}

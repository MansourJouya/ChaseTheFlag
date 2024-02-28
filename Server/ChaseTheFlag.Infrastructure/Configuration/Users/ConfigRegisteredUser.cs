using ChaseTheFlag.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChaseTheFlag.Infrastructure.Configuration.Users
{
    /// <summary>
    /// Configuration class for the RegisteredUser entity.
    /// </summary>
    internal class ConfigRegisteredUser : IEntityTypeConfiguration<RegisteredUser>
    {
        /// <summary>
        /// Configures the entity RegisteredUser.
        /// </summary>
        /// <param name="builder">The builder used to configure the entity.</param>
        public void Configure(EntityTypeBuilder<RegisteredUser> builder)
        {
            // Configure primary key
            builder.HasKey(user => user.Id);

            // Configure properties
            builder.Property(user => user.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(user => user.LastName).IsRequired().HasMaxLength(50);
            builder.Property(user => user.Username).IsRequired().HasMaxLength(50);
            builder.Property(user => user.PasswordSalt).IsRequired();
            builder.Property(user => user.PasswordHash).IsRequired();

            builder.Property(user => user.UserRole).IsRequired().HasMaxLength(50);
            builder.Property(user => user.RegistrationDateTime).IsRequired();
        }
    }
}

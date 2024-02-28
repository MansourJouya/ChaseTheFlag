using ChaseTheFlag.Domain.Entities.Games;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChaseTheFlag.Infrastructure.Configuration.Games
{
    /// <summary>
    /// Configuration class for the RegisteredRoom entity.
    /// </summary>
    internal class ConfigRoom : IEntityTypeConfiguration<RegisteredRoom>
    {
        /// <summary>
        /// Configures the entity RegisteredRoom.
        /// </summary>
        /// <param name="builder">The builder used to configure the entity.</param>
        public void Configure(EntityTypeBuilder<RegisteredRoom> builder)
        {
            // Configure primary key
            builder.HasKey(room => room.Id);

            // Configure properties
            builder.Property(room => room.Name).IsRequired().HasMaxLength(100);
            builder.Property(room => room.Color).IsRequired().HasMaxLength(50);
            builder.Property(room => room.Level).IsRequired();
            builder.Property(room => room.Capacity).IsRequired();
        }
    }
}

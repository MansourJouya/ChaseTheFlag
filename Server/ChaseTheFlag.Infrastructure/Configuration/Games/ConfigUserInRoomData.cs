using ChaseTheFlag.Domain.Entities.Games;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChaseTheFlag.Infrastructure.Configuration.Games
{
    /// <summary>
    /// Configuration class for the UserInRoomData entity.
    /// </summary>
    internal class ConfigUserInRoomData : IEntityTypeConfiguration<UserInRoomData>
    {
        /// <summary>
        /// Configures the entity UserInRoomData.
        /// </summary>
        /// <param name="builder">The builder used to configure the entity.</param>
        public void Configure(EntityTypeBuilder<UserInRoomData> builder)
        {
            // Configure primary key
            builder.HasKey(userInRoom => userInRoom.Id);
        }
    }
}

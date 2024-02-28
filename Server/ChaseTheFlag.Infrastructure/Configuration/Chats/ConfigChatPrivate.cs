using ChaseTheFlag.Domain.Entities.Chats;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChaseTheFlag.Infrastructure.Configuration.Chats
{
    /// <summary>
    /// Configuration class for the ChatPrivate entity.
    /// </summary>
    internal class ConfigChatPrivate : IEntityTypeConfiguration<ChatPrivate>
    {
        /// <summary>
        /// Configures the entity ChatPrivate.
        /// </summary>
        /// <param name="builder">The builder used to configure the entity.</param>
        public void Configure(EntityTypeBuilder<ChatPrivate> builder)
        {
            // Set the primary key for the entity
            builder.HasKey(chat => chat.Id);
        }
    }
}

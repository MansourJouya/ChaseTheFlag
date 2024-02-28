using ChaseTheFlag.Domain.Entities.Chats;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChaseTheFlag.Infrastructure.Configuration.Chats
{
    /// <summary>
    /// Configuration class for the ChatPublic entity.
    /// </summary>
    internal class ConfigChatPublic : IEntityTypeConfiguration<ChatPublic>
    {
        /// <summary>
        /// Configures the entity ChatPublic.
        /// </summary>
        /// <param name="builder">The builder used to configure the entity.</param>
        public void Configure(EntityTypeBuilder<ChatPublic> builder)
        {
            // Set the primary key for the entity
            builder.HasKey(chat => chat.Id);
        }
    }
}

using ChaseTheFlag.Domain.Entities.Chats;
using ChaseTheFlag.Domain.Entities.Games;
using ChaseTheFlag.Domain.Entities.Users;
using ChaseTheFlag.Infrastructure.Configuration.Chats;
using ChaseTheFlag.Infrastructure.Configuration.Games;
using ChaseTheFlag.Infrastructure.Configuration.Users;
using Microsoft.EntityFrameworkCore;

namespace ChaseTheFlag.Infrastructure.Context
{
    /// <summary>
    /// Represents the database context for the application.
    /// </summary>
    public class DbContextConnection : DbContext
    {
        public DbContextConnection(DbContextOptions<DbContextConnection> options) : base(options)
        {
        }

        // Define DbSet properties for each entity
        public DbSet<RegisteredUser> RegisteredUsers { get; set; }
        public DbSet<RegisteredRoom> RegisteredRooms { get; set; }
        public DbSet<UserInRoomData> UserInRoomDatas { get; set; }
        public DbSet<ChatPublic> ChatPublic { get; set; }
        public DbSet<ChatPrivate> ChatPrivates { get; set; }

        // Override OnModelCreating to apply entity configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply entity configurations
            modelBuilder.ApplyConfiguration(new ConfigChatPublic());
            modelBuilder.ApplyConfiguration(new ConfigChatPrivate());
            modelBuilder.ApplyConfiguration(new ConfigRoom());
            modelBuilder.ApplyConfiguration(new ConfigRegisteredUser());
            modelBuilder.ApplyConfiguration(new ConfigUserInRoomData());
        }
    }
}

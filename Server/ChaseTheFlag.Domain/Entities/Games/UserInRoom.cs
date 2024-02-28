namespace ChaseTheFlag.Domain.Entities.Games
{
    /// <summary>
    /// Represents a user in a room in the game.
    /// </summary>
    public class UserInRoom
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the room.
        /// </summary>
        public int RoomId { get; set; }
    }

    /// <summary>
    /// Represents additional data for a user in a room in the game.
    /// </summary>
    public class UserInRoomData : UserInRoom
    {
        /// <summary>
        /// Gets or sets the unique identifier of the additional data for the user in the room.
        /// </summary>
        public int Id { get; set; } = 0;
    }
}

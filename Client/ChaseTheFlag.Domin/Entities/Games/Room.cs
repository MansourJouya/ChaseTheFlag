namespace ChaseTheFlag.Domain.Entities.Games
{
    /// <summary>
    /// Represents a room in the game.
    /// </summary>
    public class Room
    {
        /// <summary>
        /// Gets or sets the name of the room.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the color of the room.
        /// </summary>
        public string Color { get; set; } = "";

        /// <summary>
        /// Gets or sets the level of the room.
        /// </summary>
        public int Level { get; set; } = 100;

        /// <summary>
        /// Gets or sets the capacity of the room.
        /// </summary>
        public int Capacity { get; set; } = 100;
    }

    /// <summary>
    /// Represents a registered room in the game.
    /// </summary>
    public class RegisteredRoom : Room
    {
        /// <summary>
        /// Gets or sets the unique identifier of the registered room.
        /// </summary>
        public int Id { get; set; } = 0;

        /// <summary>
        /// Gets or sets the active level of the registered room.
        /// </summary>
        public int ActiveLevel { get; set; } = 1;

        /// <summary>
        /// Gets or sets the registration date and time of the registered room.
        /// </summary>
        public DateTime RegistrationDateTime { get; set; } = DateTime.Now;
    }
}

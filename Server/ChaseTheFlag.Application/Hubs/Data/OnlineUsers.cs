namespace ChaseTheFlag.Application.Hubs.Data
{
    /// <summary>
    /// Static class to manage online game rooms.
    /// </summary>
    public static class OnlinListGame
    {
        /// <summary>
        /// Collection to store the list of online game rooms.
        /// </summary>
        public static HashSet<ListGame> ListGame = new HashSet<ListGame>();

    }

    /// <summary>
    /// Class representing a game in the list of online games.
    /// </summary>
    public class ListGame
    {
        /// <summary>
        /// The ID of the room.
        /// </summary>
        public byte RoomId { get; set; }

        /// <summary>
        /// The board data of the game.
        /// </summary>
        public byte[] Board { get; set; }
    }


    /// <summary>
    /// Static class to manage online users.
    /// </summary>
    public static class OnlineUsers
    {
        /// <summary>
        /// Collection to store the list of online users.
        /// </summary>
        public static HashSet<UserOnline> ListUser = new HashSet<UserOnline>();
    }

    /// <summary>
    /// Class representing an online user.
    /// </summary>
    public class UserOnline
    {
        /// <summary>
        /// The player's tag.
        /// </summary>
        public string PlayerTag { get; set; }

        /// <summary>
        /// The connection ID of the user.
        /// </summary>
        public string ConnectionId { get; set; }

        /// <summary>
        /// The ID of the room the user is in.
        /// </summary>
        public int RoomId { get; set; }


    }

}

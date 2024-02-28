namespace ChaseTheFlag.Domain.Entities.Chats
{
    /// <summary>
    /// Represents a private chat message between users.
    /// </summary>
    public class ChatPrivate
    {
        /// <summary>
        /// Gets or sets the unique identifier of the private chat message.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user ID of the sender of the private chat message.
        /// </summary>
        public int UserIdSend { get; set; }

        /// <summary>
        /// Gets or sets the name of the sender of the private chat message.
        /// </summary>
        public string SenderName { get; set; }

        /// <summary>
        /// Gets or sets the player tag associated with the sender.
        /// </summary>
        public string PlayerTag { get; set; }

        /// <summary>
        /// Gets or sets the user ID of the receiver of the private chat message.
        /// </summary>
        public int UserIdReceive { get; set; }

        /// <summary>
        /// Gets or sets the content of the private chat message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the private chat message was sent.
        /// </summary>
        public DateTime SendDateTime { get; set; }
    }
}

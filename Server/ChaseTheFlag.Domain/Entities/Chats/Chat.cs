namespace ChaseTheFlag.Domain.Entities.Chats
{
    /// <summary>
    /// Represents a public chat message.
    /// </summary>
    public class ChatPublic
    {
        /// <summary>
        /// Gets or sets the unique identifier of the chat message.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the sender of the chat message.
        /// </summary>
        public string SenderName { get; set; }

        /// <summary>
        /// Gets or sets the content of the chat message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the chat message was sent.
        /// </summary>
        public DateTime SendDateTime { get; set; }
    }
}

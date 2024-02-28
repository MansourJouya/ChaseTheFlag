using System.Text;

namespace ChaseTheFlag.Application.Hubs.Data
{

    public class StartGame
    {
        public async Task<StartGameResult> RunAsync(byte[] command)
        {
            // Check if there is already a running game in the specified room
            var roomGame = OnlinListGame.ListGame.FirstOrDefault(x => x.RoomId == command[3]);
            if (roomGame != null)
                return null;

            // Extract information about existing users from the command
            var existingUsers = command.Skip(4).Select(c =>
            {
                string charAsString = Encoding.UTF8.GetString(new byte[] { c });
                return OnlineUsers.ListUser.FirstOrDefault(x => x.PlayerTag == charAsString);
            }).Where(user => user != null).ToList();

            // Separate user information into two lists: connection IDs and player tags
            var connectionIds = existingUsers.Select(x => x.ConnectionId).ToList();
            var playerTags = existingUsers.Select(x => x.PlayerTag).Distinct().ToList();

            // Determine the size of the game board based on the specified level
            int col = 0;
            int barrier = 0;
            switch (command[4]) // Level
            {
                case 0x01: col = 10; barrier = 2; break;
                case 0x02: col = 11; barrier = 4; break;
                case 0x03: col = 12; barrier = 6; break;
                case 0x04: col = 13; barrier = 8; break;
                case 0x05: col = 15; barrier = 10; break;
                default: col = 15; barrier = 2; break;
            }

            // Initialize the game board with zeros
            byte[] byteArray = Enumerable.Repeat((byte)0, col * col + 5).ToArray();

            // Set up game board properties
            Random random = new Random();
            byteArray[0] = 0x01;                            // Update
            byteArray[1] = 0x01;                            // Kind 
            if (command[2] == 0x00)                         // Target
                byteArray[2] = Encoding.UTF8.GetBytes(playerTags[random.Next(playerTags.Count)])[0];
            else
                byteArray[2] = command[2];
            byteArray[3] = command[3];                      // RoomId
            byteArray[4] = command[4];                      // Level

            // Randomly place players on the game board
            for (int i = 0; i < playerTags.Count; i++)
                byteArray[random.Next(5, byteArray.Length)] = Encoding.UTF8.GetBytes(playerTags[i])[0];

            // Place barriers on the game board
            for (int i = 0; i < barrier; i++)
            {
                int rand = random.Next(5, byteArray.Length);
                if (byteArray[rand] == (byte)0)
                    byteArray[rand] = (byte)255;
            }

            // Check if the game already exists, if not, add it to the list of games
            var existingGame = OnlinListGame.ListGame.FirstOrDefault(x => x.RoomId == command[3]);
            if (existingGame == null)
            {
                // Save game data in memory
                OnlinListGame.ListGame.Add(new ListGame()
                {
                    Board = byteArray,
                    RoomId = command[3]
                });
            }
            else
                existingGame.Board = byteArray;


            return new StartGameResult
            {
                ConnectionIds = connectionIds,
                ByteArray = byteArray
            };
        }
    }
}


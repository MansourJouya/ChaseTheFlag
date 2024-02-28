using ChaseTheFlag.Application.Hubs.Data;
using ChaseTheFlag.Application.Users;
using ChaseTheFlag.Domain.Entities.Chats;
using ChaseTheFlag.Domain.Entities.Users;
using Microsoft.AspNetCore.SignalR;
using System.Text;

namespace ChaseTheFlag.Application.Hubs.Serviec
{
    public class HubService(IHubContext<GameHub> hubContext, IRegisteredUserService registeredUserService)
    {
        private readonly IHubContext<GameHub> _hubContext = hubContext;
        private readonly IRegisteredUserService _registeredUserService = registeredUserService;


        /// <summary>
        /// Hub User   ==>  Messeage Syetem  
        /// Updates user connection information and sends the updated count of online users to clients.
        /// </summary>
        /// <param name="playerTag">The player's tag</param>
        /// <param name="connectionId">The connection ID</param>
        public async Task UpdateUserConnectionAsync(string playerTag, string connectionId)
        {
            await UpdateUserConnectionLocalAsync(playerTag, connectionId);
        }


        /// <summary>
        /// Hub User   ==>  Messeage Syetem 
        /// Removes a user based on their connection ID, updates related data, 
        /// and sends the updated count of online users to clients.
        /// </summary>
        /// <param name="connectionId">The connection ID of the user to be removed</param>
        public async Task RemoveUserAsync(string connectionId)
        {
            await RemoveUserLocalAsync(connectionId);
        }


        /// <summary>
        /// Hub User   ==>  Messeage Update  
        /// Adds a user to a group identified by the room ID and sends a status message to the group.
        /// </summary>
        /// <param name="playerTag">The player's tag</param>
        /// <param name="roomId">The ID of the room</param>
        public async Task AddUserInGroupAsync(string playerTag, int roomId)
        {
            await AddUserInGroupLocalAsync(playerTag, roomId);
        }


        /// <summary>
        /// Hub User   ==>  Messeage Update  
        /// Removes a user from a group based on their player tag, updates related data, 
        /// and sends a status message to the group.
        /// </summary>
        /// <param name="PlayerTag">The player's tag</param>
        public async Task RemoveUserInGroupAsync(string PlayerTag)
        {
            await RemoveUserInGroupLocalAsync(PlayerTag);
        }


        /// <summary>
        /// Hub User   ==>  Messeage Update   
        /// Sends a status message to the group identified by the room ID.
        /// </summary>
        /// <param name="roomID">The ID of the room</param>

        public async Task SendStatusMessageToGroupAysnc(int roomID)
        {
            await SendStatusMessageToGroupLocalAysnc(roomID);
        }


        /// <summary>
        /// Hub Game   ==>  Messeage Update  
        /// Initiates the start of a new game based on the provided command.
        /// </summary>
        /// <param name="command">The command containing information necessary to begin the game</param>
        public async Task StartGameAsync(byte[] command)
        {
            await StartGameLocalAsync(command);
        }


        /// <summary>
        /// Hub Game   ==>  Messeage Update  
        /// Updates the game based on the received command.
        /// </summary>
        /// <param name="command">The command containing information to update the game</param>
        public async Task UpdateGameAsync(byte[] command)
        {

            await UpdateGameRunAsync(command);
        }


        /// <summary>
        /// Hub Game   ==>  Messeage Update  
        /// Sends a private message to a specific user based on their player tag.
        /// </summary>
        /// <param name="playersTag">The player's tag to whom the message is sent</param>
        /// <param name="message">The private message to send</param>
        public async Task SendMessagePrivateAsync(string playersTag, ChatPrivate message)
        {
            await SendMessagePrivateLocalAsync(playersTag, message);
        }


        /// <summary>
        /// Hub Game   ==>  Messeage Update  
        /// Sends a public message to all users.
        /// </summary>
        /// <param name="message">The public message to send</param>
        public async Task SendMessagePublicAsync(ChatPublic message)

        {
            await SendMessagePublicLocalAsync(message);
        }


        /// <summary>
        /// Updates the user's status in the database based on the player tag and whether the user has won the game.
        /// </summary>
        /// <param name="playerTag">The player's tag</param>
        /// <param name="isWin">A boolean indicating whether the user has won the game</param>
        private async Task UpdateUserStatusInDatabaseAsync(byte playerTag, bool isWin)
        {
            await UpdateUserStatusInDatabaseLocalAsync(playerTag, isWin);
        }


        /// <summary>
        /// Removes a user from the game based on their connection ID, updates related data, and notifies clients.
        /// </summary>
        /// <param name="connectionId">The connection ID of the user to be removed from the game</param>
        private async Task RemoveUserFromGameAsync(string connectionId)
        {
            await RemoveUserFromGameLocalAsync(connectionId);
        }




        #region Local

        /// <summary>
        /// Updates the coordinates for movement based on the given direction within a grid.
        /// </summary>
        /// <param name="direction">The direction of movement (L for left, R for right, U for up, D for down).</param>
        /// <param name="x">The current x-coordinate.</param>
        /// <param name="y">The current y-coordinate.</param>
        /// <param name="Length">The length of the grid (assuming it's a square grid).</param>
        /// <returns>A tuple containing the updated x and y coordinates after applying the movement.</returns>
        private (int newX, int newY) UpdateCoordinatesForMovement(char direction, int x, int y, int Length)
        {
            // Initialize variables to hold the updated coordinates
            int updatedX = x;
            int updatedY = y;

            // Update the coordinates based on the given direction
            switch (direction)
            {
                case 'L':
                    updatedX--;
                    // Wrap around to the opposite side if out of bounds
                    if (updatedX < 0)
                        updatedX = Length - 1;
                    break;
                case 'R':
                    updatedX++;
                    // Wrap around to the opposite side if out of bounds
                    if (updatedX >= Length)
                        updatedX = 0;
                    break;
                case 'U':
                    updatedY--;
                    // Wrap around to the opposite side if out of bounds
                    if (updatedY < 0)
                        updatedY = Length - 1;
                    break;
                case 'D':
                    updatedY++;
                    // Wrap around to the opposite side if out of bounds
                    if (updatedY >= Length)
                        updatedY = 0;
                    break;
                default:
                    break;
            }

            // Return the updated coordinates
            return (updatedX, updatedY);
        }
        private async Task UpdateGameRunAsync(byte[] command)
        {
            // Check if the message is an update message
            if (command[0] != 0x01)
                return;

            // Check if the message corresponds to a king update
            if (command[1] != 0x03)
                return;

            // Find the game room based on the received room ID
            var roomGame = OnlinListGame.ListGame.FirstOrDefault(x => x.RoomId == command[3]);
            if (roomGame != null)
            {
                byte Target = command[2];
                // Get the new cell

                int setingCell = 5;
                int index2 = Array.IndexOf(roomGame.Board, command[5], setingCell);
                int size = 0;
                switch (command[4]) // Level
                {
                    case 0x01: size = 10; break;
                    case 0x02: size = 11; break;
                    case 0x03: size = 12; break;
                    case 0x04: size = 13; break;
                    case 0x05: size = 15; break;
                    default: size = 15; break;
                }
                int x = (index2 - setingCell) / size;
                int y = (index2 - setingCell) % size;

                int newX, newY;
                (newX, newY) = UpdateCoordinatesForMovement(Convert.ToChar(command[6]), x, y, size);

                int index = newX * size + newY + setingCell;

                var existingUsers = roomGame.Board.Skip(4).Select(c =>
                {
                    string charAsString = Encoding.UTF8.GetString(new byte[] { c });
                    return OnlineUsers.ListUser.FirstOrDefault(x => x.PlayerTag == charAsString);
                }).Where(user => user != null).ToList();

                // Separate user information into two lists: connection IDs
                var connectionIds = existingUsers.Select(x => x!.ConnectionId).ToList();

                // Check if there is an obstacle
                if (roomGame.Board[index] != 0x00)
                {
                    byte[] result;
                    // Check if it's the target
                    if (roomGame.Board[index] == command[2])
                    {
                        // If the target is reached, the player wins
                        result = new byte[] { 0x01, 0x04, 1, command[5], 0x01, command[3] };
                        OnlinListGame.ListGame.Remove(roomGame);
                        await UpdateUserStatusInDatabaseAsync(playerTag: command[5], isWin: true);
                    }
                    else
                    {
                        // If it's not the target
                        if (command[5] == command[2])
                        {
                            // If the player reaches the target, they win
                            result = new byte[] { 0x01, 0x04, 1, 0x00, 0x01, command[3] };
                            OnlinListGame.ListGame.Remove(roomGame);
                            await UpdateUserStatusInDatabaseAsync(playerTag: command[2], isWin: false);
                        }
                        else
                        {
                            // If another player loses
                            result = new byte[] { 0x01, 0x04, 0, command[5], 0x00, command[3] };
                            int remoIndexUser = Array.IndexOf(roomGame.Board, command[5], setingCell);
                            roomGame.Board[remoIndexUser] = 0x00;
                            await UpdateUserStatusInDatabaseAsync(playerTag: command[5], isWin: false);

                            var getAllUser = roomGame.Board.Skip(setingCell).Where(b => b != 0 && b != 255).ToArray();
                            if (!(getAllUser.Length > 1))
                            {
                                result = new byte[] { 0x01, 0x04, 0, getAllUser[0], 0x01, command[3] };
                                OnlinListGame.ListGame.Remove(roomGame);
                                await UpdateUserStatusInDatabaseAsync(playerTag: getAllUser[0], isWin: true);
                            }
                        }
                    }

                    // Update the database and notify clients
                    await _hubContext.Clients.AllExcept(connectionIds).SendAsync("ReceiveStatusGame", result);
                }
                else
                {
                    // If there is no obstacle
                    roomGame.Board[1] = 0x02;
                    roomGame.Board[index2] = 0x00;
                    roomGame.Board[index] = command[5];
                    await _hubContext.Clients.AllExcept(connectionIds).SendAsync("ReceiveStartGame", roomGame.Board);
                }
            }

        }
        private async Task RemoveUserFromGameLocalAsync(string connectionId)
        {
            // Find the user to remove based on the connection ID
            var userToRemove = OnlineUsers.ListUser.FirstOrDefault(user => user.ConnectionId == connectionId);

            // Check if the user exists
            if (userToRemove == null)
                return;

            // Find the game room associated with the user
            var roomGame = OnlinListGame.ListGame.FirstOrDefault(x => x.RoomId == userToRemove.RoomId);

            // Check if the game room exists
            if (roomGame != null)
            {
                // Find the index of the user's tag in the game board
                int settingCell = 5;
                int removeIndexUser = Array.IndexOf(roomGame.Board, Encoding.UTF8.GetBytes(userToRemove.PlayerTag)[0], settingCell);

                // Update the game board to remove the user's tag
                roomGame.Board[removeIndexUser] = 0x00;

                // Find existing users in the game room
                var existingUsers = roomGame.Board.Skip(settingCell).Select(c =>
                {
                    string charAsString = Encoding.UTF8.GetString(new byte[] { c });
                    return OnlineUsers.ListUser.FirstOrDefault(x => x.PlayerTag == charAsString);
                }).Where(user => user != null).ToList();

                // Extract connection IDs of existing users
                var connectionIds = existingUsers.Select(x => x!.ConnectionId).ToList();

                // Notify clients about the updated game board
                await _hubContext.Clients.AllExcept(connectionIds).SendAsync("ReceiveStartGame", roomGame.Board);

                // Notify clients about the user's exit from the game
                await _hubContext.Clients.AllExcept(userToRemove.ConnectionId).SendAsync("ReceiveExitGame", true, userToRemove.PlayerTag);

                // Check if there's only one user left in the game
                var getAllUser = roomGame.Board.Skip(settingCell).Where(b => b != 0 && b != 255).ToArray();
                if (!(getAllUser.Length <= 1))
                    return;

                // Prepare a result message
                byte[] result = new byte[] { 0x01, 0x04, 1, getAllUser[0], 0x01, (byte)roomGame.RoomId };

                // Remove the game room from the list of games
                OnlinListGame.ListGame.Remove(roomGame);

                // Update the user's status in the database
                await UpdateUserStatusInDatabaseAsync(playerTag: getAllUser[0], isWin: true);

                // Notify clients about the game status
                await _hubContext.Clients.AllExcept(connectionIds).SendAsync("ReceiveStatusGame", result);
            }

        }
        private async Task UpdateUserConnectionLocalAsync(string playerTag, string connectionId)
        {
            // Find the user with the given player tag
            var existingUser = OnlineUsers.ListUser.FirstOrDefault(user => user.PlayerTag == playerTag);

            // If the user does not exist, add a new user with the given player tag and connection ID
            if (existingUser == null)
            {
                OnlineUsers.ListUser.Add(new Data.UserOnline
                {
                    PlayerTag = playerTag,
                    ConnectionId = connectionId
                });
            }
            else
            {
                // If the user exists, update the connection ID
                existingUser.ConnectionId = connectionId;
            }

            // Prepare a message to update the count of online users
            byte[] countMessage = new byte[] { 0x00, 0x01, Convert.ToByte(OnlineUsers.ListUser.Count) }; // System Message 01x

            // Send the message to all clients
            await _hubContext.Clients.All.SendAsync("UpdateUserConnection", countMessage);
        }
        private async Task RemoveUserLocalAsync(string connectionId)
        {
            // Find the user to remove based on the connection ID
            var userToRemove = OnlineUsers.ListUser.FirstOrDefault(user => user.ConnectionId == connectionId);

            // Check if the user exists
            if (userToRemove != null)
            {
                // Remove the user from any groups
                await RemoveUserInGroupAsync(userToRemove.PlayerTag);

                // Remove the user from any games
                await RemoveUserFromGameAsync(connectionId);

                // Remove the user from the list of online users
                OnlineUsers.ListUser.Remove(userToRemove);

                // Prepare a message to update the count of online users
                byte[] countMessage = new byte[] { 0x00, 0x01, Convert.ToByte(OnlineUsers.ListUser.Count) }; // System Message 01x

                // Send the message to all clients
                await _hubContext.Clients.All.SendAsync("UpdateUserConnection", countMessage);
            }
        }
        private async Task AddUserInGroupLocalAsync(string playerTag, int roomId)
        {
            // Find the existing user with the given player tag
            var existingUserGroup = OnlineUsers.ListUser.FirstOrDefault(user => user.PlayerTag == playerTag);

            // Check if the user exists
            if (existingUserGroup != null)
            {
                // Update the user's room ID
                existingUserGroup.RoomId = roomId;
            }

            // Send a status message to the group
            await SendStatusMessageToGroupAysnc(roomId);
        }
        private async Task RemoveUserInGroupLocalAsync(string PlayerTag)
        {
            // Find the user based on the player tag
            var userFounded = OnlineUsers.ListUser.FirstOrDefault(user => user.PlayerTag == PlayerTag);

            // Check if the user exists
            if (userFounded != null)
            {
                // Remove the user from any games they are participating in
                await RemoveUserFromGameAsync(userFounded.ConnectionId);

                // Store the room ID of the user
                int roomID = userFounded.RoomId;

                // Reset the room ID of the user to 0
                userFounded.RoomId = 0;

                // Send a status message to the group associated with the room ID
                await SendStatusMessageToGroupAysnc(roomID);
            }
        }
        private async Task SendStatusMessageToGroupLocalAysnc(int roomID)
        {
            // Retrieve a list of users in the specified room
            var listUser = OnlineUsers.ListUser.Where(x => x.RoomId == roomID).ToList();

            // Extract player tags and connection IDs from the list of users
            var listPlayerTags = listUser.Select(x => x.PlayerTag).ToList();
            var ListConnectionUsers = listUser.Select(x => x.ConnectionId).ToList();

            // Create a list to hold the bytes of the status message
            List<byte> allTagBytes = new List<byte>();

            // Add message components: Update, Kind, and Room
            allTagBytes.Add(0x00);                                      // Update
            allTagBytes.Add(0x03);                                      // Kind
            allTagBytes.Add(BitConverter.GetBytes((short)roomID)[0]);   // Room

            // Add user tags to the message
            foreach (var tag in listPlayerTags)                         // User
            {
                allTagBytes.AddRange(Encoding.ASCII.GetBytes(tag));
            }

            // Convert the list of bytes to an array
            byte[] resultBytes = allTagBytes.ToArray();

            // Send the status message to all clients in the group except those in the list of connection IDs
            await _hubContext.Clients.AllExcept(ListConnectionUsers).SendAsync("ReceiveStatusMessage", resultBytes);
        }
        private async Task StartGameLocalAsync(byte[] command)
        {
            // Instantiate a GameManager object to manage the game
            StartGame startGame = new();

            // Call the RunAsync method of the StartGame to initiate the game and await the result
            StartGameResult result = await startGame.RunAsync(command);

            // Extract the connection IDs and game board byte array from the result
            List<string> connectionIds = result.ConnectionIds;
            byte[] byteArray = result.ByteArray;

            // Notify all clients except those with the specified connection IDs about the start of the game
            await _hubContext.Clients.AllExcept(connectionIds).SendAsync("ReceiveStartGame", byteArray);
        }
        private async Task SendMessagePrivateLocalAsync(string playersTag, ChatPrivate message)
        {
            // Find the user with the specified player tag
            var existingUser = OnlineUsers.ListUser.FirstOrDefault(x => x.PlayerTag == playersTag);

            // Check if the user exists
            if (existingUser != null)
            {
                // Send the private message to the user except their own connection
                await _hubContext.Clients.AllExcept(existingUser.ConnectionId).SendAsync("ReceiveMessagePrivate", message);
            }
        }
        private async Task SendMessagePublicLocalAsync(ChatPublic message)
        {
            // Send the public message to all users
            await _hubContext.Clients.All.SendAsync("ReceiveMessagePublic", message);
        }
        private async Task UpdateUserStatusInDatabaseLocalAsync(byte playerTag, bool isWin)
        {
            // Retrieve the user from the database based on the player tag
            RegisteredUser user = await _registeredUserService.GetByIdLocalAsync(Convert.ToChar(playerTag).ToString());

            // Check if the user exists
            if (user == null)
                return;

            // Update the user's status based on the game outcome
            if (isWin)
                user.NumberOfWins += 1;
            else
                user.NumberOfLosses += 1;

            // Update the user's information in the database
            await _registeredUserService.UpdateAsync(user);

            // Notify clients about the updated user's status
            await _hubContext.Clients.All.SendAsync("UpdateUserStatusResualt", playerTag);
        }
        #endregion
    }
}

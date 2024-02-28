using ChaseTheFlag.Client.Data.Authentication.Additions;
using ChaseTheFlag.Client.Data.Models;
using ChaseTheFlag.Client.Request;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChaseTheFlag.Client.Data.SendData
{
    /// <summary>
    /// Static service responsible for sending data to the SignalR hub.
    /// </summary>
    public static class HubDataSender
    {
        public static async Task SendDataToHub(HubConnection hubConnection, RequestHandler requestHandler, UserDataLocal currentUserData, string playerTag)
        {
            // Check if the hub connection is connected
            if (hubConnection!.State == HubConnectionState.Connected)
            {
                try
                {
                    // Construct the API URL for sending data to the hub
                    string apiUrl = ChaseTheFlag.Client.Data.Links.ApiEndpoints.GetApiBaseUrl() + $"SignalR/sendMessage/{playerTag}/{hubConnection.ConnectionId}";

                    // Send HTTP POST request to the API endpoint
                    var userResult = await requestHandler.SendHttpRequestAsync<string, string>(HttpMethod.Post, apiUrl, currentUserData);

                    // Check if the request was successful
                    if (userResult.Status == ResultStatus.Success)
                        Console.WriteLine("Data sent to the hub successfully.");
                }
                catch (Exception ex)
                {
                    // Handle exceptions that occur during the data sending process
                    Console.WriteLine($"Error sending data to hub: {ex.Message}");
                }
            }
            else
            {
                // Print a message indicating that the connection to the hub is not established
                Console.WriteLine("Connection to hub is not established.");
            }
        }
    }
}

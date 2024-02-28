using ChaseTheFlag.Client.Data.Authentication.Additions;
using ChaseTheFlag.Client.Data.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace ChaseTheFlag.Client.Request
{
    // Class responsible for handling HTTP requests
    public class RequestHandler
    {
        private readonly HttpClient _httpClient;

        // Constructor to initialize the HttpClient
        public RequestHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Method to send an HTTP request asynchronously
        public async Task<Result<U>> SendHttpRequestAsync<T, U>(HttpMethod method, string apiUrl, UserDataLocal userAccount, T requestData = default!, CancellationToken cancellationToken = default)
        {
            try
            {
                // Check if user is authenticated
                if (userAccount == null || !userAccount.IsAuthenticated)
                    return Result<U>.Fail("Error, please try again.");

                // Create a new HttpRequestMessage
                var request = new HttpRequestMessage(method, apiUrl);

                // Set authorization header with bearer token
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", userAccount.Token);

                // If method is not GET and there is request data, add content to the request
                if (method != HttpMethod.Get && requestData != null)
                {
                    var jsonContent = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");
                    request.Content = jsonContent;
                }

                // Send the request and get the response
                HttpResponseMessage response = await _httpClient.SendAsync(request, cancellationToken);
                string jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);

                // If response is not successful, extract error code
                if (!response.IsSuccessStatusCode)
                    return Result<U>.Mistake(jsonResponse);

                // Deserialize response based on generic type U
                if (typeof(U) == typeof(T))
                {
                    // Deserialize response into type T
                    if (method == HttpMethod.Get)
                    {
                        JsonSerializerOptions jsonOptions = new JsonSerializerOptions
                        {
                            ReferenceHandler = ReferenceHandler.Preserve,
                            PropertyNameCaseInsensitive = true,
                            WriteIndented = true,
                        };
                        T result = JsonSerializer.Deserialize<T>(jsonResponse, jsonOptions)!;
                        U convertedResult = (U)(object)result;
                        return Result<U>.Success(convertedResult);
                    }
                    // Return success with default value for type U
                    return Result<U>.Success(default!, jsonResponse);
                }
                else if (typeof(U) == typeof(List<T>))
                {
                    // Deserialize response into list of type T
                    JsonSerializerOptions jsonOptions = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        PropertyNameCaseInsensitive = true,
                        WriteIndented = true,
                    };
                    List<T> result = JsonSerializer.Deserialize<List<T>>(jsonResponse, jsonOptions)!;
                    U convertedResult = (U)(object)result;
                    return Result<U>.Success(convertedResult);
                }
                else
                {
                    // Return failure if response type does not match expected type
                    return Result<U>.Fail("Error, please try again.");
                }
            }
            catch (Exception ex)
            {
                // Return failure if an exception occurs
                return Result<U>.Fail(ex.Message);
            }
        }


        public async Task<Result<U>> SendHttpRequestRegistryAsync<T, U>(HttpMethod method, string apiUrl, T requestData = default!, CancellationToken cancellationToken = default)
        {
            try
            {


                // Create a new HttpRequestMessage
                var request = new HttpRequestMessage(method, apiUrl);



                // If method is not GET and there is request data, add content to the request
                if (method != HttpMethod.Get && requestData != null)
                {
                    var jsonContent = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");
                    request.Content = jsonContent;
                }

                // Send the request and get the response
                HttpResponseMessage response = await _httpClient.SendAsync(request, cancellationToken);
                string jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);

                // If response is not successful, extract error code
                if (!response.IsSuccessStatusCode)
                    return Result<U>.Mistake(jsonResponse);

                // Deserialize response based on generic type U
                if (typeof(U) == typeof(T))
                {
                    // Deserialize response into type T
                    if (method == HttpMethod.Get)
                    {
                        JsonSerializerOptions jsonOptions = new JsonSerializerOptions
                        {
                            ReferenceHandler = ReferenceHandler.Preserve,
                            PropertyNameCaseInsensitive = true,
                            WriteIndented = true,
                        };
                        T result = JsonSerializer.Deserialize<T>(jsonResponse, jsonOptions)!;
                        U convertedResult = (U)(object)result;
                        return Result<U>.Success(convertedResult);
                    }
                    // Return success with default value for type U
                    return Result<U>.Success(default!, jsonResponse);
                }
                else if (typeof(U) == typeof(List<T>))
                {
                    // Deserialize response into list of type T
                    JsonSerializerOptions jsonOptions = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        PropertyNameCaseInsensitive = true,
                        WriteIndented = true,
                    };
                    List<T> result = JsonSerializer.Deserialize<List<T>>(jsonResponse, jsonOptions)!;
                    U convertedResult = (U)(object)result;
                    return Result<U>.Success(convertedResult);
                }
                else
                {
                    // Return failure if response type does not match expected type
                    return Result<U>.Fail("Error, please try again.");
                }
            }
            catch (Exception ex)
            {
                // Return failure if an exception occurs
                return Result<U>.Fail(ex.Message);
            }
        }




    }
}


//using ChaseTheFlag.Client.Authentication.Additions;
//using ChaseTheFlag.Client.Models;
//using System.Net.Http.Headers;
//using System.Text;
//using System.Text.Json;
//using System.Text.Json.Serialization;

//namespace ChaseTheFlag.Client.Request
//{
//    /// <summary>
//    /// Class responsible for handling HTTP requests.
//    /// </summary>
//    public class RequestHandler
//    {
//        private readonly HttpClient _httpClient;

//        // Constructor to initialize the HttpClient.
//        public RequestHandler(HttpClient httpClient)
//        {
//            _httpClient = httpClient;
//        }

//        /// <summary>
//        /// Method to send an HTTP request asynchronously.
//        /// </summary>
//        public async Task<Result<U>> SendHttpRequestAsync<T, U>(HttpMethod method, string apiUrl, UserDataLocal userAccount, T requestData = default!, CancellationToken cancellationToken = default)
//        {
//            try
//            {
//                // Check if the user is authenticated.
//                if (userAccount == null || !userAccount.IsAuthenticated)
//                    return Result<U>.Fail("Error: User not authenticated.");

//                // Create a new HttpRequestMessage.
//                var request = new HttpRequestMessage(method, apiUrl);

//                // Set the authorization header with the bearer token.
//                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", userAccount.Token);

//                // If the method is not GET and there is request data, add content to the request.
//                if (method != HttpMethod.Get && requestData != null)
//                {
//                    var jsonContent = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");
//                    request.Content = jsonContent;
//                }

//                // Send the request and get the response.
//                HttpResponseMessage response = await _httpClient.SendAsync(request, cancellationToken);
//                string jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);

//                // If the response is not successful, extract the error message.
//                if (!response.IsSuccessStatusCode)
//                    return Result<U>.Fail(jsonResponse);

//                // Deserialize the response based on the generic type U.
//                if (typeof(U) == typeof(T))
//                {
//                    // Deserialize the response into type T.
//                    if (method == HttpMethod.Get)
//                    {
//                        JsonSerializerOptions jsonOptions = new JsonSerializerOptions
//                        {
//                            ReferenceHandler = ReferenceHandler.Preserve,
//                            PropertyNameCaseInsensitive = true,
//                            WriteIndented = true,
//                        };
//                        T result = JsonSerializer.Deserialize<T>(jsonResponse, jsonOptions)!;
//                        U convertedResult = (U)(object)result;
//                        return Result<U>.Success(convertedResult);
//                    }
//                    // Return success with the default value for type U.
//                    return Result<U>.Success(default!, jsonResponse);
//                }
//                else if (typeof(U) == typeof(List<T>))
//                {
//                    // Deserialize the response into a list of type T.
//                    JsonSerializerOptions jsonOptions = new JsonSerializerOptions
//                    {
//                        ReferenceHandler = ReferenceHandler.Preserve,
//                        PropertyNameCaseInsensitive = true,
//                        WriteIndented = true,
//                    };
//                    List<T> result = JsonSerializer.Deserialize<List<T>>(jsonResponse, jsonOptions)!;
//                    U convertedResult = (U)(object)result;
//                    return Result<U>.Success(convertedResult);
//                }
//                else
//                {
//                    // Return failure if the response type does not match the expected type.
//                    return Result<U>.Fail("Error: Response type does not match the expected type.");
//                }
//            }
//            catch (Exception ex)
//            {
//                // Return failure if an exception occurs.
//                return Result<U>.Fail($"Error: {ex.Message}");
//            }
//        }

//        /// <summary>
//        /// Method to send an HTTP request for registry asynchronously.
//        /// </summary>
//        public async Task<Result<U>> SendHttpRequestRegistryAsync<T, U>(HttpMethod method, string apiUrl, T requestData = default!, CancellationToken cancellationToken = default)
//        {
//            try
//            {
//                // Create a new HttpRequestMessage.
//                var request = new HttpRequestMessage(method, apiUrl);

//                // If the method is not GET and there is request data, add content to the request.
//                if (method != HttpMethod.Get && requestData != null)
//                {
//                    var jsonContent = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");
//                    request.Content = jsonContent;
//                }

//                // Send the request and get the response.
//                HttpResponseMessage response = await _httpClient.SendAsync(request, cancellationToken);
//                string jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);

//                // If the response is not successful, extract the error message.
//                if (!response.IsSuccessStatusCode)
//                    return Result<U>.Fail(jsonResponse);

//                // Deserialize the response based on the generic type U.
//                if (typeof(U) == typeof(T))
//                {
//                    // Deserialize the response into type T.
//                    if (method == HttpMethod.Get)
//                    {
//                        JsonSerializerOptions jsonOptions = new JsonSerializerOptions
//                        {
//                            ReferenceHandler = ReferenceHandler.Preserve,
//                            PropertyNameCaseInsensitive = true,
//                            WriteIndented = true,
//                        };
//                        T result = JsonSerializer.Deserialize<T>(jsonResponse, jsonOptions)!;
//                        U convertedResult = (U)(object)result;
//                        return Result<U>.Success(convertedResult);
//                    }
//                    // Return success with the default value for type U.
//                    return Result<U>.Success(default!, jsonResponse);
//                }
//                else if (typeof(U) == typeof(List<T>))
//                {
//                    // Deserialize the response into a list of type T.
//                    JsonSerializerOptions jsonOptions = new JsonSerializerOptions
//                    {
//                        ReferenceHandler = ReferenceHandler.Preserve,
//                        PropertyNameCaseInsensitive = true,
//                        WriteIndented = true,
//                    };
//                    List<T> result = JsonSerializer.Deserialize<List<T>>(jsonResponse, jsonOptions)!;
//                    U convertedResult = (U)(object)result;
//                    return Result<U>.Success(convertedResult);
//                }
//                else
//                {
//                    // Return failure if the response type does not match the expected type.
//                    return Result<U>.Fail("Error: Response type does not match the expected type.");
//                }
//            }
//            catch (Exception ex)
//            {
//                // Return failure if an exception occurs.
//                return Result<U>.Fail($"Error: {ex.Message}");
//            }
//        }
//    }
//}

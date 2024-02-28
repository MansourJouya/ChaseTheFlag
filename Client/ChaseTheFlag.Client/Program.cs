global using Microsoft.AspNetCore.Components.Authorization;
using Blazored.SessionStorage;
using ChaseTheFlag.Client;
using ChaseTheFlag.Client.Data.Authentication;
using ChaseTheFlag.Client.Data.Dialog;
using ChaseTheFlag.Client.Data.Snak;
using ChaseTheFlag.Client.Request;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using System.IdentityModel.Tokens.Jwt;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();


// -*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-
builder.Services.AddSingleton<JwtSecurityTokenHandler>();
builder.Services.AddScoped<RequestHandler>();
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<MyDialog>();
builder.Services.AddScoped<SnackbarManager>();

builder.Services.AddMudServices();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredSessionStorage();
// -*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-

await builder.Build().RunAsync();

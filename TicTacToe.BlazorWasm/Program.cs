using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TicTacToe.BlazorWasm;
using TicTacToe.BlazorWasm.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://gestionhuacalesapi.azurewebsites.net/") });
builder.Services.AddScoped<IJugadoreApiService, JugadoresApiServices>();
builder.Services.AddScoped<IPartidasApiService, PartidasApiService>();
builder.Services.AddScoped<IMovimientoApiService, MovimientoApiService>();

await builder.Build().RunAsync();

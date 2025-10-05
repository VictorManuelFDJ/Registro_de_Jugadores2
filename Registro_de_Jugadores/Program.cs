using Blazored.Toast;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Registro_de_Jugadores.Components;
using Registro_de_Jugadores.DAL;
using Registro_de_Jugadores.Models;
using Registro_de_Jugadores.Services;
using Registro_de_Jugadores.Hubs;

var builder = WebApplication.CreateBuilder(args);

var ConStr = builder.Configuration.GetConnectionString("SqlConStr");
builder.Services.AddDbContextFactory<Contexto>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConStr")));



builder.Services.AddScoped<JugadoresServices>();
builder.Services.AddScoped<PartidasService>();
builder.Services.AddSignalR();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddBlazoredToast();
builder.Services.AddSweetAlert2();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();



app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();
app.MapHub<GameHub>("/gamehub");

app.Run();
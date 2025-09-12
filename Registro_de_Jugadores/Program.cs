using Microsoft.EntityFrameworkCore;
using Registro_de_Jugadores.Components;
using Registro_de_Jugadores.DAL;
using Registro_de_Jugadores.Models;
using Registro_de_Jugadores.Services;

var builder = WebApplication.CreateBuilder(args);

var ConStr = builder.Configuration.GetConnectionString("SqlConStr");
builder.Services.AddDbContextFactory<Contexto>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConStr")));



builder.Services.AddScoped<JugadoresServices>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

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


app.Run();
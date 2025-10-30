using System.Net.Http.Json;
using Registro_de_Jugadores;
using Registro_de_Jugadores.DTOs;

namespace Registro_de_Jugadores.Services;

public class JugadoresApiService(HttpClient httpClient) : IJugadoresApiService
{
    public async Task<Resource<List<JugadorResponse>>> GetJugadoresAsync()
    {
        try
        {
            var response = await httpClient.GetFromJsonAsync<List<JugadorResponse>>("api/Jugadores");
            return new Resource<List<JugadorResponse>>.Success(response ?? []);
        }
        catch (Exception ex)
        {
            return new Resource<List<JugadorResponse>>.Error(ex.Message);
        }
    }

    public async Task<Resource<JugadorResponse>> GetJugadoresAsync(int JugadorId)
    {
        try
        {
            var response = await httpClient.GetFromJsonAsync<JugadorResponse>($"api/Jugadores/{JugadorId}");
            return new Resource<JugadorResponse>.Success(response!);
        }
        catch (Exception ex)
        {
            return new Resource<JugadorResponse>.Error(ex.Message);
        }
    }

    public async Task<Resource<JugadorResponse>> PostJugadores(string nombre, string email)
    {
        var request = new JugadorRequest(nombre, email);
        try
        {
            var response = await httpClient.PostAsJsonAsync("api/Jugadores", request);
            response.EnsureSuccessStatusCode();
            var created = await response.Content.ReadFromJsonAsync<JugadorResponse>();
            return new Resource<JugadorResponse>.Success(created!);
        }
        catch (HttpRequestException ex)
        {
            return new Resource<JugadorResponse>.Error($"Error de red: {ex.Message}");
        }
        catch (NotSupportedException)
        {
            return new Resource<JugadorResponse>.Error("Respuesta inválida del servidor.");
        }
    }

    public async Task<Resource<JugadorResponse>> PutJugador(int JugadorId, string nombre, string email)
    {
        var request = new JugadorRequest(nombre, email);
        try
        {
            var response = await httpClient.PutAsJsonAsync($"api/Jugadores/{JugadorId}", request);
            response.EnsureSuccessStatusCode();
            return new Resource<JugadorResponse>.Success(null!);
        }
        catch (HttpRequestException ex)
        {
            return new Resource<JugadorResponse>.Error($"Error de red: {ex.Message}");
        }
        catch (NotSupportedException)
        {
            return new Resource<JugadorResponse>.Error("Respuesta inválida del servidor.");
        }
    }
}

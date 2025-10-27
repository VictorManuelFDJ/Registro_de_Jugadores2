using System.Net.Http.Json;
using TicTacToe.Shared;
using TicTacToe.Shared.DTOs;

namespace TicTacToe.BlazorWasm.Services;

public class JugadoresApiServices(HttpClient httpClient) : IJugadoreApiService
{

    public async Task<Resource<JugadorResponse>> GetJugadorAsync(int JugadorId)
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

    public async Task<Resource<JugadorResponse>> PostJugador(string nombres, string email)
    {
        var request = new JugadorRequest(nombres, email);
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
}
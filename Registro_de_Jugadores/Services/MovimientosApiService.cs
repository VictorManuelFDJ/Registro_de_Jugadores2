using System.Net.Http.Json;
using Registro_de_Jugadores;
using Registro_de_Jugadores.DTOs;
namespace Registro_de_Jugadores.Services;

public class MovimientosApiService(HttpClient httpClient) : IMovimientosApiService
{
    public async Task<Resource<List<MovimientoResponse>>> GetMovimientoAsync(int partidaId)
    {
        try
        {
            var response = await httpClient.GetFromJsonAsync<List<MovimientoResponse>>($"api/Movimientos/{partidaId}");
            return new Resource<List<MovimientoResponse>>.Success(response ?? []);
        }
        catch (Exception ex)
        {
            return new Resource<List<MovimientoResponse>>.Error(ex.Message);
        }
    }

    public async Task<Resource<MovimientoResponse>> PostMovimiento(int PartidaId, string Jugador, int PosicionFila, int PosicionColumna)
    {
        var request = new MovimientoRequest(PartidaId, Jugador, PosicionFila, PosicionColumna);
        try
        {
            var response = await httpClient.PostAsJsonAsync("api/Movimientos", request);
            response.EnsureSuccessStatusCode();
            var created = await response.Content.ReadFromJsonAsync<MovimientoResponse>();
            return new Resource<MovimientoResponse>.Success(created!);
        }
        catch (HttpRequestException ex)
        {
            return new Resource<MovimientoResponse>.Error($"Error de red: {ex.Message}");
        }
        catch (NotSupportedException)
        {
            return new Resource<MovimientoResponse>.Error("Respuesta inválida del servidor.");
        }
    }
}

using TicTacToe.Shared;
using TicTacToe.Shared.DTOs;

namespace TicTacToe.BlazorWasm.Services;

public interface IJugadoreApiService
{
    Task<Resource<List<JugadorResponse>>> GetJugadoresAsync();
    Task<Resource<JugadorResponse>> GetJugadorAsync(int jugadorId);
    Task<Resource<JugadorResponse>> PostJugador(string nombres, string email);
}

using TicTacToe.Shared;
using TicTacToe.Shared.DTOs;

namespace Registro_de_Jugadores.Services;

public interface IJugadoreApiService
{
    Task<Resource<List<JugadorResponse>>> GetJugadoresAsync();
    Task<Resource<JugadorResponse>> GetJugadorAsync(int jugadorId);
    Task<Resource<JugadorResponse>> PostJugador(string nombres,string email);
}

using TicTacToe.Shared;
using TicTacToe.Shared.DTOs;

namespace Registro_de_Jugadores.Services;

public interface IMovimientoApiService
{
    Task<Resource<List<MovimientoResponse>>> GetMovimientosAsync(int partidaId);
    Task<Resource<MovimientoResponse>> PostMovimientos(int patidaId, string Jugador, int PosicionFila,int PosicionColumna);
}

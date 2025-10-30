using Registro_de_Jugadores.DTOs;

namespace Registro_de_Jugadores.Services;

public interface IMovimientosApiService
{
    Task<Resource<List<MovimientoResponse>>> GetMovimientoAsync(int partidaId);
    Task<Resource<MovimientoResponse>> PostMovimiento(int PartidaId, string Jugador, int PosicionFila, int PosicionColumna);
}

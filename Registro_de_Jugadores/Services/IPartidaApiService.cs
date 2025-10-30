using Registro_de_Jugadores;
using Registro_de_Jugadores.DTOs;

namespace Registro_de_Jugadores.Services;

public interface IPartidaApiService
{
    Task<Resource<List<PartidaResponse>>> GetPartidasAsync();
    Task<Resource<PartidaResponse>> GetPartidaAsync(int partidaId);
    Task<Resource<PartidaResponse>> PostPartida(int jugador1, int jugador2);
}

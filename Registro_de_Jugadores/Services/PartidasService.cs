using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Registro_de_Jugadores.DAL;
using Registro_de_Jugadores.Models;

namespace Registro_de_Jugadores.Services;


public class PartidasService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Guardar(Partidas partidas) 
    {
        if(partidas.PartidaId == 0) 
        {
            return await Insertar(partidas);
        }
        else 
        {
            return await Modificar(partidas);
        }
    }
    public async Task <bool>Insertar(Partidas partidas) 
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Partidas.Add(partidas);
        return await contexto .SaveChangesAsync() > 0;
    }
    public async Task<bool> Modificar(Partidas partidas) 
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(partidas);
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<bool> Existe(int partidaId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Partidas
            .AsNoTracking()
            .AnyAsync(p => p.PartidaId == partidaId);
    }
    public async Task<bool> Eliminar(int PartidaId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Partidas.
            AsNoTracking()
            .Where(p => p.PartidaId == PartidaId)
            .ExecuteDeleteAsync() > 0;
    }
    public async Task<Partidas> Buscar(int partidasId) 
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Partidas
            .Include(p => p.Jugador1)
            .Include(p => p.Jugador2)
            .Include(p => p.Ganador)
            .Include(p => p.TurnoJugador)
            .AsNoTracking().FirstOrDefaultAsync(p => p.PartidaId == partidasId);
    }
    public async Task<List<Partidas>> Listar(Expression<Func<Partidas, bool>> criterio) 
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Partidas
            .Include(p => p.Jugador1)
            .Include(p => p.Jugador2)
            .Include(p => p.Ganador)
            .Include(p => p.TurnoJugador)
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }


}


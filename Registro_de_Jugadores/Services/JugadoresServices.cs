using Microsoft.EntityFrameworkCore;
using Registro_de_Jugadores.DAL;
using Registro_de_Jugadores.Models;
using System.Linq.Expressions;

namespace Registro_de_Jugadores.Services;

public class JugadoresServices(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Guardar(Jugadores jugador)
    {
        if (!await Existe(jugador.Nombre))
        {
            return await Insertar(jugador);
        }
        else
        {
            return await Modificar(jugador);
        }
    }

    public async Task<bool> Insertar(Jugadores jugador)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Jugadores.Add(jugador);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Jugadores jugador)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(jugador);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Jugadores> Buscar(int jugadorId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Jugadores.FirstOrDefaultAsync(j => j.JugadorId == jugadorId);
    }

    public async Task<bool> Existe(string nombres)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Jugadores
            .AnyAsync(j => j.Nombre == nombres);
    }

    public async Task<bool> Eliminar(int jugadorId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Jugadores.
            AsNoTracking()
            .Where(j => j.JugadorId == jugadorId)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Jugadores>> Listar(Expression<Func<Jugadores, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Jugadores
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }


}
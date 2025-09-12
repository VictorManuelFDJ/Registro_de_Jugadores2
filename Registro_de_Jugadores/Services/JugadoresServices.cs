using Microsoft.EntityFrameworkCore;
using Registro_de_Jugadores.DAL;
using Registro_de_Jugadores.Models;
using System.Linq.Expressions;

namespace Registro_de_Jugadores.Services;

    public class JugadoresServices(IDbContextFactory<Contexto> DbFactory)
    {
        
        private async Task<bool> Existe(int jugadorId)
        {
           await using var context = await DbFactory.CreateDbContextAsync();
            return await context.Jugadores
                .AnyAsync(j => j.JugadorId == jugadorId);
        }

        
        private async Task<bool> NombreExiste(string nombre, int jugadorId = 0)
        {
             await using var context = await DbFactory.CreateDbContextAsync();
            
            return await context.Jugadores
                .AnyAsync(j => j.Nombre.ToLower() == nombre.ToLower() && j.JugadorId != jugadorId);
        }

        public async Task<bool> Guardar(Jugadores jugador)
        {
            if (await NombreExiste(jugador.Nombre!, jugador.JugadorId)) 
            {
               return false;
            }
            if (jugador.JugadorId == 0)
            {
                return await Insertar(jugador);
            }
            else 
            {
                if(!await Existe(jugador.JugadorId)) 
                { 
                  return false;
                }
                return await Modificar(jugador);
            }
        }

        private async Task<bool> Insertar(Jugadores jugador)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            context.Add(jugador);
            return await context.SaveChangesAsync() >0;
            
        }

        private async Task<bool> Modificar(Jugadores jugador)
        {
           await using var context = await DbFactory.CreateDbContextAsync();
            context.Update(jugador);
            return  await context.SaveChangesAsync() >0;
            
        }

        public async Task<bool> Eliminar(int jugadorId)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            return await context.Jugadores.Where
                (j => j.JugadorId == jugadorId)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<Jugadores?> Buscar(int jugadorId)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
        return await context.Jugadores.AsNoTracking().FirstOrDefaultAsync(j => j.JugadorId == jugadorId);
                
        }

        public async Task<List<Jugadores>> Listar(Expression<Func<Jugadores, bool>> criterio)
        {
           await using var context =await  DbFactory.CreateDbContextAsync();
            return await context.Jugadores
                .Where(criterio)
                .AsNoTracking()
                .ToListAsync(); 
        }

       
    }

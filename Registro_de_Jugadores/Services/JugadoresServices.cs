using Microsoft.EntityFrameworkCore;
using Registro_de_Jugadores.DAL;
using Registro_de_Jugadores.Models;
using System.Linq.Expressions;

namespace Registro_de_Jugadores.Services
{
    public class JugadoresServices
    {
        private readonly IDbContextFactory<Contexto> _contextFactory;

        public JugadoresServices(IDbContextFactory<Contexto> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<bool> Existe(int jugadorId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Jugadores.AnyAsync(j => j.JugadorId == jugadorId);
        }

        // --- CAMBIO EN ESTE MÉTODO ---
        private async Task<bool> NombreExiste(string nombre)
        {
            using var context = _contextFactory.CreateDbContext();
            // Se usa "Nombre" en lugar de "Nombres"
            return await context.Jugadores.AnyAsync(j => j.Nombre != null && j.Nombre.ToLower() == nombre.ToLower());
        }

        public async Task<Jugadores?> Guardar(Jugadores jugador)
        {
            if (jugador.JugadorId == 0)
            {
                // Se usa "Nombre" en lugar de "Nombres"
                if (await NombreExiste(jugador.Nombre!))
                {
                    return null;
                }
                return await Insertar(jugador);
            }
            else
            {
                if (!await Existe(jugador.JugadorId))
                {
                    return null;
                }
                return await Modificar(jugador);
            }
        }

        private async Task<Jugadores?> Insertar(Jugadores jugador)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Jugadores.Add(jugador);
            await context.SaveChangesAsync();
            return jugador;
        }

        private async Task<Jugadores?> Modificar(Jugadores jugador)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Entry(jugador).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return jugador;
        }

        public async Task<bool> Eliminar(int jugadorId)
        {
            using var context = _contextFactory.CreateDbContext();
            var jugador = await context.Jugadores.FindAsync(jugadorId);
            if (jugador == null)
            {
                return false;
            }
            context.Jugadores.Remove(jugador);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<Jugadores?> Buscar(int jugadorId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Jugadores.FindAsync(jugadorId);
        }

        public async Task<List<Jugadores>> Listar(Expression<Func<Jugadores, bool>> criterio)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Jugadores.AsNoTracking().Where(criterio).ToListAsync();
        }

        public async Task<List<Jugadores>> ListarTodos()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Jugadores.AsNoTracking().ToListAsync();
        }
    }
}
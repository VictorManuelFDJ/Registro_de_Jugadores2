using Microsoft.EntityFrameworkCore;
using Registro_de_Jugadores.Models;

namespace Registro_de_Jugadores.DAL
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }

        public DbSet<Jugadores> Jugadores { get; set; }
    }
}
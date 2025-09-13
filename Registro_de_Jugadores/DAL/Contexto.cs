using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Registro_de_Jugadores.Models;

namespace Registro_de_Jugadores.DAL;
    public class Contexto : DbContext
    {
        public DbSet<Jugadores> Jugadores { get; set; }
        public DbSet<Partidas> Partidas { get; set;} 
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Partidas>(entity =>
            {
                //Jugador1
                entity.HasOne(p => p.Jugador1)
                     .WithMany()
                     .HasForeignKey(p => p.Jugador1Id)
                     .OnDelete(DeleteBehavior.NoAction);
                //Jugador2 
                entity.HasOne(p => p.Jugador2)
                      .WithMany()
                      .HasForeignKey(p => p.Jugador2Id)
                      .OnDelete(DeleteBehavior.NoAction);
                //Ganador
                entity.HasOne(p => p.Ganador)
                       .WithMany()
                       .HasForeignKey(p => p.GanadorId)
                       .OnDelete(DeleteBehavior.NoAction);
                //TurnoJugador
                entity.HasOne(p => p.TurnoJugador)
                       .WithMany()
                       .HasForeignKey(p => p.TurnoJugadorId)
                       .OnDelete(DeleteBehavior.NoAction);
            });
        }

        
    }
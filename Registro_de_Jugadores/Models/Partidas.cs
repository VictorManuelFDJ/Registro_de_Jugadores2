using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Registro_de_Jugadores.Models;

public class Partidas
{
    //comentario
    [Key]
    public int PartidaId { get; set; }

    public int Jugador1Id { get; set; }
    public int? Jugador2Id { get; set; }

    [Required(ErrorMessage = "EL campo estado de la partida es obligatorio")]
    [StringLength(20)]
    public string EstadoPartida { get; set; } = string.Empty;

    public int? GanadorId { get; set; }
    public int TurnoJugadorId { get; set; }

    [StringLength(9)]
    public string EstadoTablero { get; set; }

    public DateTime FechaInicio { get; set; } = DateTime.UtcNow;
    public DateTime? FechaFin { get; set; }

    // Propiedades de navegación
    [ForeignKey(nameof(Jugador1Id))]
    public virtual Jugadores Jugador1 { get; set; }

    [ForeignKey(nameof(Jugador2Id))]
    public virtual Jugadores Jugador2 { get; set; }

    [ForeignKey(nameof(GanadorId))]
    public virtual Jugadores Ganador { get; set; }

    [ForeignKey(nameof(TurnoJugadorId))]
    public virtual Jugadores TurnoJugador { get; set; }
}


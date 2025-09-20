using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Registro_de_Jugadores.Models;
 public class Jugadores
 {
    [Key]
    public int JugadorId { get; set; }
    [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
    public string Nombre { get; set; } = string.Empty;
    [Required(ErrorMessage = "El campo Victorias es obligatorio.")]
    [Range(0, int.MaxValue, ErrorMessage = "La cantidad de Victorias debe ser un número positivo.")]
    public int Victorias { get; set; } = 0;
    [Required(ErrorMessage = "El campo Derrotas es obligatorio.")]
    [Range(0, int.MaxValue, ErrorMessage = "La cantidad de Derrotas debe ser un número positivo.")]
    public int Derrotas { get; set; } = 0;
    [Required(ErrorMessage = "El campo Empate es obligatorio.")]
    [Range(0, int.MaxValue, ErrorMessage = "La cantidad de Empate debe ser un número positivo.")]
    public int Empate { get; set; } = 0;
    [InverseProperty(nameof(Models.Movimientos.Jugador))]
    public virtual ICollection<Movimientos> Movimiento { get; set; } = new List<Movimientos>();


 }
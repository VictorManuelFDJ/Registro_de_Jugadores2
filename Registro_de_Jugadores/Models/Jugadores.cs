using System.ComponentModel.DataAnnotations;

namespace Registro_de_Jugadores.Models
{
    public class Jugadores
    {
        [Key]
        public int JugadorId { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        public string? Nombre { get; set; } 

        [Required(ErrorMessage = "El campo Partidas es obligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad de partidas debe ser un número positivo.")]
        public int Partidas { get; set; }
    }
}
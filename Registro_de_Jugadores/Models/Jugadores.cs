using System.ComponentModel.DataAnnotations;

namespace Registro_de_Jugadores.Models;
    public class Jugadores
    {
        [Key]
        public int JugadorId { get; set; }
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        public string Nombre { get; set; } = string.Empty;
        [Required(ErrorMessage = "El campo Victorias es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad de partidas debe ser un número positivo.")]
        public int Victorias { get; set; } = 0;
        public int Derrotas { get; set; } = 0;
        public int Empate { get; set; } = 0;
    
    }
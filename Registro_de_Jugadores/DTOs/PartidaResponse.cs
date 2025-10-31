using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registro_de_Jugadores.DTOs;

public record PartidaResponse(
    int PartidaId,
    int Jugador1Id,
    int? Jugador2Id
);

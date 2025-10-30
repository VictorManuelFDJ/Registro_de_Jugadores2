using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registro_de_Jugadores.DTOs;

public record JugadorRequest(
    string Nombres, 
    string Email
);

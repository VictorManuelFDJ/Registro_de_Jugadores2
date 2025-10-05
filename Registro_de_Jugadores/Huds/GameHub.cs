using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Registro_de_Jugadores.Services; 
using Registro_de_Jugadores.Models;

namespace Registro_de_Jugadores.Hubs
{
    public class GameHub : Hub
    {
        private readonly PartidasService _partidasService;

        public GameHub(PartidasService partidasService)
        {
            _partidasService = partidasService;
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
           
            return base.OnDisconnectedAsync(exception);
        }

        public async Task JoinGame(int partidaId, int jugadorId)
        {
            var groupName = $"partida-{partidaId}";
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            var partida = await _partidasService.Buscar(partidaId);
            if (partida != null)
            {
                await Clients.Caller.SendAsync("ReceiveState",
                    partida.EstadoTablero ?? "---------",
                    partida.TurnoJugadorId,
                    partida.EstadoPartida ?? "Desconocido",
                    partida.GanadorId);
            }
        }

        public async Task MakeMove(int partidaId, int index, int jugadorId)
        {
            if (index < 0 || index > 8)
            {
                await Clients.Caller.SendAsync("Error", "Posición inválida");
                return;
            }

            var partida = await _partidasService.Buscar(partidaId);
            if (partida == null)
            {
                await Clients.Caller.SendAsync("Error", "Partida no encontrada");
                return;
            }

            if (partida.TurnoJugadorId != jugadorId)
            {
                await Clients.Caller.SendAsync("Error", "No es tu turno");
                return;
            }

            var board = (partida.EstadoTablero ?? "---------").PadRight(9, '-').ToCharArray();
            if (board[index] != '-')
            {
                await Clients.Caller.SendAsync("Error", "Casilla ocupada");
                return;
            }

            var playerChar = jugadorId == partida.Jugador1Id ? 'X' : 'O';
            board[index] = playerChar;
            partida.EstadoTablero = new string(board);

            
            int[][] lines = new[]
            {
                new[]{0,1,2}, new[]{3,4,5}, new[]{6,7,8},
                new[]{0,3,6}, new[]{1,4,7}, new[]{2,5,8},
                new[]{0,4,8}, new[]{2,4,6}
            };

            int? ganadorId = null;
            foreach (var line in lines)
            {
                var a = board[line[0]];
                if (a != '-' && a == board[line[1]] && a == board[line[2]])
                {
                    ganadorId = a == 'X' ? partida.Jugador1Id : partida.Jugador2Id;
                    break;
                }
            }

            if (ganadorId.HasValue)
            {
                partida.GanadorId = ganadorId;
                partida.EstadoPartida = "Finalizada";
                partida.FechaFin = DateTime.UtcNow;
            }
            else if (!partida.EstadoTablero.Contains('-'))
            {
                partida.GanadorId = null;
                partida.EstadoPartida = "Empate";
                partida.FechaFin = DateTime.UtcNow;
            }
            else
            {
                
                partida.TurnoJugadorId = (partida.TurnoJugadorId == partida.Jugador1Id) ? partida.Jugador2Id : partida.Jugador1Id;
                partida.EstadoPartida = "En Proceso";
            }

            await _partidasService.Guardar(partida);

            var groupName = $"partida-{partidaId}";
            await Clients.Group(groupName).SendAsync("ReceiveState",
                partida.EstadoTablero,
                partida.TurnoJugadorId,
                partida.EstadoPartida,
                partida.GanadorId);
        }
    }
}

using Microsoft.AspNetCore.SignalR;
using RPSLS.Api.Data.Enums;
using RPSLS.Api.Interfaces;
using RPSLS.Api.Services;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace RPSLS.Api.Hubs
{
    public class GameHub(IGameService game) : Hub
    {
        private readonly IGameService gameService = game;

        public async Task CreateRoom(string roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        }

        public async Task JoinRoom(string roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);

            gameService.UpdateStatus(Guid.Parse(roomId), Status.GameStarted);

            await Clients.Group(roomId).SendAsync("PlayerJoined", $"{Context.ConnectionId} joined the room.", Status.GameStarted);
        }

        public async Task LeaveRoom(string roomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);

            gameService.UpdateStatus(Guid.Parse(roomId), Status.WaitingForPlayerToJoin);

            await Clients.Group(roomId).SendAsync("PlayerLeft", $"{Context.ConnectionId} left the room.");

        }

        public async Task MakeMove(string roomId, Guid gameId, Guid userId, Move move)
        {
            (Status status, string result) = gameService.MakeMove(gameId, userId, move);

            await Clients.Group(roomId).SendAsync("MakeMove", Context.ConnectionId, status, userId, result);
        }

        public async Task CreateNewGame(string roomId)
        {
            var game = gameService.CreateGame(Guid.Parse(roomId));

            await Clients.Group(roomId).SendAsync("StartNewGame", $"Start a new game!", game);
        }
    }
}

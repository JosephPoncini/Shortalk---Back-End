using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Shortalk___Back_End.Models;
using Shortalk___Back_End.Services;

namespace Shortalk___Back_End.Hubs;
public class GameHub : Hub
{
    private readonly SharedDb _shared;
    private readonly GameService _data;

    public GameHub(SharedDb shared, GameService data)
    {
        _shared = shared;
        _data = data;
    }

    public async Task JoinSpecificGame(UserConnection conn)
    {

        await Groups.AddToGroupAsync(Context.ConnectionId, conn.LobbyRoom);

        _shared.connections[Context.ConnectionId] = conn;

        await Clients.Group(conn.LobbyRoom)
            .SendAsync("JoinSpecificGame", "admin", $"{conn.Username} has joined the game");

    }

    public async Task GetNextCard(UserConnection conn)
    {
        if (_data.GetNextCard(conn.LobbyRoom))
        {
            GameModel game = _data.GetGameByLobbyName(conn.LobbyRoom);
            string json = JsonConvert.SerializeObject(game);
            await Clients.Group(conn.LobbyRoom)
                .SendAsync("GetNextCard", json);
        }
    }

    public async Task SubmitGuess(string msg)
    {
        if (_shared.connections.TryGetValue(Context.ConnectionId, out UserConnection conn))
        {
            await Clients.Group(conn.LobbyRoom)
                .SendAsync("ReceiveGuess", conn.Username, msg);
        }
    }

    public async Task TypeDescription(string description)
    {
        if (_shared.connections.TryGetValue(Context.ConnectionId, out UserConnection conn))
        {
            await Clients.Group(conn.LobbyRoom)
                .SendAsync("RenderDescription", description);
        }
    }
}

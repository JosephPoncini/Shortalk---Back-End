using Microsoft.AspNetCore.SignalR;
using Shortalk___Back_End.Models;
using Shortalk___Back_End.Services;

namespace Shortalk___Back_End.Hubs;
public class LobbyHub : Hub
{
    private readonly SharedDb _shared;
    public LobbyHub(SharedDb shared) => _shared = shared;

    public async Task JoinLobby(UserConnection conn)
    {
        await Clients.All
            .SendAsync("ReceiveMessage", "admin", $"{conn.Username} has joined the lobby");
    }

    public async Task JoinSpecificChatRoom(UserConnection conn)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, conn.LobbyRoom);

        _shared.connections[Context.ConnectionId] = conn;

        await Clients.Group(conn.LobbyRoom)
            .SendAsync("JoinSpecificChatRoom", "admin", $"{conn.Username} has joined {conn.LobbyRoom}");
    }

    public async Task SendMessage(string msg)
    {
        if (_shared.connections.TryGetValue(Context.ConnectionId, out UserConnection conn))
        {
            await Clients.Group(conn.LobbyRoom)
                .SendAsync("ReceiveSpecificMessage", conn.Username, msg);
        }
    }

}

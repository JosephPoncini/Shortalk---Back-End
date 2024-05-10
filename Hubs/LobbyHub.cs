using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Shortalk___Back_End.Models;
using Shortalk___Back_End.Services;

namespace Shortalk___Back_End.Hubs;
public class LobbyHub : Hub
{

    private readonly SharedDb _shared;
    private readonly LobbyService _data;
    public LobbyHub(SharedDb shared, LobbyService data)
    {
        _shared = shared;
        _data = data;
    }


    public async Task JoinLobby(UserConnection conn)
    {
        LobbyRoomModel lobby = _data.GetLobbyByLobbyName(conn.LobbyRoom);

        string json = JsonConvert.SerializeObject(lobby);

        await Clients.All
            // .SendAsync("ReceiveMessage", "admin", $"{conn.Username} has joined the lobby");
            .SendAsync("ReceiveMessage", "admin", json);
    }

    public async Task UpdateSpecificLobbyRoom(UserConnection conn)
    {
        LobbyRoomModel lobby = _data.GetLobbyByLobbyName(conn.LobbyRoom);

        string json = JsonConvert.SerializeObject(lobby);

        await Clients.Group(conn.LobbyRoom)
            .SendAsync("UpdateSpecificLobbyRoom", "admin", json);
    }

    public async Task JoinSpecificLobbyRoom(UserConnection conn)
    {
        if (!_data.DoesUserExistInLobby(conn.LobbyRoom, conn.Username))
        {
            bool res = _data.AddPlayerToLobby(conn.LobbyRoom, conn.Username);
        }

        LobbyRoomModel lobby = _data.GetLobbyByLobbyName(conn.LobbyRoom);

        string json = JsonConvert.SerializeObject(lobby);

        await Groups.AddToGroupAsync(Context.ConnectionId, conn.LobbyRoom);

        _shared.connections[Context.ConnectionId] = conn;

        await Clients.Group(conn.LobbyRoom)
            .SendAsync("JoinSpecificLobbyRoom", "admin", $"{conn.Username} has joined {conn.LobbyRoom}", json);

    }

    public async Task SendMessage(string msg)
    {
        if (_shared.connections.TryGetValue(Context.ConnectionId, out UserConnection conn))
        {
            await Clients.Group(conn.LobbyRoom)
                .SendAsync("ReceiveSpecificMessage", conn.Username, msg);
        }
    }

    public async Task TogglePayerAsReady(UserConnection conn)
    {
        if (_data.TogglePlayerAsReady(conn.LobbyRoom, conn.Username))
        {
            LobbyRoomModel lobby = _data.GetLobbyByLobbyName(conn.LobbyRoom);

            string json = JsonConvert.SerializeObject(lobby);

            await Clients.Group(conn.LobbyRoom)
                .SendAsync("TogglePayerAsReady", json);
        }
    }

}

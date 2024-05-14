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

    public async Task GetGameInfo(UserConnection conn, string host)
    {   
        

        await Clients.Group(conn.LobbyRoom)
            .SendAsync("GetGameInfo", "admin", $"{conn.Username} has joined {conn.LobbyRoom}");
    }
}

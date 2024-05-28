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



    public override async Task OnDisconnectedAsync(Exception exception)
    {
        // Perform actions when a client disconnects
        await base.OnDisconnectedAsync(exception);

        if (_shared.connections.TryGetValue(Context.ConnectionId, out UserConnection conn))
        {
            if (_data.RemovePlayerFromLobby(conn.LobbyRoom, conn.Username))
            {
                LobbyRoomModel lobby = _data.GetLobbyByLobbyName(conn.LobbyRoom);

                if (lobby.TeamMemberA1 == ""
                    && lobby.TeamMemberA2 == ""
                    && lobby.TeamMemberA3 == ""
                    && lobby.TeamMemberA4 == ""
                    && lobby.TeamMemberA5 == ""
                    && lobby.TeamMemberB1 == ""
                    && lobby.TeamMemberB2 == ""
                    && lobby.TeamMemberB3 == ""
                    && lobby.TeamMemberB4 == ""
                    && lobby.TeamMemberB5 == ""
                    )
                {
                    _data.DeleteLobby(conn.LobbyRoom);

                    await Clients.All
                        .SendAsync("OnHostDisconnectedAsync");
                }
                else
                {
                    string json = JsonConvert.SerializeObject(lobby);

                    await Clients.All
                        .SendAsync("OnDisconnectedAsync", $"{conn.Username} has successfully disconnected", json);
                }

            }
            else
            {
                await Clients.All
                    .SendAsync("OnDisconnectedAsync", $"{conn.Username} has unsuccessfully disconnected", "");
            }


        }



    }

    public async Task RemovePlayer(string playerName, string lobbyName)
    {
        if (_data.RemovePlayerFromLobby(lobbyName, playerName))
        {
            LobbyRoomModel lobby = _data.GetLobbyByLobbyName(lobbyName);
            string json = JsonConvert.SerializeObject(lobby);
            await Clients.All
                .SendAsync("RemovePlayer",playerName,json);

        }
    }

    public async Task ToggleTeam(UserConnection conn)
    {
        if (_data.ToggleTeam(conn.LobbyRoom, conn.Username))
        {
            LobbyRoomModel lobby = _data.GetLobbyByLobbyName(conn.LobbyRoom);

            string json = JsonConvert.SerializeObject(lobby);

            await Clients.All
                // .SendAsync("ReceiveMessage", "admin", $"{conn.Username} has joined the lobby");
                .SendAsync("ToggleTeam", json);
        }
    }


    public async Task ShuffleTeams(UserConnection conn)
    {
        if (_data.ShuffleTeams(conn.LobbyRoom))
        {
            LobbyRoomModel lobby = _data.GetLobbyByLobbyName(conn.LobbyRoom);

            string json = JsonConvert.SerializeObject(lobby);

            await Clients.All
                // .SendAsync("ReceiveMessage", "admin", $"{conn.Username} has joined the lobby");
                .SendAsync("ShuffleTeams", json);
        }
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

    public async Task RemoveFromGroup(UserConnection conn)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, conn.LobbyRoom);
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

    public async Task ChangeNumberOfRounds(UserConnection conn, string NumberOfRounds)
    {
        if (_data.ChangeNumberOfRounds(conn.LobbyRoom, NumberOfRounds))
        {
            LobbyRoomModel lobby = _data.GetLobbyByLobbyName(conn.LobbyRoom);

            string json = JsonConvert.SerializeObject(lobby);

            await Clients.Group(conn.LobbyRoom)
                .SendAsync("ChangeNumberOfRounds", json);
        }
    }

    public async Task ChangeTimeLimit(UserConnection conn, string TimeLimit)
    {
        if (_data.ChangeTimeLimit(conn.LobbyRoom, TimeLimit))
        {
            LobbyRoomModel lobby = _data.GetLobbyByLobbyName(conn.LobbyRoom);

            string json = JsonConvert.SerializeObject(lobby);

            await Clients.Group(conn.LobbyRoom)
                .SendAsync("ChangeTimeLimit", json);
        }
    }

    public async Task StartGame(UserConnection conn)
    {
        await Clients.Group(conn.LobbyRoom)
            .SendAsync("StartGame");
    }

}

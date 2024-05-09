using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Shortalk___Back_End.Models;
using Shortalk___Back_End.Models.DTO;
using Shortalk___Back_End.Services.Context;

namespace Shortalk___Back_End.Services
{
    public class LobbyService
    {
        private readonly DataContext _context;

        public LobbyService(DataContext context)
        {
            _context = context;
        }

        public bool DoesLobbyExist(string lobbyName)
        {
            return _context.LobbyInfo.SingleOrDefault(lobby => lobby.LobbyName == lobbyName) != null;
        }

        public bool AddLobbyRoom(CreateLobbyRoomDTO LobbyToAdd)
        {
            bool result = false;

            if (!DoesLobbyExist(LobbyToAdd.LobbyName))
            {
                LobbyRoomModel newLobby = new LobbyRoomModel();

                newLobby.ID = LobbyToAdd.ID;
                newLobby.LobbyName = LobbyToAdd.LobbyName;
                newLobby.TeamMemberA1 = "";
                newLobby.TeamMemberA2 = "";
                newLobby.TeamMemberA3 = "";
                newLobby.TeamMemberA4 = "";
                newLobby.TeamMemberA5 = "";
                newLobby.TeamMemberB1 = "";
                newLobby.TeamMemberB2 = "";
                newLobby.TeamMemberB3 = "";
                newLobby.TeamMemberB4 = "";
                newLobby.TeamMemberB5 = "";

                _context.Add(newLobby);

                result = _context.SaveChanges() != 0;
            }

            return result;
        }

        public LobbyRoomModel GetLobbyByLobbyName(string lobbyName)
        {
            return _context.LobbyInfo.SingleOrDefault(lobby => lobby.LobbyName == lobbyName);
        }

        public bool DeleteLobby(string lobbyToDelete)
        {
            LobbyRoomModel foundLobby = GetLobbyByLobbyName(lobbyToDelete);

            bool result = false;

            if (foundLobby != null)
            {

                _context.Remove<LobbyRoomModel>(foundLobby);
                result = _context.SaveChanges() != 0;
            }

            return result;
        }

        public bool AddPlayerToLobby(string lobbyName, string playerName)
        {
            bool result = false;

            LobbyRoomModel lobby = GetLobbyByLobbyName(lobbyName);

            if (lobby.TeamMemberA1 == "")
            {
                lobby.TeamMemberA1 = playerName;
                result = true;
            }
            else if (lobby.TeamMemberB1 == "")
            {
                lobby.TeamMemberB1 = playerName;
                result = true;
            }
            else if (lobby.TeamMemberA2 == "")
            {
                lobby.TeamMemberA2 = playerName;
                result = true;
            }
            else if (lobby.TeamMemberB2 == "")
            {
                lobby.TeamMemberB2 = playerName;
                result = true;
            }
            else if (lobby.TeamMemberA3 == "")
            {
                lobby.TeamMemberA3 = playerName;
                result = true;
            }
            else if (lobby.TeamMemberB3 == "")
            {
                lobby.TeamMemberB3 = playerName;
                result = true;
            }
            else if(lobby.TeamMemberA4 == "")
            {
                lobby.TeamMemberA4 = playerName;
                result = true;
            }
            else if(lobby.TeamMemberB4 == "")
            {
                lobby.TeamMemberB4 = playerName;
                result = true;
            }
            else if(lobby.TeamMemberA5 == "")
            {
                lobby.TeamMemberA5 = playerName;
                result = true;
            }
            else if(lobby.TeamMemberB5 == "")
            {
                lobby.TeamMemberB5 = playerName;
                result = true;
            }

            if(result){
                _context.Update<LobbyRoomModel>(lobby);
                result = _context.SaveChanges() != 0;
            }

            return result;
        }

    }
}
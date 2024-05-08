using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            if(!DoesLobbyExist(LobbyToAdd.LobbyName))
            {
                LobbyRoomModel newLobby = new LobbyRoomModel();

                newLobby.ID = LobbyToAdd.ID;
                newLobby.LobbyName = LobbyToAdd.LobbyName;

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

            if(foundLobby != null){

                _context.Remove<LobbyRoomModel>(foundLobby);
                result = _context.SaveChanges() != 0;
            }

            return result;
        }

    }
}
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
                newLobby.Host = LobbyToAdd.Host;
                newLobby.NumberOfRounds = 1;
                newLobby.TimeLimit = 90;
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

                newLobby.ReadyStatusA1 = true;
                newLobby.ReadyStatusA2 = false;
                newLobby.ReadyStatusA3 = false;
                newLobby.ReadyStatusA4 = false;
                newLobby.ReadyStatusA5 = false;
                newLobby.ReadyStatusB1 = false;
                newLobby.ReadyStatusB2 = false;
                newLobby.ReadyStatusB3 = false;
                newLobby.ReadyStatusB4 = false;
                newLobby.ReadyStatusB5 = false;

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
            else if (lobby.TeamMemberA4 == "")
            {
                lobby.TeamMemberA4 = playerName;
                result = true;
            }
            else if (lobby.TeamMemberB4 == "")
            {
                lobby.TeamMemberB4 = playerName;
                result = true;
            }
            else if (lobby.TeamMemberA5 == "")
            {
                lobby.TeamMemberA5 = playerName;
                result = true;
            }
            else if (lobby.TeamMemberB5 == "")
            {
                lobby.TeamMemberB5 = playerName;
                result = true;
            }

            if (result)
            {
                _context.Update<LobbyRoomModel>(lobby);
                result = _context.SaveChanges() != 0;
            }

            return result;
        }

        public bool DoesUserExistInLobby(string lobbyName, string playerName)
        {
            bool result = false;

            LobbyRoomModel lobby = GetLobbyByLobbyName(lobbyName);

            if (playerName == lobby.TeamMemberA1 ||
                playerName == lobby.TeamMemberA2 ||
                playerName == lobby.TeamMemberA3 ||
                playerName == lobby.TeamMemberA4 ||
                playerName == lobby.TeamMemberA5 ||
                playerName == lobby.TeamMemberB1 ||
                playerName == lobby.TeamMemberB2 ||
                playerName == lobby.TeamMemberB3 ||
                playerName == lobby.TeamMemberB4 ||
                playerName == lobby.TeamMemberB5)
            {
                result = true;
            }

            return result;
        }

        public bool TogglePlayerAsReady(string lobbyName, string playerName)
        {
            bool result = false;

            LobbyRoomModel lobby = GetLobbyByLobbyName(lobbyName);

            switch (playerName)
            {
                case var _ when playerName == lobby.TeamMemberA1:
                    lobby.ReadyStatusA1 = !lobby.ReadyStatusA1;
                    result = true;
                    break;
                case var _ when playerName == lobby.TeamMemberA2:
                    lobby.ReadyStatusA2 = !lobby.ReadyStatusA2;
                    result = true;
                    break;
                case var _ when playerName == lobby.TeamMemberA3:
                    lobby.ReadyStatusA3 = !lobby.ReadyStatusA3;
                    result = true;
                    break;
                case var _ when playerName == lobby.TeamMemberA4:
                    lobby.ReadyStatusA4 = !lobby.ReadyStatusA4;
                    result = true;
                    break;
                case var _ when playerName == lobby.TeamMemberA5:
                    lobby.ReadyStatusA5 = !lobby.ReadyStatusA5;
                    result = true;
                    break;
                case var _ when playerName == lobby.TeamMemberB1:
                    lobby.ReadyStatusB1 = !lobby.ReadyStatusB1;
                    result = true;
                    break;
                case var _ when playerName == lobby.TeamMemberB2:
                    lobby.ReadyStatusB2 = !lobby.ReadyStatusB2;
                    result = true;
                    break;
                case var _ when playerName == lobby.TeamMemberB3:
                    lobby.ReadyStatusB3 = !lobby.ReadyStatusB3;
                    result = true;
                    break;
                case var _ when playerName == lobby.TeamMemberB4:
                    lobby.ReadyStatusB4 = !lobby.ReadyStatusB4;
                    result = true;
                    break;
                case var _ when playerName == lobby.TeamMemberB5:
                    lobby.ReadyStatusB5 = !lobby.ReadyStatusB5;
                    result = true;
                    break;
                default:
                    Console.WriteLine("error in switch statements");
                    break;
            }


            if (result)
            {
                _context.Update<LobbyRoomModel>(lobby);
                result = _context.SaveChanges() != 0;
            }

            return result;
        }

        public bool ChangeNumberOfRounds(string lobbyName, string NumberOfRounds)
        {

            LobbyRoomModel lobby = GetLobbyByLobbyName(lobbyName);
            lobby.NumberOfRounds = int.Parse(NumberOfRounds);

            _context.Update<LobbyRoomModel>(lobby);
            bool result = _context.SaveChanges() != 0;

            return result;
        }

        public bool ChangeTimeLimit(string lobbyName, string TimeLimit)
        {

            LobbyRoomModel lobby = GetLobbyByLobbyName(lobbyName);
            lobby.TimeLimit = int.Parse(TimeLimit);

            _context.Update<LobbyRoomModel>(lobby);
            bool result = _context.SaveChanges() != 0;

            return result;
        }

    }
}
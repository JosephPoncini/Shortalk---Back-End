using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.ObjectPool;
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

        public bool ShuffleTeams(string lobbyName)
        {
            LobbyRoomModel lobby = GetLobbyByLobbyName(lobbyName);

            string[] players = new string[10];

            players[0] = lobby.TeamMemberA1;
            players[1] = lobby.TeamMemberA2;
            players[2] = lobby.TeamMemberA3;
            players[3] = lobby.TeamMemberA4;
            players[4] = lobby.TeamMemberA5;
            players[5] = lobby.TeamMemberB1;
            players[6] = lobby.TeamMemberB2;
            players[7] = lobby.TeamMemberB3;
            players[8] = lobby.TeamMemberB4;
            players[9] = lobby.TeamMemberB5;

            bool[] readyStatuses = new bool[10];

            readyStatuses[0] = lobby.ReadyStatusA1;
            readyStatuses[1] = lobby.ReadyStatusA2;
            readyStatuses[2] = lobby.ReadyStatusA3;
            readyStatuses[3] = lobby.ReadyStatusA4;
            readyStatuses[4] = lobby.ReadyStatusA5;
            readyStatuses[5] = lobby.ReadyStatusB1;
            readyStatuses[6] = lobby.ReadyStatusB2;
            readyStatuses[7] = lobby.ReadyStatusB3;
            readyStatuses[8] = lobby.ReadyStatusB4;
            readyStatuses[9] = lobby.ReadyStatusB5;

            // Shuffle the players array
            Random rnd = new Random();
            for (int i = players.Length - 1; i > 0; i--)
            {
                int j = rnd.Next(0, i + 1);
                string temp = players[i];
                bool temp2 = readyStatuses[i];
                players[i] = players[j];                
                readyStatuses[i] = readyStatuses[j];
                players[j] = temp;
                readyStatuses[j] = temp2;
            }

            string[] playersRandomized = new string[10];
            bool[] statusesRandomized = new bool[10];
            int ii = 0;

            for (int i = 0; i < players.Length; i++)
            {   
                playersRandomized[i] = "";
                statusesRandomized[i] = false;
                if (players[i] != "")
                {
                    playersRandomized[ii] = players[i];
                    statusesRandomized[ii] = readyStatuses[i];
                    ii++;
                }
            }

            int coinFlip = rnd.Next(0, 2);

            switch (coinFlip)
            {
                case 0:
                    lobby.TeamMemberA1 = playersRandomized[0];
                    lobby.TeamMemberB1 = playersRandomized[1];
                    lobby.TeamMemberA2 = playersRandomized[2];
                    lobby.TeamMemberB2 = playersRandomized[3];
                    lobby.TeamMemberA3 = playersRandomized[4];
                    lobby.TeamMemberB3 = playersRandomized[5];
                    lobby.TeamMemberA4 = playersRandomized[6];
                    lobby.TeamMemberB4 = playersRandomized[7];
                    lobby.TeamMemberA5 = playersRandomized[8];
                    lobby.TeamMemberB5 = playersRandomized[9];

                    lobby.ReadyStatusA1 = statusesRandomized[0];
                    lobby.ReadyStatusB1 = statusesRandomized[1];
                    lobby.ReadyStatusA2 = statusesRandomized[2];
                    lobby.ReadyStatusB2 = statusesRandomized[3];
                    lobby.ReadyStatusA3 = statusesRandomized[4];
                    lobby.ReadyStatusB3 = statusesRandomized[5];
                    lobby.ReadyStatusA4 = statusesRandomized[6];
                    lobby.ReadyStatusB4 = statusesRandomized[7];
                    lobby.ReadyStatusA5 = statusesRandomized[8];
                    lobby.ReadyStatusB5 = statusesRandomized[9];

                    break;
                case 1:
                    lobby.TeamMemberB1 = playersRandomized[0];
                    lobby.TeamMemberA1 = playersRandomized[1];
                    lobby.TeamMemberB2 = playersRandomized[2];
                    lobby.TeamMemberA2 = playersRandomized[3];
                    lobby.TeamMemberB3 = playersRandomized[4];
                    lobby.TeamMemberA3 = playersRandomized[5];
                    lobby.TeamMemberB4 = playersRandomized[6];
                    lobby.TeamMemberA4 = playersRandomized[7];
                    lobby.TeamMemberB5 = playersRandomized[8];
                    lobby.TeamMemberA5 = playersRandomized[9];


                    lobby.ReadyStatusB1 = statusesRandomized[0];
                    lobby.ReadyStatusA1 = statusesRandomized[1];
                    lobby.ReadyStatusB2 = statusesRandomized[2];
                    lobby.ReadyStatusA2 = statusesRandomized[3];
                    lobby.ReadyStatusB3 = statusesRandomized[4];
                    lobby.ReadyStatusA3 = statusesRandomized[5];
                    lobby.ReadyStatusB4 = statusesRandomized[6];
                    lobby.ReadyStatusA4 = statusesRandomized[7];
                    lobby.ReadyStatusB5 = statusesRandomized[8];
                    lobby.ReadyStatusA5 = statusesRandomized[9];
                    break;
            }

            _context.Update<LobbyRoomModel>(lobby);
            bool result = _context.SaveChanges() != 0;

            return result;
        }
        public bool ToggleTeam(string lobbyName, string playerName)
        {

            string spot = "";
            string spot2 = "";
            bool playerStatus = false;
            LobbyRoomModel lobby = GetLobbyByLobbyName(lobbyName);


            if (lobby.TeamMemberA1 == playerName)
            {
                spot = "A1";
                playerStatus = lobby.ReadyStatusA1;
            }
            else if (lobby.TeamMemberB1 == playerName)
            {
                spot = "B1";
                playerStatus = lobby.ReadyStatusB1;
            }
            else if (lobby.TeamMemberA2 == playerName)
            {
                spot = "A2";
                playerStatus = lobby.ReadyStatusA2;
            }
            else if (lobby.TeamMemberB2 == playerName)
            {
                spot = "B2";
                playerStatus = lobby.ReadyStatusB2;
            }
            else if (lobby.TeamMemberA3 == playerName)
            {
                spot = "A3";
                playerStatus = lobby.ReadyStatusA3;
            }
            else if (lobby.TeamMemberB3 == playerName)
            {
                spot = "B3";
                playerStatus = lobby.ReadyStatusB3;
            }
            else if (lobby.TeamMemberA4 == playerName)
            {
                spot = "A4";
                playerStatus = lobby.ReadyStatusA4;
            }
            else if (lobby.TeamMemberB4 == playerName)
            {
                spot = "B4";
                playerStatus = lobby.ReadyStatusB4;
            }
            else if (lobby.TeamMemberA5 == playerName)
            {
                spot = "A5";
                playerStatus = lobby.ReadyStatusA5;
            }
            else if (lobby.TeamMemberB5 == playerName)
            {
                spot = "B5";
                playerStatus = lobby.ReadyStatusB5;
            }
            else
            {
                return false;
            }

            if ((spot == "A1") || (spot == "A2") || (spot == "A3") || (spot == "A4") || (spot == "A5"))
            {
                if (lobby.TeamMemberB1 == "")
                {
                    spot2 = "B1";
                }
                else if (lobby.TeamMemberB2 == "")
                {
                    spot2 = "B2";
                }
                else if (lobby.TeamMemberB3 == "")
                {
                    spot2 = "B3";
                }
                else if (lobby.TeamMemberB4 == "")
                {
                    spot2 = "B4";
                }
                else if (lobby.TeamMemberB5 == "")
                {
                    spot2 = "B5";
                }
            }
            else if ((spot == "B1") || (spot == "B2") || (spot == "B3") || (spot == "B4") || (spot == "B5"))
            {
                if (lobby.TeamMemberA1 == "")
                {
                    spot2 = "A1";
                }
                else if (lobby.TeamMemberA2 == "")
                {
                    spot2 = "A2";
                }
                else if (lobby.TeamMemberA3 == "")
                {
                    spot2 = "A3";
                }
                else if (lobby.TeamMemberA4 == "")
                {
                    spot2 = "A4";
                }
                else if (lobby.TeamMemberA5 == "")
                {
                    spot2 = "A5";
                }
            }
            else
            {
                return false;
            }

            switch (spot)
            {
                case "A1":
                    lobby.TeamMemberA1 = lobby.TeamMemberA2;
                    lobby.TeamMemberA2 = lobby.TeamMemberA3;
                    lobby.TeamMemberA3 = lobby.TeamMemberA4;
                    lobby.TeamMemberA4 = lobby.TeamMemberA5;
                    lobby.TeamMemberA5 = "";
                    lobby.ReadyStatusA1 = lobby.ReadyStatusA2;
                    lobby.ReadyStatusA2 = lobby.ReadyStatusA3;
                    lobby.ReadyStatusA3 = lobby.ReadyStatusA4;
                    lobby.ReadyStatusA4 = lobby.ReadyStatusA5;
                    lobby.ReadyStatusA5 = false;
                    break;
                case "A2":
                    lobby.TeamMemberA2 = lobby.TeamMemberA3;
                    lobby.TeamMemberA3 = lobby.TeamMemberA4;
                    lobby.TeamMemberA4 = lobby.TeamMemberA5;
                    lobby.TeamMemberA5 = "";
                    lobby.ReadyStatusA2 = lobby.ReadyStatusA3;
                    lobby.ReadyStatusA3 = lobby.ReadyStatusA4;
                    lobby.ReadyStatusA4 = lobby.ReadyStatusA5;
                    lobby.ReadyStatusA5 = false;
                    break;
                case "A3":
                    lobby.TeamMemberA3 = lobby.TeamMemberA4;
                    lobby.TeamMemberA4 = lobby.TeamMemberA5;
                    lobby.TeamMemberA5 = "";
                    lobby.ReadyStatusA3 = lobby.ReadyStatusA4;
                    lobby.ReadyStatusA4 = lobby.ReadyStatusA5;
                    lobby.ReadyStatusA5 = false;
                    break;
                case "A4":
                    lobby.TeamMemberA4 = lobby.TeamMemberA5;
                    lobby.TeamMemberA5 = "";
                    lobby.ReadyStatusA4 = lobby.ReadyStatusA5;
                    lobby.ReadyStatusA5 = false;
                    break;
                case "A5":
                    lobby.TeamMemberA5 = "";
                    lobby.ReadyStatusA5 = false;
                    break;
                case "B1":
                    lobby.TeamMemberB1 = lobby.TeamMemberB2;
                    lobby.TeamMemberB2 = lobby.TeamMemberB3;
                    lobby.TeamMemberB3 = lobby.TeamMemberB4;
                    lobby.TeamMemberB4 = lobby.TeamMemberB5;
                    lobby.TeamMemberB5 = "";
                    lobby.ReadyStatusB1 = lobby.ReadyStatusB2;
                    lobby.ReadyStatusB2 = lobby.ReadyStatusB3;
                    lobby.ReadyStatusB3 = lobby.ReadyStatusB4;
                    lobby.ReadyStatusB4 = lobby.ReadyStatusB5;
                    lobby.ReadyStatusB5 = false;
                    break;
                case "B2":
                    lobby.TeamMemberB2 = lobby.TeamMemberB3;
                    lobby.TeamMemberB3 = lobby.TeamMemberB4;
                    lobby.TeamMemberB4 = lobby.TeamMemberB5;
                    lobby.TeamMemberB5 = "";
                    lobby.ReadyStatusB2 = lobby.ReadyStatusB3;
                    lobby.ReadyStatusB3 = lobby.ReadyStatusB4;
                    lobby.ReadyStatusB4 = lobby.ReadyStatusB5;
                    lobby.ReadyStatusB5 = false;
                    break;
                case "B3":
                    lobby.TeamMemberB3 = lobby.TeamMemberB4;
                    lobby.TeamMemberB4 = lobby.TeamMemberB5;
                    lobby.TeamMemberB5 = "";
                    lobby.ReadyStatusB3 = lobby.ReadyStatusB4;
                    lobby.ReadyStatusB4 = lobby.ReadyStatusB5;
                    lobby.ReadyStatusB5 = false;
                    break;
                case "B4":
                    lobby.TeamMemberB4 = lobby.TeamMemberB5;
                    lobby.TeamMemberB5 = "";
                    lobby.ReadyStatusB4 = lobby.ReadyStatusB5;
                    lobby.ReadyStatusB5 = false;
                    break;
                case "B5":
                    lobby.TeamMemberB5 = "";
                    lobby.ReadyStatusB5 = false;
                    break;
                default:
                    return false;
            }


            switch (spot2)
            {
                case "A1":
                    lobby.TeamMemberA1 = playerName;
                    lobby.ReadyStatusA1 = playerStatus;
                    break;
                case "A2":
                    lobby.TeamMemberA2 = playerName;
                    lobby.ReadyStatusA2 = playerStatus;
                    break;
                case "A3":
                    lobby.TeamMemberA3 = playerName;
                    lobby.ReadyStatusA3 = playerStatus;
                    break;
                case "A4":
                    lobby.TeamMemberA4 = playerName;
                    lobby.ReadyStatusA4 = playerStatus;
                    break;
                case "A5":
                    lobby.TeamMemberA5 = playerName;
                    lobby.ReadyStatusA5 = playerStatus;
                    break;
                case "B1":
                    lobby.TeamMemberB1 = playerName;
                    lobby.ReadyStatusB1 = playerStatus;
                    break;
                case "B2":
                    lobby.TeamMemberB2 = playerName;
                    lobby.ReadyStatusB2 = playerStatus;
                    break;
                case "B3":
                    lobby.TeamMemberB3 = playerName;
                    lobby.ReadyStatusB3 = playerStatus;
                    break;
                case "B4":
                    lobby.TeamMemberB4 = playerName;
                    lobby.ReadyStatusB4 = playerStatus;
                    break;
                case "B5":
                    lobby.TeamMemberB5 = playerName;
                    lobby.ReadyStatusB5 = playerStatus;
                    break;
                default:
                    return false;
            }

            _context.Update<LobbyRoomModel>(lobby);
            bool result = _context.SaveChanges() != 0;


            return result;
        }
        public bool RemovePlayerFromLobby(string lobbyName, string playerName)
        {
            string spot = "";

            LobbyRoomModel lobby = GetLobbyByLobbyName(lobbyName);

            if (lobby.TeamMemberA1 == playerName)
            {
                spot = "A1";
            }
            else if (lobby.TeamMemberB1 == playerName)
            {
                spot = "B1";
            }
            else if (lobby.TeamMemberA2 == playerName)
            {
                spot = "A2";
            }
            else if (lobby.TeamMemberB2 == playerName)
            {
                spot = "B2";
            }
            else if (lobby.TeamMemberA3 == playerName)
            {
                spot = "A3";
            }
            else if (lobby.TeamMemberB3 == playerName)
            {
                spot = "B3";
            }
            else if (lobby.TeamMemberA4 == playerName)
            {
                spot = "A4";
            }
            else if (lobby.TeamMemberB4 == playerName)
            {
                spot = "B4";
            }
            else if (lobby.TeamMemberA5 == playerName)
            {
                spot = "A5";
            }
            else if (lobby.TeamMemberB5 == playerName)
            {
                spot = "B5";
            }

            switch (spot)
            {
                case "A1":
                    lobby.TeamMemberA1 = lobby.TeamMemberA2;
                    lobby.TeamMemberA2 = lobby.TeamMemberA3;
                    lobby.TeamMemberA3 = lobby.TeamMemberA4;
                    lobby.TeamMemberA4 = lobby.TeamMemberA5;
                    lobby.TeamMemberA5 = "";
                    lobby.ReadyStatusA1 = lobby.ReadyStatusA2;
                    lobby.ReadyStatusA2 = lobby.ReadyStatusA3;
                    lobby.ReadyStatusA3 = lobby.ReadyStatusA4;
                    lobby.ReadyStatusA4 = lobby.ReadyStatusA5;
                    lobby.ReadyStatusA5 = false;
                    break;
                case "A2":
                    lobby.TeamMemberA2 = lobby.TeamMemberA3;
                    lobby.TeamMemberA3 = lobby.TeamMemberA4;
                    lobby.TeamMemberA4 = lobby.TeamMemberA5;
                    lobby.TeamMemberA5 = "";
                    lobby.ReadyStatusA2 = lobby.ReadyStatusA3;
                    lobby.ReadyStatusA3 = lobby.ReadyStatusA4;
                    lobby.ReadyStatusA4 = lobby.ReadyStatusA5;
                    lobby.ReadyStatusA5 = false;
                    break;
                case "A3":
                    lobby.TeamMemberA3 = lobby.TeamMemberA4;
                    lobby.TeamMemberA4 = lobby.TeamMemberA5;
                    lobby.TeamMemberA5 = "";
                    lobby.ReadyStatusA3 = lobby.ReadyStatusA4;
                    lobby.ReadyStatusA4 = lobby.ReadyStatusA5;
                    lobby.ReadyStatusA5 = false;
                    break;
                case "A4":
                    lobby.TeamMemberA4 = lobby.TeamMemberA5;
                    lobby.TeamMemberA5 = "";
                    lobby.ReadyStatusA4 = lobby.ReadyStatusA5;
                    lobby.ReadyStatusA5 = false;
                    break;
                case "A5":
                    lobby.TeamMemberA5 = "";
                    lobby.ReadyStatusA5 = false;
                    break;
                case "B1":
                    lobby.TeamMemberB1 = lobby.TeamMemberB2;
                    lobby.TeamMemberB2 = lobby.TeamMemberB3;
                    lobby.TeamMemberB3 = lobby.TeamMemberB4;
                    lobby.TeamMemberB4 = lobby.TeamMemberB5;
                    lobby.TeamMemberB5 = "";
                    lobby.ReadyStatusB1 = lobby.ReadyStatusB2;
                    lobby.ReadyStatusB2 = lobby.ReadyStatusB3;
                    lobby.ReadyStatusB3 = lobby.ReadyStatusB4;
                    lobby.ReadyStatusB4 = lobby.ReadyStatusB5;
                    lobby.ReadyStatusB5 = false;
                    break;
                case "B2":
                    lobby.TeamMemberB2 = lobby.TeamMemberB3;
                    lobby.TeamMemberB3 = lobby.TeamMemberB4;
                    lobby.TeamMemberB4 = lobby.TeamMemberB5;
                    lobby.TeamMemberB5 = "";
                    lobby.ReadyStatusB2 = lobby.ReadyStatusB3;
                    lobby.ReadyStatusB3 = lobby.ReadyStatusB4;
                    lobby.ReadyStatusB4 = lobby.ReadyStatusB5;
                    lobby.ReadyStatusB5 = false;
                    break;
                case "B3":
                    lobby.TeamMemberB3 = lobby.TeamMemberB4;
                    lobby.TeamMemberB4 = lobby.TeamMemberB5;
                    lobby.TeamMemberB5 = "";
                    lobby.ReadyStatusB3 = lobby.ReadyStatusB4;
                    lobby.ReadyStatusB4 = lobby.ReadyStatusB5;
                    lobby.ReadyStatusB5 = false;
                    break;
                case "B4":
                    lobby.TeamMemberB4 = lobby.TeamMemberB5;
                    lobby.TeamMemberB5 = "";
                    lobby.ReadyStatusB4 = lobby.ReadyStatusB5;
                    lobby.ReadyStatusB5 = false;
                    break;
                case "B5":
                    lobby.TeamMemberB5 = "";
                    lobby.ReadyStatusB5 = false;
                    break;
                default:
                    return false;
            }

            _context.Update<LobbyRoomModel>(lobby);
            bool result = _context.SaveChanges() != 0;


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
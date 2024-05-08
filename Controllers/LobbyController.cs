using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shortalk___Back_End.Models.DTO;
using Shortalk___Back_End.Services;

namespace Shortalk___Back_End.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LobbyController : ControllerBase
    {
        private readonly LobbyService _data;
        public LobbyController(LobbyService data)
        {
            _data = data;
        }

        [HttpPost]
        [Route("AddLobby")]
        public bool AddLobby(CreateLobbyRoomDTO lobbyToAdd){
            return _data.AddLobbyRoom(lobbyToAdd);
        }

        [HttpGet]
        [Route("JoinLobby/{lobbyName}")]
        public bool JoinLobby(string lobbyName){
            return _data.DoesLobbyExist(lobbyName);
        }
    }
}
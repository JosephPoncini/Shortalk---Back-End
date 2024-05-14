using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shortalk___Back_End.Models;
using Shortalk___Back_End.Services;

namespace Shortalk___Back_End.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly GameService _data;

        public GameController(GameService data)
        {
            _data = data;
        }

        [HttpPost]
        [Route("AddGame")]
        public bool AddGame(LobbyRoomModel lobby){
            return _data.AddGame(lobby);
        }

        [HttpGet]
        [Route("GetGameInfo")]
        public GameModel GetGameInfo(string lobbyName){
            return _data.GetGameInfo(lobbyName);
        }
    }
}
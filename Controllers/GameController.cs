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
        [Route("GetGameInfo/{lobbyName}")]
        public GameModel GetGameInfo(string lobbyName){
            return _data.GetGameInfo(lobbyName);
        }

        [HttpGet]
        [Route("GetCard")]
        public CardModel GetCard(){
            return _data.GetCard();
        }

        [HttpGet]
        [Route("AreStringsOffByOneChar/{str1}/{str2}")]
        public bool AreStringsOffByOneChar(string str1, string str2)
        {
            return _data.AreStringsOffByOneChar(str1,str2);
        }

        [HttpPut]
        [Route("AppendBuzzWords/{lobbyName}/{buzzWordTop}/{buzzWordBottom}")]
        public bool AppendBuzzWords(string lobbyName, string buzzWordTop, string buzzWordBottom)
        {
            return _data.AppendBuzzWords(lobbyName, buzzWordTop, buzzWordBottom);
        }

        [HttpPut]
        [Route("AppendOnePointWords/{lobbyName}/{onePointWordTop}/{onePointWordBottom}")]
        public bool AppendOnePointWords(string lobbyName, string onePointWordTop, string onePointWordBottom)
        {
            return _data.AppendOnePointWords(lobbyName, onePointWordTop, onePointWordBottom);
        }

        [HttpPut]
        [Route("AppendThreePointWords/{lobbyName}/{threePointWordTop}/{threePointWordBottom}")]
        public bool AppendThreePointWords(string lobbyName, string threePointWordTop, string threePointWordBottom)
        {
            return _data.AppendThreePointWords(lobbyName, threePointWordTop, threePointWordBottom);
        }

        [HttpPut]
        [Route("AppendSkipPointWords/{lobbyName}/{skipWordTop}/{skipWordBottom}")]
        public bool AppendSkipPointWords(string lobbyName, string skipWordTop, string skipWordBottom)
        {
            return _data.AppendSkipPointWords(lobbyName, skipWordTop, skipWordBottom);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shortalk___Back_End.Models.DTO
{
    public class CreateLobbyRoomDTO
    {
        public int ID { get; set; }
        public string LobbyName { get; set; }
        public string Host {get; set;}
    }
}
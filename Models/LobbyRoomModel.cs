using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shortalk___Back_End.Models
{
    public class LobbyRoomModel
    {
        public int ID { get; set; }
        public string? LobbyName { get; set; }
        public string? TeamMemberA1 { get; set; }
        public string? TeamMemberA2 { get; set; }
        public string? TeamMemberA3 { get; set; }
        public string? TeamMemberA4 { get; set; }
        public string? TeamMemberA5 { get; set; }
        public string? TeamMemberB1 { get; set; }
        public string? TeamMemberB2 { get; set; }
        public string? TeamMemberB3 { get; set; }
        public string? TeamMemberB4 { get; set; }
        public string? TeamMemberB5 { get; set; }
        public LobbyRoomModel()
        {
            
        }
    }
}
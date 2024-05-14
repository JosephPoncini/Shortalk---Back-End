using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shortalk___Back_End.Models
{
    public class GameModel
    {
        public int ID { get; set; }
        public string LobbyName { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public int NumberOfRounds { get; set; }
        public int TimeLimit { get; set; }
        public string TeamMemberA1 { get; set; } = string.Empty;
        public string TeamMemberA2 { get; set; } = string.Empty;
        public string TeamMemberA3 { get; set; } = string.Empty;
        public string TeamMemberA4 { get; set; } = string.Empty;
        public string TeamMemberA5 { get; set; } = string.Empty;
        public string TeamMemberB1 { get; set; } = string.Empty;
        public string TeamMemberB2 { get; set; } = string.Empty;
        public string TeamMemberB3 { get; set; } = string.Empty;
        public string TeamMemberB4 { get; set; } = string.Empty;
        public string TeamMemberB5 { get; set; } = string.Empty;
        public int Turn { get; set; }
        public string Speaker { get; set; } = string.Empty;
        public string OnePointWord { get; set; } = string.Empty;
        public string ThreePointWord { get; set; } = string.Empty;
        public int Team1Score { get; set; }
        public int Team2Score { get; set; }

    }
}
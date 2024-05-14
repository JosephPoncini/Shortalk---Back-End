using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shortalk___Back_End.Models
{
    public class CardChoiceModel
    {
        public string TopWord { get; set; } = string.Empty;
        public List<string> BottomWords { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReposatoryPatternWithUOW.Core.Models
{
    public class Game
    {
        public string Id { get; set; } = null!;
        public int Player1 { get; set; }
        public int Player2 { get; set; }
        //public int? Winner { get; set; }
        public virtual ICollection<User> Users { get; set; } = null!;
        //public virtual ICollection<Message> Messages { get; set; } = null!;
        //public virtual ICollection<ChatMessage> ChatMessage { get; set; } = null!;


    }
}

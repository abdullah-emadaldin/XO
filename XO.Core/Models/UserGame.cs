using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReposatoryPatternWithUOW.Core.Models
{
    public class UserGame
    {
        public int UserId { get; set; }
        public string GameId { get; set; }
      //  public virtual ICollection<Message> Messages { get; set; } = null!;
    }
}

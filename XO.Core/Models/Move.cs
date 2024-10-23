using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XO.Core.Models
{
    public class Move
    {
        public int Id { get; set; }
        public string GameId { get; set; } = null!;
        public int? PlayerId { get; set; }
        public int? Moveplay { get; set; }
    }
}

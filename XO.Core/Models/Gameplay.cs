using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XO.Core.Models
{
    public class GamePlay
    {
        public string Id { get; set; } = null!;
        public int? PlayerId { get; set; }
        public int? Move { get; set; }

    }
}

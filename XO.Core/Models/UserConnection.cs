﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReposatoryPatternWithUOW.Core.Models
{
    public class UserConnection
    {
        public string ConnectionId { get; set; } = null!;
        public int UserId { get; set; }
       // public bool IsWaiting { get; set; } = true;
        public bool IsPlaying { get; set; } = false;
        public virtual User User { get; set; } = null!;
    }
}

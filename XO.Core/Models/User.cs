using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ReposatoryPatternWithUOW.Core.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string? ProfilePicture { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool EmailConfirmed { get; set; }
        public int? Points { get; set; } = 0;
        public virtual IdentityTokenVerification IdentityTokenVerification { get; set; } = null!;
       // public virtual List<RefreshToken> RefreshTokens { get; set; } = new();
        public virtual EmailVerificationCode EmailVerificationCode { get; set; }
       // public virtual ICollection<Message> SentMessages { get; set; } = null!;
      //  public virtual ICollection<Message> ReceivedMessages { get; set; } = null!;
        public virtual ICollection<Game>? Game { get; set; }
        public virtual ICollection<UserConnection>? UserConnections { get; set; }
        //public virtual ICollection<FriendRequest> SentRequests { get; set; } = null!;
       // public virtual ICollection<FriendRequest> RecievedRequests { get; set; } = null!;
       // public virtual ICollection<ChatMessage> ChatMessage { get; set; } = null!;



    }
}

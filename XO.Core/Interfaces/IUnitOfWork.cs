using ReposatoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XO.Core.Interfaces;
using XO.EF.Reposatories;

namespace ReposatoryPatternWithUOW.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IUserReposatory UserReposatory { get; }
        public IUserConnectionRepository UserConnection { get; }
      
        public IBaseRepository<UserGame> UserChat { get; }
        public IGameRepository GameRepository { get; }
        public IGameplayRepository GameplayRepository { get; }


        public Task<int> SaveChangesAsync();
    }
}

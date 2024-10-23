using ReposatoryPatternWithUOW.Core.Interfaces;
using ReposatoryPatternWithUOW.Core.Models;
using ReposatoryPatternWithUOW.Core.OptionsPatternClasses;
using ReposatoryPatternWithUOW.EF.Mapper;
using ReposatoryPatternWithUOW.EF.Reposatories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XO.Core.Interfaces;
using XO.EF.Reposatories;

namespace ReposatoryPatternWithUOW.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        AppDbContext context;
        public IUserReposatory UserReposatory { get; }

        public IUserConnectionRepository UserConnection { get; }

        public IGameRepository GameRepository { get; }
        public IBaseRepository<UserGame> UserChat { get; }
        public IGameplayRepository GameplayRepository { get; }



        public UnitOfWork(AppDbContext context,Mapperly mapper,TokenOptionsPattern options,ISenderService senderService)
        {
            this.context = context;
            UserReposatory = new UserReposatory(context, mapper,options,senderService);
            UserConnection = new UserConnectionRepository(context);
            GameplayRepository = new GameplayRepository(context);
            UserChat = new BaseRepository<UserGame>(context);
            GameRepository = new GameRepository(context);


        }


        public Task<int> SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }
    }
}

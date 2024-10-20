using Microsoft.EntityFrameworkCore;
using ReposatoryPatternWithUOW.Core.Interfaces;
using ReposatoryPatternWithUOW.Core.Models;
using ReposatoryPatternWithUOW.Core.OptionsPatternClasses;
using ReposatoryPatternWithUOW.EF;
using ReposatoryPatternWithUOW.EF.Mapper;
using ReposatoryPatternWithUOW.EF.Reposatories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace XO.EF.Reposatories
{
    public class UserConnectionRepository : BaseRepository<UserConnection>, IUserConnectionRepository
    {
        public UserConnectionRepository(AppDbContext context) : base(context)
        {


        }
        public async Task<int> SetIsPlaying(string connectionid)
        {
            return await context.Set<UserConnection>()
                                .Where(x => x.ConnectionId == connectionid)
                                .ExecuteUpdateAsync(x => x.SetProperty(x => x.IsPlaying, true));
        }


        public async Task<int> SetIsPlayingFalse(string connectionid)
        {
            return await context.Set<UserConnection>()
                                .Where(x => x.ConnectionId == connectionid)
                                .ExecuteUpdateAsync(x => x.SetProperty(x => x.IsPlaying, false));
        }
    }
}

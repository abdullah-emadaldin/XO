using ReposatoryPatternWithUOW.Core.Models;
using ReposatoryPatternWithUOW.EF;
using ReposatoryPatternWithUOW.EF.Reposatories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XO.EF.Reposatories
{
    public class GameRepository : BaseRepository<Game>, IGameRepository
    {
        public GameRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> AddPoints(int id)
        {
            try
            {

                var player = await context.Users.FindAsync(id);
                player!.Points += 10;
                return true;
            }
            catch
            {

                return false;
            }

        }

        public async Task<bool> AddPoints(int id, int points)
        {
            try
            {

                var player = await context.Users.FindAsync(id);
                player!.Points += points;
                return true;
            }
            catch
            {

                return false;
            }

        }


    }
}

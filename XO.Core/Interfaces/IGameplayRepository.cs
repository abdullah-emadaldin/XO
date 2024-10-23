using ReposatoryPatternWithUOW.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XO.Core.Models;

namespace XO.Core.Interfaces
{
    public interface IGameplayRepository:IBaseRepository<Move>
    {
        Task<int?> IsWinner(string gameId);
    }
}

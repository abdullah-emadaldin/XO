
using ReposatoryPatternWithUOW.Core.Interfaces;
using ReposatoryPatternWithUOW.Core.Models;

namespace XO.EF.Reposatories
{
    public interface IGameRepository:IBaseRepository<Game>
    {
        Task<bool> AddPoints(int id);
        Task<bool> AddPoints(int id, int points);
    }
}
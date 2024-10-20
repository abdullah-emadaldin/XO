using Microsoft.EntityFrameworkCore;
using ReposatoryPatternWithUOW.EF;
using ReposatoryPatternWithUOW.EF.Reposatories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XO.Core.Interfaces;
using XO.Core.Models;

namespace XO.EF.Reposatories
{
    public class GameplayRepository : BaseRepository<GamePlay>, IGameplayRepository
    {
        public GameplayRepository(AppDbContext context) : base(context)
        {
          //  this.context = context;
        }
        public async Task<int> IsWinner(string gameId)
        {
            int[][] winningCombinations =
           [
                new[] { 1, 2, 3 }, // Top row
                new[] { 4, 5, 6 }, // Middle row
                new[] { 7, 8, 9 }, // Bottom row
                new[] { 1, 4, 7 }, // Left column
                new[] { 2, 5, 8 }, // Middle column
                new[] { 3, 6, 9 }, // Right column
                new[] { 1, 5, 9 }, // Diagonal from top-left to bottom-right
                new[] { 3, 5, 7 }, // Diagonal from top-right to bottom-left
           ];

            // Retrieve all moves for the given game ID
            var moves = await context.Set<GamePlay>()
                                      .Where(g => g.Id == gameId)
                                      .ToListAsync();

            // Check for each player (Player 1 and Player 2)
            foreach (var playerId in moves.Select(m => m.PlayerId).Distinct())
            {
                // Get the positions (moves) of the current player
                var playerMoves = moves.Where(m => m.PlayerId == playerId)
                                       .Select(m => m.Move)
                                       .ToList();

                // Check if any winning combination is fully covered by the player's moves
                foreach (var combination in winningCombinations)
                {
                    if (combination.All(c => playerMoves.Contains(c)))
                    {
                        // Return the playerId if they have a winning combination
                        return playerId ?? 0;
                    }
                }
            }

            // Return 0 if no winner is found
            return 0;

        
        }
    }
}

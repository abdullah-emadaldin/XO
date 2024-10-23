using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using ReposatoryPatternWithUOW.Core.Interfaces;
using ReposatoryPatternWithUOW.Core.Models;
using ReposatoryPatternWithUOW.EF.Reposatories;
using System.Text.RegularExpressions;
using XO.Core.Models;

namespace XO.Hubs
{
    [Authorize]
    public class GameHub(IUnitOfWork unitOfWork):Hub
    {

        public override async Task OnConnectedAsync()
        {
            try
            {
                int id = int.Parse(TokenHandler.ExtractJwtFromQuery(Context.GetHttpContext()!.Request)!.Payload["id"].ToString()!);
                await Console.Out.WriteLineAsync("id is:  " + id);
                string name = TokenHandler.ExtractJwtFromQuery(Context.GetHttpContext()!.Request)!.Payload["username"].ToString()!;
                var user = await unitOfWork.UserReposatory.GetByIdAsync(id);
                user!.UserConnections!.Add(new() { ConnectionId = Context.ConnectionId });
                await Console.Out.WriteLineAsync("user: " + name + " has joined");
                await unitOfWork.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                await Console.Out.WriteLineAsync($"id: {Context.ConnectionId} joined without account");
                await unitOfWork.UserConnection.AddAsync(new() { ConnectionId=Context.ConnectionId});
                await unitOfWork.SaveChangesAsync();
            }
            
        }



        //public async Task SearchForPlayers()
        //{
        //    Console.ForegroundColor = ConsoleColor.Green;
        //    await Console.Out.WriteLineAsync("i");
        //    // Extract the id safely from the token (nullable int)
        //    string? idPayload = TokenHandler.ExtractJwtFromQuery(Context.GetHttpContext()!.Request)?.Payload["id"]?.ToString();
        //    int? id = null;

        //    if (!string.IsNullOrEmpty(idPayload) && int.TryParse(idPayload, out int parsedId))
        //    {
        //        id = parsedId;
        //    }
        //    Console.ForegroundColor = ConsoleColor.Yellow;
        //    await Console.Out.WriteLineAsync($"id is {id}");
        //    // Fetch online players excluding the current connection
        //    var onlinePlayers = await unitOfWork.UserConnection.GetWhere(x => x.ConnectionId != Context.ConnectionId && x.IsPlaying == false);
        //    if (onlinePlayers.Any())
        //    {
        //        // Create a random instance
        //        Random random = new Random();
        //        await Console.Out.WriteLineAsync("iii");
        //        // Get a random player by selecting a random index
        //        var randomIndex = random.Next(onlinePlayers.Count());
        //        var randomPlayerId = onlinePlayers.ToList()[randomIndex];
        //        await Console.Out.WriteLineAsync("iiii");
        //        // Log the selected player
        //        Console.ForegroundColor = ConsoleColor.Green;
        //        Console.WriteLine($"Random player ID selected: {randomPlayerId} --------------------");

        //        // Create a unique game ID
        //        var gameId = Guid.NewGuid().ToString();

        //        // Create the game, handling both scenarios (with or without Player1 ID)
        //        Game game = new Game
        //        {
        //            Id = gameId,
        //            Player1 = id,  // If no id, it will be null
        //            Player2 = randomPlayerId.UserId
        //        };

        //        await unitOfWork.UserConnection.SetIsPlaying(Context.ConnectionId);
        //        await unitOfWork.UserConnection.SetIsPlaying(randomPlayerId.ConnectionId);
        //        Console.WriteLine($"Game created with ID: {gameId}, Player1: {game.Player1}, Player2: {game.Player2}");

        //        // Save the game to the database
        //        await unitOfWork.SaveChangesAsync();

        //        // Notify other clients in the group about the game
        //        await Clients.OthersInGroup(game.Id).SendAsync("SearchForPlayers", gameId, game.Player1, game.Player2);
        //    }
        //    else
        //    {
        //        // Handle the case when no other players are online
        //        Console.ForegroundColor = ConsoleColor.Green;
        //        var badMessage = "No other players are online :::::)))).";
        //        Console.WriteLine(badMessage);
        //        await Clients.Caller.SendAsync("SearchForPlayers", badMessage);
        //    }
        //}

        public async Task SearchForPlayers()
        {
            int id = int.Parse(TokenHandler.ExtractJwtFromQuery(Context.GetHttpContext()!.Request)!.Payload["id"].ToString()!);
            var waitinglist = (await unitOfWork.UserConnection.GetWhere(x => x.IsPlaying == false && x.ConnectionId !=Context.ConnectionId)).FirstOrDefault();
            if (waitinglist != null)
            {

                var gameId = Guid.NewGuid().ToString();
                Game game = new Game() { Id = gameId, Player1 = id, Player2 = waitinglist.UserId };
                //Move gameplay = new Move()
                //{
                //    GameId = gameId
                //};
                Console.WriteLine(gameId, game.Player1, game.Player2);

               
                await Task.WhenAll(unitOfWork.GameRepository.AddAsync(game),
                    //unitOfWork.GameplayRepository.AddAsync(gameplay),
                    //unitOfWork.UserConnection.SetIsWaitingFalse(waitinglist.ConnectionId),
                    //unitOfWork.UserConnection.SetIsWaitingFalse(Context.ConnectionId),
                    unitOfWork.UserConnection.SetIsPlaying(Context.ConnectionId),
                    unitOfWork.UserConnection.SetIsPlaying(waitinglist.ConnectionId));
                //await unitOfWork.UserConnection.SetIsPlaying(game.Player2);
                await unitOfWork.SaveChangesAsync();

                await Task.WhenAll(Groups.AddToGroupAsync(Context.ConnectionId, game.Id), Groups.AddToGroupAsync(waitinglist.ConnectionId, game.Id));
                Console.ForegroundColor = ConsoleColor.Cyan;
                await Console.Out.WriteLineAsync(gameId + "   " + game.Player1 + "    " + game.Player2);
                await Clients.Group(game.Id).SendAsync("SearchForPlayers", gameId, game.Player1, game.Player2);
            }
          
        }



        //public async Task SendM()
        //{
        //    string m = "flaaaaaah w naqaaaash";
              
        //    //Console.WriteLine("message: " + message);
        //    await Clients.All.SendAsync("SendM", m);
        //}


        public async Task Gameplay(string GameId , int Move)
        {
            int id = int.Parse(TokenHandler.ExtractJwtFromQuery(Context.GetHttpContext()!.Request)!.Payload["id"].ToString()!);
            Move gameplay = new Move()
            {

                GameId = GameId,
                PlayerId = id,
                Moveplay = Move

            };
            await unitOfWork.GameplayRepository.AddAsync(gameplay);
            await unitOfWork.SaveChangesAsync();
            var winnerId = await unitOfWork.GameplayRepository.IsWinner(GameId);
            await Clients.Group(GameId).SendAsync("Gameplay",id,Move, winnerId);
        }


        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await unitOfWork.UserConnection.SetIsPlayingFalse(Context.ConnectionId);
            await Task.WhenAll(unitOfWork.UserConnection.ExecuteDeleteAsync(x => x.ConnectionId == Context.ConnectionId),
                 base.OnDisconnectedAsync(exception));
        }
        }
    }

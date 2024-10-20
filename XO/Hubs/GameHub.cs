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
            var user = await unitOfWork.UserConnection.GetWhere(x=>x.UserId==id)!;
            //if (user.FirstOrDefault()!.IsPlaying)
            //{
            //    var game = await unitOfWork.GameRepository.GetWhere(x => x.Player2 == id);
            //    await Clients.OthersInGroup(game.Select(x => x.Id).FirstOrDefault()!).SendAsync("SearchForPlayers", game.Select(x => x.Id).FirstOrDefault(), game.Select(x => x.Player1).FirstOrDefault(), game.Select(x => x.Id).FirstOrDefault());

            //}



            while (true)
            {
                var onlineplayers = await unitOfWork.UserConnection.GetWhere(x => x.ConnectionId != Context.ConnectionId && x.IsPlaying == false);
                await Task.Delay(1000);
                if (onlineplayers.Any())
                {

                    // Create a random instance
                    Random random = new Random();

                    // Get a random player by selecting a random index
                    var randomIndex = random.Next(onlineplayers.Count());

                    // Get the UserId of the random player
                    var randomPlayerId = onlineplayers.ToList()[randomIndex].UserId;
                    Console.ForegroundColor = ConsoleColor.Green;
                    // Proceed with the matchmaking or the next steps
                    Console.WriteLine($"Random player ID selected: {randomPlayerId} --------------------");
                    var gameId = Guid.NewGuid().ToString();
                    Game game = new Game() { Id = gameId, Player1 = id, Player2 = randomPlayerId };
                    GamePlay gameplay = new GamePlay()
                    {
                        Id = gameId
                    };
                    Console.WriteLine(gameId, game.Player1, game.Player2);

                    await unitOfWork.GameRepository.AddAsync(game);
                    await unitOfWork.GameplayRepository.AddAsync(gameplay);
                    //await unitOfWork.UserConnection.SetIsPlaying(game.Player2);
                    await Groups.AddToGroupAsync(Context.ConnectionId, game.Id);
                    await unitOfWork.SaveChangesAsync();
                    var p2connid = await unitOfWork.UserConnection.GetByIdAsync( game.Player2);
                    var p2id = p2connid!.ConnectionId;
                    await unitOfWork.UserConnection.SetIsPlaying(Context.ConnectionId);
                    await unitOfWork.UserConnection.SetIsPlaying(p2id);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    await Console.Out.WriteLineAsync(gameId + "   " + game.Player1 + "    " + game.Player2);
                    await Groups.AddToGroupAsync(p2id, game.Id);
                    await Clients.OthersInGroup(game.Id).SendAsync("SearchForPlayers", gameId, game.Player1, game.Player2);
                    //await Clients.OthersInGroup(game.Id).SendAsync("SearchForPlayers");

                    break;
                }
                Console.ForegroundColor = ConsoleColor.Magenta;
                await Console.Out.WriteLineAsync("still looking for player");
            }
            //else
            //{
            //    Console.ForegroundColor = ConsoleColor.Green;
            //    // Handle the case when no online players are available
            //    var BadMessage = "No other players are online :::::)))).";
            //    Console.WriteLine(BadMessage);
            //    await Clients.Caller.SendAsync("SearchForPlayers", BadMessage);
            //}
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
            var game = await unitOfWork.GameplayRepository.GetByIdAsync(GameId);
            game.PlayerId = id;
            game.Move = Move;
            await unitOfWork.SaveChangesAsync();
            var winnerId = unitOfWork.GameplayRepository.IsWinner(game.Id);
            await Clients.OthersInGroup(game.Id).SendAsync("Gameplay", winnerId);
        }


        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await unitOfWork.UserConnection.SetIsPlayingFalse(Context.ConnectionId);
            await Task.WhenAll(unitOfWork.UserConnection.ExecuteDeleteAsync(x => x.ConnectionId == Context.ConnectionId),
                 base.OnDisconnectedAsync(exception));
        }
        }
    }

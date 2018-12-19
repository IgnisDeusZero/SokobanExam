using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Kontur.Sokoban.Models;
using Kontur.Sokoban.Services;

namespace Kontur.Sokoban.Controllers
{
    [Route("api/games/{gameId}/moves")]
    public class MovesController : Controller
    {
        [HttpPost]
        public IActionResult Moves(Guid gameId, [FromBody]UserInputForMovesPost userInput)
        {
            if (!GamesRepo.Instance.ContainsGame(gameId))
            {
                return new BadRequestObjectResult("game id is bad");
            }
            var game = GamesRepo.Instance.GetGame(gameId);
            var dir = InputParser.GetDirection(userInput, game);
            if (dir is Game.Direction direction)
            {
                game.Move(direction);
            }
            var gameDto = new GameDto(GameDtoBuilder.BuildCells(game).ToArray(),
                true, true,
                game.Width, game.Height,
                gameId,
                game.IsGameFinished,
                game.Moves);
            return new ObjectResult(gameDto);
        }
    }
}
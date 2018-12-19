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
            var dir = InputParser.GetDirection(userInput);
            var game = GamesRepo.Instance.GetGame(gameId);
            if (dir is Game.Direction direction)
            {
                game.Move(direction);
            }
            var gameDto = new GameDto(GameDtoBuilder.BuildCells(game).ToArray(),
                true, false,
                game.Width, game.Height,
                gameId,
                game.IsGameFinished,
                game.Moves);
            return new ObjectResult(gameDto);
        }
    }
}
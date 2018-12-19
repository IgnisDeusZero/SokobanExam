using Microsoft.AspNetCore.Mvc;
using Kontur.Sokoban.Models;
using Kontur.Sokoban.Services;
using System;
using System.Linq;

namespace Kontur.Sokoban.Controllers
{
    [Route("api/games")]
    public class GamesController : Controller
    {
        [HttpPost]
        public IActionResult Index([FromBody] LevelName level)
        {
            if (!LevelRepo.Instance.ContainsLevel(level.Name))
            {
                return new BadRequestObjectResult("there is no such level");
            }
            var gameId = Guid.NewGuid();
            var game = GamesRepo.Instance.NewGame(gameId, level.Name);
            GameDto dto = new GameDto(
                GameDtoBuilder.BuildCells(game).ToArray(),
                true, false,
                game.Width, game.Height, 
                gameId, 
                false, 
                0);
            return new ObjectResult(dto);
        }
    }
}

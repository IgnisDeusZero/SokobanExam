using Kontur.Sokoban.Game;
using Kontur.Sokoban.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kontur.Sokoban.Services
{
    public class GameDtoBuilder
    {
        public static IEnumerable<CellDto> BuildCells(SokobanField field)
        {
            foreach (var block in field.GetBlocksToPos)
            {
                yield return new CellDto(block.Key.Id.ToString(),
                    block.Value,
                    block.Key.Type,
                    "",
                    block.Key.ZIndex);
            }
        } 
    }
}

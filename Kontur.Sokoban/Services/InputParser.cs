using Kontur.Sokoban.Game;
using Kontur.Sokoban.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kontur.Sokoban.Services
{
    public static class InputParser
    {
        public static Direction? GetDirection(UserInputForMovesPost userInput, SokobanField game)
        {
            if (!IsCorrectInput(userInput))
                return null;
            if (userInput.ClickedPos == null)
            {
                switch ((int)userInput.KeyPressed)
                {
                    case 37:
                        return Direction.Left;
                    case 39:
                        return Direction.Right;
                    case 38:
                        return Direction.Up;
                    case 40:
                        return Direction.Down;
                    default:
                        return null;
                }
            }
            else
            {
                return ParseClick(userInput, game);
            }
        }

        private static Direction? ParseClick(UserInputForMovesPost userInput, SokobanField game)
        {
            var playerPos = game.PlayerPos;
            var clickedPos = userInput.ClickedPos;
            // Swicth playerPos.Y and clickedPos.Y because (0,0) in upper left corner
            var playerToClickVec = new Vec(clickedPos.X - playerPos.X, playerPos.Y - clickedPos.Y);
            if (playerToClickVec.X == 0 && playerToClickVec.Y == 0)
            {
                return null;
            }
            var radian = Math.Atan2(playerToClickVec.Y, playerToClickVec.X);
            radian += radian < 0 ? 2 * Math.PI : 0;
            if (radian < Math.PI / 4)
            {
                return Direction.Right;
            }
            else if (radian < 3 * Math.PI / 4)
            {
                return Direction.Up;
            }
            else if (radian < 5 * Math.PI / 4)
            {
                return Direction.Left;
            }
            else if (radian < 7 * Math.PI / 4)
            {
                return Direction.Down;
            }
            else
            {
                return Direction.Right;
            }

        }

        private static bool IsCorrectInput(UserInputForMovesPost userInput)
        {
            var isKeyboardInput = (userInput.KeyPressed >= 37 || userInput.KeyPressed <= 40)
                    && userInput.ClickedPos == null;
            var isClickInput = userInput.ClickedPos != null;
            return isKeyboardInput || isClickInput;
        }

    }
}

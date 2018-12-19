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
        private static Direction?[,] PartsToDirection = new Direction?[,]
        {
            {null, Direction.Up, null },
            {Direction.Left, null, Direction.Right },
            {null, Direction.Down, null }
        };

        public static Direction? GetDirection(UserInputForMovesPost userInput, Vec levelSize)
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
                return ParseClick(userInput, levelSize);
            }
        }

        private static Direction? ParseClick(UserInputForMovesPost userInput, Vec levelSize)
        {
            int partByWidth = userInput.ClickedPos.X < levelSize.X / 3
                ? 0
                : userInput.ClickedPos.X < 2 * levelSize.X / 3
                    ? 1
                    : 2;

            int partByHeight = userInput.ClickedPos.Y < levelSize.Y / 3
                ? 0
                : userInput.ClickedPos.Y < 2 * levelSize.Y / 3
                    ? 1
                    : 2;
            return PartsToDirection[partByHeight, partByWidth];


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

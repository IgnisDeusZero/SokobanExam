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
        public static Direction? GetDirection(UserInputForMovesPost userInput)
        {
            if (!IsCorrectInput(userInput))
                return null;
            if (userInput.KeyPressed != default(char))
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
                }
            }
            return null;
        }

        private static bool IsCorrectInput(UserInputForMovesPost userInput)
        {
            return userInput.ClickedPos == null
                    && (userInput.KeyPressed >= 37 || userInput.KeyPressed <= 40);
        }

    }
}

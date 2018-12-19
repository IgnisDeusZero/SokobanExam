using Kontur.Sokoban.Game.Blocks;
using Kontur.Sokoban.Models;

namespace Kontur.Sokoban.Game
{
    public class PushData
    {
        public PushData(Direction direction, int power, IBlock pusher, Vec from)
        {
            Direction = direction;
            Power = power;
            Pusher = pusher;
            From = from;
        }
        public readonly Direction Direction;
        public readonly int Power;
        public readonly IBlock Pusher;
        public readonly Vec From;
    }
}

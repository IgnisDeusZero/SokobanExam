using System;

namespace Kontur.Sokoban.Game.Blocks
{
    public class Player : IBlock
    {
        private Guid _id = Guid.NewGuid();
        public Guid Id => _id;
        public bool IsSolid => true;
        public int ZIndex => 100;
        public string Type => "unicorn";
        private const bool isMovedByPush = false;
        public int Power => 1;


        public PushResult Push(PushData pushData)
        {
            return new PushResult(
                 isMovedByPush,
                 0,
                 pushData);
        }
    }
}

using System;

namespace Kontur.Sokoban.Game.Blocks
{
    public class Box : IBlock
    {
        private Guid _id = Guid.NewGuid();
        public Guid Id => _id;
        public bool IsSolid => true;
        public int ZIndex => 50;
        public string Type => "package";

        private const bool isMovedByPush = true;
        private int Weight = 1;

        public PushResult Push(PushData pushData)
        {
            return new PushResult(
                pushData.Power > 0,
                Weight,
                pushData);
        }
    }
}

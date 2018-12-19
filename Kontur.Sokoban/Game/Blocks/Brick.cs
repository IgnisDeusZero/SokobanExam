using System;
namespace Kontur.Sokoban.Game.Blocks
{
    public class Brick : IBlock
    {
        private Guid _id = Guid.NewGuid();
        public Guid Id => _id;
        public bool IsSolid => true;
        public int ZIndex => 52;
        public string Type => "bricks";

        public PushResult Push(PushData pushData)
        {
            return new PushResult(
                false,
                0,
                pushData);
        }
    }
}

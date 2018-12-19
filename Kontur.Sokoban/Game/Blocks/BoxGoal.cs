using System;
namespace Kontur.Sokoban.Game.Blocks
{
    public class BoxGoal : IBlock
    {
        private Guid _id = Guid.NewGuid();
        public Guid Id => _id;
        public bool IsSolid => false;
        public int ZIndex => 51;
        public string Type => "goal";

        public PushResult Push(PushData pushData)
        {
            return new PushResult(
                pushData.Power >= 0,
                0,
                pushData);
        }
    }
}

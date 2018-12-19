using System;

namespace Kontur.Sokoban.Game.Blocks
{
    public interface IBlock
    {
        Guid Id { get; }
        bool IsSolid { get; }
        int ZIndex { get; }
        string Type { get; }
        PushResult Push(PushData pushData);
    }
}

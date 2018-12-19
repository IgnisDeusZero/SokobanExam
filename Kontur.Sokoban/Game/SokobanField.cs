using System;
using System.Linq;
using System.Collections.Generic;
using Kontur.Sokoban.Game.Blocks;
using Kontur.Sokoban.Models;

namespace Kontur.Sokoban.Game
{
    public class SokobanField
    {
        public bool IsGameFinished => IsGameFinishedChecks();
        private Dictionary<IBlock, Vec> BlocksToPos;
        public IEnumerable<KeyValuePair<IBlock, Vec>> GetBlocksToPos => BlocksToPos;
        private IBlock Player => BlocksToPos.Where(b => b.Key is Player).First().Key;
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Moves { get; private set; }

        public SokobanField(IEnumerable<KeyValuePair<IBlock, Vec>> blocksToPos)
        {
            if (blocksToPos.Count(b => b.Key is Player) != 1)
            {
                throw new ArgumentException("Must be 1 player", nameof(blocksToPos));
            }
            var solidBlocks = blocksToPos.Where(b => b.Key.IsSolid);
            if (solidBlocks.Count() > solidBlocks.Select(x => x.Value).Distinct().Count())
            {
                throw new ArgumentException("Must be 1 solid block per position", nameof(blocksToPos));
            }
            if (blocksToPos.Any(x => x.Value.X < 0 || x.Value.Y < 0))
            {
                throw new ArgumentException("All positions must be positive", nameof(blocksToPos));
            }
            Width = blocksToPos.Max(x => x.Value.X) + 1;
            Height = blocksToPos.Max(x => x.Value.Y) + 1;
            BlocksToPos = new Dictionary<IBlock, Vec>(blocksToPos);
        }

        public void Move(Direction dir)
        {
            lock (Player)
            {
                Vec moveTo = GetMoveToPos(BlocksToPos[Player], dir);
                if (Move(GetBlocksFrom(moveTo), new PushData(dir, ((Player)Player).Power, Player, moveTo)))
                {
                    BlocksToPos[Player] = moveTo;
                    Moves++;
                }
            }
        }

        private bool Move(IEnumerable<KeyValuePair<IBlock, Vec>> blocksToPos, PushData pushData)
        {
            var results = blocksToPos.Select(b => (PushResult: b.Key.Push(pushData), BlockToPos: b));
            var moveTo = GetMoveToPos(pushData.From, pushData.Direction);
            var powerLeft = pushData.Power - results.Sum(r => r.PushResult.PowerSpent);
            var moved = false;
            if (powerLeft < 0)
            {
                return moved;
            }
            if (results.Any(r => r.BlockToPos.Key.IsSolid && r.PushResult.IsMovedByPush))
            {
                var nextBlocks = GetBlocksFrom(moveTo);
                if (nextBlocks.Any())
                {
                    moved = Move(nextBlocks,
                        new PushData(
                            pushData.Direction,
                            powerLeft,
                            results.Where(b => b.BlockToPos.Key.IsSolid).First().BlockToPos.Key,
                            moveTo));
                }
                else
                {
                    moved = true;
                }
            }
            else if (results.Any(r => r.BlockToPos.Key.IsSolid && !r.PushResult.IsMovedByPush))
            {
                return false;
            }
            else
            {
                return true;
            }
            if (moved)
            {
                var movedBlock = results.First(r => r.BlockToPos.Key.IsSolid).BlockToPos.Key;
                BlocksToPos[movedBlock] = moveTo;
            }
            return moved;
        }

        IEnumerable<KeyValuePair<IBlock, Vec>> GetBlocksFrom(Vec pos)
        {
            return BlocksToPos.Where(block => block.Value.Equals(pos));
        }

        private Vec GetMoveToPos(Vec from, Direction dir)
        {
            switch (dir)
            {
                case Direction.Up:
                    return new Vec(from.X, from.Y - 1);
                case Direction.Down:
                    return new Vec(from.X, from.Y + 1);
                case Direction.Left:
                    return new Vec(from.X - 1, from.Y);
                case Direction.Right:
                    return new Vec(from.X + 1, from.Y);
            }
            return null;
        }

        private bool IsGameFinishedChecks()
        {
            var boxesPos = BlocksToPos.Where(b => b.Key is Box).Select(x => x.Value);
            var goalsPos = BlocksToPos.Where(b => b.Key is BoxGoal).Select(x => x.Value);
            return !boxesPos.Except(goalsPos).Any() && !goalsPos.Except(boxesPos).Any();
        }
    }
}

namespace Kontur.Sokoban.Game
{
    public class PushResult
    {
        public PushResult(
            bool isMovedByPush, 
            int powerSpent,
            PushData pushData)
        {
            IsMovedByPush = isMovedByPush;
            PowerSpent = powerSpent;
            PushData = pushData;
        }
        
        public readonly bool IsMovedByPush;
        public readonly int PowerSpent;
        public readonly PushData PushData;
    }
}

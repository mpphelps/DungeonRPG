namespace DungeonRPG.Commands
{
    public class MoveSouth : ICommand
    {
        public bool Execute(Board board, Party party, bool roundOver)
        {
            if (party.Position.Row + 1 > (int)board.Size)
                return false;
            party.Position.Row++;
            return true;
        }
    }
}



namespace DungeonRPG.Commands
{
    public class MoveNorth : ICommand
    {
        public bool Execute(Board board, Party party, bool roundOver)
        {
            if (party.Position.Row - 1 < 0)
                return false;
            party.Position.Row--;
            return true;
        }
    }
}



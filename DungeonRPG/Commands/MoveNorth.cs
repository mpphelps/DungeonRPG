namespace DungeonRPG.Commands
{
    public class MoveNorth : ICommand
    {
        public bool Execute(Board board, Party party, bool roundOver)
        {
            if (party.Position.Row - 1 < 0)
            {
                ((ICommand)this).CantMove("North");
                return false;
            }
            party.Position.Row--;
            return true;
        }
    }
}



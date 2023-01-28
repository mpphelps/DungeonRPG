namespace DungeonRPG.Commands
{
    public class MoveEast : ICommand
    {
        public bool Execute(Board board, Party party, ref bool roundOver)
        {
            if (party.Position.Col + 1 > (int)board.Size)
            {
                ((ICommand)this).CantMove("East");
                return false;
            }
                
            party.Position.Col++;
            return true;
        }
    }
}



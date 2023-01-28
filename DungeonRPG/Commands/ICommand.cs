namespace DungeonRPG.Commands
{
    public interface ICommand
    {
        // Return true if Execute succeeded
        bool Execute(Board board, Party party, ref bool roundOver);
        public void CantMove(string direction)
        {
            Console.WriteLine($"You can't move {direction}, there is a wall blocking the way");
        }
    }

}



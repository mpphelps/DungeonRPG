namespace DungeonRPG.Commands
{
    public class Descend : ICommand
    {
        public bool Execute(Board board, Party party, ref bool roundOver)
        {
            Console.WriteLine("You descended further into the cavern, the rickety ladder has crumbled leaving no way out!");
            roundOver = true;
            return true;
        }
    }
}



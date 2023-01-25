namespace DungeonRPG.Commands
{
    public class Leave : ICommand
    {
        public bool Execute(Board board, Party party, bool roundOver)
        {
            Console.WriteLine("You left the cavern safely!");
            roundOver = true;
            return true;
        }
    }
}



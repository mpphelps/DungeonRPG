namespace DungeonRPG.Commands
{
    public interface ICommand
    {
        // Return true if Execute succeeded
        bool Execute(Board board, Party party, bool roundOver);
    }
}



namespace DungeonRPG.Rooms
{
    public class Pit : IRoom
    {
        public void RoomEvent(Board board, Party party)
        {
            ConsoleUtilities.SetDeadColor();
            Console.WriteLine("Your party fell to a gruesome death in a pit");
            ConsoleUtilities.ResetColor();
            foreach (var character in party.Characters)
            {
                character.IsDead = true;
            }
        }
        public void NeighborEvent()
        {
            ConsoleUtilities.SetWarningColor();
            Console.WriteLine("You feel a draft. There is a pit in a nearby room.");
            ConsoleUtilities.ResetColor();
        }
    }

}
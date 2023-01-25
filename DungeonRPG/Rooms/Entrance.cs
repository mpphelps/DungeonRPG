namespace DungeonRPG.Rooms
{
    public class Entrance : IRoom
    {
        public void RoomEvent(Board board, Party party)
        {
            Console.WriteLine("You see light coming from the cavern entrance");
        }
        public void NeighborEvent()
        {

        }
    }

}
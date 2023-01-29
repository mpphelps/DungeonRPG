namespace DungeonRPG.Rooms
{
    public class Exit : IRoom
    {
        public void RoomEvent(Board board, Party party)
        {
            Console.WriteLine("You see candles lighting a ladder down further into the cavern");
        }
        public void NeighborEvent()
        {

        }
    }

}
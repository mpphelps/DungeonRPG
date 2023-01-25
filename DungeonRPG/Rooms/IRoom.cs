namespace DungeonRPG.Rooms
{
    public interface IRoom
    {
        public void RoomEvent(Board board, Party party);
        public void NeighborEvent();
    }
}
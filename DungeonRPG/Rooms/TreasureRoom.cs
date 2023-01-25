namespace DungeonRPG.Rooms
{
    public class TreasureRoom : IRoom
    {
        public IItem Treasure { get; set; }

        public TreasureRoom(IItem treasure)
        {
            Treasure = treasure;
        }

        public void NeighborEvent()
        {
            Console.WriteLine("You hear the tinkling of sparkly treasure");
        }

        public void RoomEvent(Board board, Party party)
        {
            Console.WriteLine($"You found {Treasure.Name}!  It's been added to your inventory");
            party.Inventory.Add(Treasure);
        }
    }
}
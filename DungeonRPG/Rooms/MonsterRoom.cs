namespace DungeonRPG.Rooms
{
    public class MonsterRoom : IRoom
    {
        public Party Monsters { get; set; }

        public MonsterRoom(Party monsters)
        {
            Monsters = monsters;
        }

        public void NeighborEvent()
        {
            Console.WriteLine("You can hear monsters preparing for battle nearby");
        }

        public void RoomEvent(Board board, Party party)
        {
            var battle = new Battle(party, Monsters);
            battle.Fight();
        }
    }

}
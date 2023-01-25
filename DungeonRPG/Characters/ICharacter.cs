namespace DungeonRPG
{
    public interface ICharacter
    {
        public int Health { get; set; }
        public int MaxHealth { get; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool IsDead { get; set; }
        public void DoNothing()
        {
            Console.WriteLine($"{Name} does nothing!");
        }
        public void Attack(ICharacter character);
        public void UseItem(IItem item);
        

    }
}
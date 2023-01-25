namespace DungeonRPG
{
    public class Skeleton : ICharacter
    {
        public int Health { get; set; }
        public int MaxHealth { get; }
        public string Name { get; set; } = "Skeleton";
        public int Level { get; set; }
        public bool IsDead { get; set; } = false;
        public Skeleton(int level)
        {
            Level = level;
            MaxHealth = Level * 5;
            Health = MaxHealth;
            IsDead = false;
        }
        public void Attack(ICharacter character)
        {
            int damageDone = Level * 2;
            character.Health -= damageDone;
            Console.WriteLine($"{Name} uses bone crunch on {character.Name} for {damageDone} damage!");
            Console.WriteLine($"{character.Name} has {(character.Health < 0? 0 : character.Health)}/{character.MaxHealth} health points remaining.");
            if (character.Health <= 0)
            {
                character.IsDead = true;
                Console.WriteLine($"{character.Name} was knocked by {Name}");
            }
        }
        public void UseItem(IItem item)
        {
            Console.WriteLine($"{Name} used {item.Name}");
            item.Use(this);
        }
    }
}
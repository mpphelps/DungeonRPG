namespace DungeonRPG
{
    public class Player : ICharacter
    {
        public int Health { get; set; }
        public int MaxHealth { get; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool IsDead { get; set; }
        public Player(int level, string name)
        {
            Level = level;
            MaxHealth = Level * 5;
            Health = MaxHealth;
            Name = name;
            IsDead = false;
        }

        public void Attack(ICharacter character)
        {
            int damageDone = Level * 2;
            character.Health -= damageDone;
            Console.WriteLine($"{Name} punches {character.Name} for {damageDone} damage!");
            Console.WriteLine($"{character.Name} has {(character.Health < 0 ? 0 : character.Health)} health points remaining.");
            if (character.Health <= 0)
            {
                character.IsDead = true;
                Console.WriteLine($"{character.Name} was knocked out by {Name}");
            }
        }

        public void UseItem(IItem item)
        {
            Console.WriteLine($"{Name} used {item.Name}");
            item.Use(this);
        }

    }
}
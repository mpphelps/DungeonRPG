namespace DungeonRPG
{
    public class Player : ICharacter
    {
        public Dictionary<IItem, int> Buffs { get; set; } = new();
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int PhysicalDefense { get; set; }
        public int MagicDefense { get; set; }
        public int MagicAttack { get; set; }
        public int PhsycialAttack { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool IsDead { get; set; }

        public Player(int level, string name)
        {
            Level = level;
            MaxHealth = Level * 15;
            Health = MaxHealth;
            Name = name;
            IsDead = false;
        }

        public void Attack(ICharacter character)
        {
            int damageDone = Level * 2;
            Console.WriteLine($"{Name} punches {character.Name} for {damageDone} damage!");
            character.PhysicalDamage(damageDone, this);
            
        }

    }
}
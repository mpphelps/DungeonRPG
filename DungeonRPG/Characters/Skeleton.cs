namespace DungeonRPG
{
    public class Skeleton : ICharacter
    {
        public Dictionary<IItem, int> Buffs { get; set; } = new();
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int PhysicalDefense { get; set; }
        public int MagicDefense { get; set; }
        public int MagicAttack { get; set; }
        public int PhsycialAttack { get; set; }
        public string Name { get; set; } = "Skeleton";
        public int Level { get; set; }
        public bool IsDead { get; set; }
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
            Console.WriteLine($"{Name} uses bone crunch on {character.Name} for {damageDone} damage!");
            character.PhysicalDamage(damageDone, this);
        }
    }
}
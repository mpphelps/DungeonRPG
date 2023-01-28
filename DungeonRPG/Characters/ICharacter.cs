namespace DungeonRPG
{
    public interface ICharacter
    {
        public Dictionary<IItem, int> Buffs { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int PhysicalDefense { get; set; }
        public int MagicDefense { get; set; }
        public int MagicAttack { get; set; }
        public int PhsycialAttack { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool IsDead { get; set; }
        public void DoNothing()
        {
            Console.WriteLine($"{Name} does nothing!");
        }
        public void Attack(ICharacter character);
        public void UseItem(IItem item)
        {
            Console.WriteLine($"{Name} used {item.Name}");
            item.Use(this);
        }
        public void DecrementBuffTimer()
        {
            foreach (var buff in Buffs)
            {
                if (buff.Value > 0)
                {
                    Buffs.Remove(buff.Key);
                    Buffs.Add(buff.Key, buff.Value - 1);
                }
                else
                {
                    var item = buff.Key;
                    item.RemoveBuff(this);
                    Buffs.Remove(buff.Key);
                }
            }
        }
        public void LevelUp(int level)
        {
            Level += level;
            Console.WriteLine($"{Name} leveled up to {Level}!");
            MaxHealth += Level;
            Console.WriteLine($"Max HP went up to {MaxHealth}!");
            Heal(Level);
        }
        public void Heal(int amount)
        {
            Health += amount;
            if (Health > MaxHealth)
            {
                Health = MaxHealth;
                Console.WriteLine($"{Name}'s health points maxed out!");
            }
            else
            {
                Console.WriteLine($"{Name}'s health increased by {amount} points");
            }
        }
        public void PhysicalDamage(int amount, ICharacter attacker)
        {
            int adjustedAmount = amount - PhysicalDefense;
            Damage(adjustedAmount, attacker);
        }
        public void MagicalDamage(int amount, ICharacter attacker)
        {
            int adjustedAmount = amount - MagicDefense;
            Damage(adjustedAmount, attacker);
        }
        private void Damage(int amount, ICharacter attacker)
        {
            Health -= (amount - PhysicalDefense);
            Console.WriteLine($"{Name} has {(Health < 0 ? 0 : Health)}/{MaxHealth} health points remaining.");
            if (Health <= 0)
            {
                IsDead = true;
            }
            if (IsDead)
            {
                Console.WriteLine($"{Name} was knocked out by {attacker.Name}");
            }
        }

        
    }
}
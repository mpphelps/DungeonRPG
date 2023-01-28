using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonRPG.Characters
{
    public class Sorcerer : ICharacter
    {
        public Dictionary<IItem, int> Buffs { get; set; } = new();
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int PhysicalDefense { get; set; }
        public int MagicDefense { get; set; }
        public int MagicAttack { get; set; }
        public int PhsycialAttack { get; set; }
        public string Name { get; set; } = "The Evil Sorcerer";
        public int Level { get; set; }
        public bool IsDead { get; set; }

        public Sorcerer(int level)
        {
            Level = level;
            MaxHealth = Level * 5;
            Health = MaxHealth;
            IsDead = false;
        }
        public void Attack(ICharacter character)
        {
            var random = new Random();
            int damageDone = random.Next(0, 3);
            Console.WriteLine($"{Name} unravels a spell on {character.Name} for {damageDone} damage!");
            character.MagicalDamage(damageDone, this);
        }
    }
}

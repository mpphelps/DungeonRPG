using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonRPG.Characters
{
    public class UncodedOne : ICharacter
    {
        public int Health { get; set; }
        public int MaxHealth { get; }
        public string Name { get; set; } = "The Uncoded One";
        public int Level { get; set; }
        public bool IsDead { get; set; }
        public UncodedOne(int level)
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
            character.Health -= damageDone;
            Console.WriteLine($"{Name} unravels code on {character.Name} for {damageDone} damage!");
            Console.WriteLine($"{character.Name} has {(character.Health < 0 ? 0 : character.Health)}/{character.MaxHealth} health points remaining.");
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

namespace DungeonRPG
{
    public class HealthPotion : IItem
    {
        public string Name { get; } = "Health Potion";
        public int Points { get; } = 20;

        public void RemoveBuff(ICharacter character)
        {
            return;
        }

        public void Use(ICharacter character)
        {
            character.Health += Points;
            if (character.Health > character.MaxHealth)
            {
                character.Health = character.MaxHealth;
                Console.WriteLine($"{character.Name}'s health points maxed out!");
            }
            else
            {
                Console.WriteLine($"{character.Name}'s health increased by {Points} points");
            }
        }
    }

}
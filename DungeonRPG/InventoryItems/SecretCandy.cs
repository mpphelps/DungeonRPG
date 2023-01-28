namespace DungeonRPG
{
    public class SecretCandy : IItem
    {
        // When used, instantly level up the player in the party
        public string Name { get; } = "Secret Candy";
        public int Points { get; } = 1;

        public void RemoveBuff(ICharacter character)
        {
            return;
        }

        public void Use(ICharacter character)
        {
            character.LevelUp(Points);
            Console.WriteLine($"{character.Level}'s health increased by {Points} points");
        }
    }

}
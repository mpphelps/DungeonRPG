namespace DungeonRPG
{
    public class DefenseSpray : IItem
    {
        // Improves the characters defense from attacks
        public string Name { get; } = "Defense Spray";
        public int Points { get; } = 3;
        public int Rounds = 3;

        public void Use(ICharacter character)
        {
            character.PhysicalDefense += Points;
            character.Buffs.Add(this, Rounds);
        }

        public void RemoveBuff(ICharacter character)
        {
            character.PhysicalDefense -= Points;
        }
    }

}
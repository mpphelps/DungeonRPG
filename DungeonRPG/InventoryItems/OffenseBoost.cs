namespace DungeonRPG
{
    public class OffenseBoost : IItem
    {
        // Improves the characters defense from attacks
        public string Name { get; } = "Offense Boost";
        public int Points { get; } = 3;
        public int Rounds = 3;

        public void Use(ICharacter character)
        {
            character.PhsycialAttack += Points;
            character.Buffs.Add(this, Rounds);
        }

        public void RemoveBuff(ICharacter character)
        {
            character.PhsycialAttack -= Points;
        }
    }

}
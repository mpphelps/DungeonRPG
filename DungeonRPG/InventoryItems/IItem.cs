namespace DungeonRPG
{
    public interface IItem
    {
        public string Name { get; }
        public void Use(ICharacter character);
        public void RemoveBuff(ICharacter character);
    }
}
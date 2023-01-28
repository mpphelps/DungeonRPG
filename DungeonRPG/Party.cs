using DungeonRPG.Enums;
using System.Collections;

namespace DungeonRPG
{
    public class Party : IEnumerable<ICharacter>
    {
        public List<ICharacter> Characters = new List<ICharacter>();
        public List<IItem> Inventory { get; set; } = new List<IItem>();
        public bool PlayerIsComputer { get; set; }
        public int Size { get { return Characters.Count; } }
        public ICharacter this[int index] => Characters[index];
        public Position Position { get; set; } = new Position();

        public Party()
        {
            PlayerIsComputer = false;
        }
        public Party(bool isPartyComputer) : base()
        {
            PlayerIsComputer = isPartyComputer;
        }

        public void AddPlayer(int level)
        {
            ConsoleUtilities.ResetColor();
            Console.WriteLine("Enter player name: ");
            bool valid = false;
            string? name;
            while (!valid)
            {
                name = Console.ReadLine();
                if (!string.IsNullOrEmpty(name))
                {
                    valid = true;
                    Characters.Add(new Player(level, name));
                }
                else
                {
                    ConsoleUtilities.SetWarningColor();
                    Console.WriteLine("Invalid name provided, try again: ");
                    ConsoleUtilities.ResetColor();
                }
            }
        }

        public void Turn(Party EnemyParty)
        {
            if (PlayerIsComputer) ComputerTurn(EnemyParty);
            else PlayerTurn(EnemyParty);

        }

        public void PlayerTurn(Party EnemyParty)
        {
            foreach (var character in Characters)
            {
                bool inTurn = true;
                while (inTurn)
                {
                    Console.WriteLine($"It's {character.Name}'s turn. (Level: {character.Level} Health: {character.Health})");
                    var action = GetPlayerAction();
                    if (action == Actions.DoNothing)
                    {
                        character.DoNothing();
                    }
                    else if (action == Actions.Attack)
                    {
                        var enemy = SelectEnemy(EnemyParty);
                        if (enemy == null) continue;
                        character.Attack(enemy);
                        if(enemy.IsDead) EnemyParty.Remove(enemy);
                    }
                    else if (action == Actions.UseItem)
                    {
                        var item = SelectItem();
                        if (item == null) continue;
                        character.UseItem(item);
                    }
                    inTurn = false;
                    Console.WriteLine();
                    Thread.Sleep(500);
                }
            }
        }

        public void ComputerTurn(Party EnemyParty)
        {
            foreach (var character in Characters)
            {
                if (EnemyParty.Size == 0) continue;
                Console.WriteLine($"It's {character.Name}'s turn.");
                if (character.Health < character.MaxHealth && Inventory.Count > 0)
                {
                    var rand = new Random();
                    if (rand.Next(0, 2) == 1) character.UseItem(Inventory[0]);
                    else character.Attack(EnemyParty[0]);
                }
                else 
                {
                    character.Attack(EnemyParty[0]);
                }
                
                if (EnemyParty[0].IsDead) EnemyParty.Remove(EnemyParty[0]);
                Console.WriteLine();
                Thread.Sleep(500);
            }
        }

        private Actions GetPlayerAction()
        {
            int selection = 0;
            Console.WriteLine($"Select an action to take");
            Console.WriteLine($"1 - Do Nothing");
            Console.WriteLine($"2 - Attack");
            Console.WriteLine($"3 - Use Item");
            var actionList = Enum.GetValues(typeof(Actions)).Cast<Actions>().ToList();
            bool valid = false;
            while (!valid)
            {
                var input = Console.ReadKey(true).KeyChar.ToString();
                if (int.TryParse(input, out selection) && selection >= 1 && selection <= actionList.Count)
                    valid = true;
                else
                    Console.WriteLine("Not a valid selection");
            }

            return (Actions)(selection-1);
        }

        private ICharacter? SelectEnemy(Party EnemyParty)
        {
            Console.WriteLine("Select enemy to attack");
            for (int i = 0; i < EnemyParty.Size; i++)
            {
                Console.WriteLine($"{i+1} - {EnemyParty[i].Name} (Level: {EnemyParty[i].Level} Health: {EnemyParty[i].Health})");
            }
            Console.WriteLine($"{EnemyParty.Size + 1} - Return");
            while (true)
            {
                var input = Console.ReadKey(true).KeyChar.ToString();
                if (int.TryParse(input, out int selection) && selection >= 0 && selection <= EnemyParty.Size + 1)
                {
                    if (selection == EnemyParty.Size + 1) return null;
                    return EnemyParty[selection - 1];
                }
                else
                    Console.WriteLine("Selection not valid");
            }

        }

        public IItem? SelectItem()
        {
            if (Inventory.Count == 0)
            {
                Console.WriteLine("Inventory is empty.");
                return null;
            }
            Console.WriteLine("Select item to use");
            for (int i = 0; i < Inventory.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {Inventory[i].Name}");
            }
            while (true)
            {
                var input = Console.ReadKey(true).KeyChar.ToString();
                if (int.TryParse(input, out int selection) && selection >= 0 && selection <= Inventory.Count + 1)
                {
                    if (selection == Inventory.Count + 1 ) return null;
                    return Inventory[selection - 1];
                }
                else
                    Console.WriteLine("Selection not valid");
            }
        }

        public void Add(ICharacter character)
        {
            Characters.Add(character);
        }

        public void Remove(ICharacter character)
        {
            Characters.Remove(character);
        }

        public IEnumerator<ICharacter> GetEnumerator()
        {
            return Characters.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public bool AreAllDead()
        {
            foreach (var character in Characters)
            {
                if (!character.IsDead) return false;
            }
            return true;
        }

        public void DecrementPartyBuffs()
        {
            foreach (var character in Characters)
            {
                character.DecrementBuffTimer();
            }
        }
    }
}



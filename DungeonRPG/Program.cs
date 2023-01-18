using DungeonRPG.Characters;
using System.Collections;
using System.ComponentModel;
using System.Threading;
using System.Xml.Linq;

namespace DungeonRPG
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var game = new Game();
            game.Run();
        }
    }

    public class Game
    {
        private Party _heroes;
        private Party _monsterParty1;
        private Party _monsterParty2;
        private Party _monsterParty3;

        public enum GameMode
        {
            PVE, PVP, EVE
        }
        public Game()
        {
            bool isHeroComputer = false;
            bool isMonsterComputer = false;
            var mode = SelectGameMode();
            if (mode == GameMode.PVE)
                isMonsterComputer = true;
            if (mode == GameMode.EVE)
            {
                isHeroComputer = true;
                isMonsterComputer = true;
            }

            _heroes = new Party(playerIsComputer: isHeroComputer);
            _heroes.Inventory.Add(new HealthPotion());
            AddPlayer(level: 5);
            _monsterParty1 = new Party(playerIsComputer: isMonsterComputer);
            _monsterParty1.Add(new Skeleton(level: 1));

            _monsterParty2 = new Party(playerIsComputer: isMonsterComputer);
            _monsterParty2.Add(new Skeleton(level: 1));
            _monsterParty2.Add(new Skeleton(level: 1));

            _monsterParty3 = new Party(playerIsComputer: isMonsterComputer);
            _monsterParty3.Add(new UncodedOne(level: 3));
        }

        private GameMode SelectGameMode()
        {
            Console.WriteLine("Select Mode: ");
            Console.WriteLine("1 - Player vs Computer: ");
            Console.WriteLine("2 - Player vs Player: ");
            Console.WriteLine("3 - Computer vs Computer: ");
            while (true)
            {
                var input = Console.ReadKey(true).KeyChar.ToString();
                if (int.TryParse(input, out int selection) && selection >= 1 && selection <= 3)
                {
                    return (GameMode)(selection-1);
                }
                else
                {
                    Console.WriteLine("Invalid selection");
                }
            }
        }

        private void AddPlayer(int level)
        {
            Console.WriteLine("Enter player name: ");
            bool valid = false;
            string? name;
            while (!valid)
            {
                name = Console.ReadLine();
                if (!string.IsNullOrEmpty(name))
                {
                    valid = true;
                    _heroes.Add(new Player(level, name));
                }
                else
                {
                    Console.WriteLine("Invalid name provided, try again: ");
                }
            }
        }

        public void Run()
        {
            bool didHeroesLose;
            Round(_heroes, _monsterParty1, out didHeroesLose);
            if (didHeroesLose) return;
            Round(_heroes, _monsterParty2, out didHeroesLose);
            if (didHeroesLose) return;
            Round(_heroes, _monsterParty3, out _);
        }
        public void Round(Party heroes, Party monsters, out bool gameOver)
        {
            while (true)
            {
                PrintRoundStatus(heroes, monsters);
                heroes.Turn(monsters);
                if (monsters.Size == 0)
                {
                    Console.WriteLine("The monsters have all been vanquished!");
                    Console.WriteLine("*********** YOU WON ***********");
                    gameOver = false;
                    return;
                }
                monsters.Turn(heroes);
                if (heroes.Size == 0)
                {
                    Console.WriteLine("The monsters have slain the hero!");
                    Console.WriteLine("*********** GAME OVER ***********");
                    gameOver = true;
                    return;
                }
            }
        }

        private void PrintRoundStatus(Party heroes, Party monsters)
        {
            Console.WriteLine("======================== BATTLE ========================");
            foreach (var hero in heroes)
            {
                Console.WriteLine($"{hero.Name} ({hero.Health}/{hero.MaxHealth})");
            }
            Console.WriteLine("--------------------------------------------------------");
            foreach (var monster in monsters)
            {
                Console.WriteLine($"{monster.Name} ({monster.Health}/{monster.MaxHealth})");
            }
            
            Console.WriteLine("========================================================");
        }
    }

    public enum Actions
    {
        DoNothing, Attack, UseItem
    }

    public class Party : IEnumerable<ICharacter>
    {
        public List<ICharacter> Characters = new List<ICharacter>();
        public List<IItem> Inventory { get; set; }
        public bool PlayerIsComputer { get; set; }
        public int Size { get { return Characters.Count; } }
        public ICharacter this[int index] => Characters[index];

        public Party(bool playerIsComputer)
        {
            PlayerIsComputer = playerIsComputer;
            Inventory = new List<IItem>();
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
    }
}
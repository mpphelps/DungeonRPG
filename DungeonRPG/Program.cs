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
        private Party _monsters;

        public Game()
        {
            _heroes = new Party();
            _monsters = new Party();
            AddPlayer();
            _monsters.Add(new Skeleton(5));
        }

        private void AddPlayer()
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
                    _heroes.Add(new Player(15, name));
                }
                else
                {
                    Console.WriteLine("Invalid name provided, try again: ");
                }
            }
        }

        public void Run()
        {
            while (true)
            {
                _heroes.Turn();
                _monsters.Turn();
            }
        }
    }

    public interface ICharacter
    {
        public int Health { get; set; }
        public string Name { get; set; }
        public void TakeAction(Action<int> action, int value) { }

        void TakeAction(Action<string, int> doNothing, int v);
    }

    public class Player : ICharacter
    {
        public int Health { get; set; }
        public string Name { get; set; }

        public Player(int health, string name)
        {
            Health = health;
            Name = name;
        }

        public void TakeAction(Action<string, int> action, int value)
        {
            action(Name, value);
        }
    }

    public class Skeleton : ICharacter
    {
        public int Health { get; set; }
        public string Name { get; set; } = "Skeleton";

        public Skeleton(int health)
        {
            Health = health;
        }

        public void TakeAction(Action<string, int> action, int value)
        {
            action(Name, value);
        }
    }

    public static class Actions
    {
        public static void DoNothing(string Name, int value)
        {
            Console.WriteLine($"{Name} does nothing.");
        }

        
    }

    public class GameObject
    {

    }

    public class Party
    {
        List<ICharacter> Characters = new List<ICharacter>();

        public void Turn()
        {
            foreach (var character in Characters)
            {
                Console.WriteLine($"It's {character.Name}'s turn.");
                character.TakeAction(Actions.DoNothing,0);
                Console.WriteLine();
                Thread.Sleep(500);
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
    }
}
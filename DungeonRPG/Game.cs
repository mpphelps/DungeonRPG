using DungeonRPG.Commands;
using DungeonRPG.Enums;
using DungeonRPG.Rooms;
using System.Text;

namespace DungeonRPG
{
    public class Game
    {
        private Party _heroes;
        private Board _board;
        private string _bigBadGuy = "Mehdivh";
        private int _roundDifficulty;
        private bool _gameOver = false;
        public Game(BoardSize boardSize)
        {
            var _mode = SelectGameMode();
            _heroes = new Party();
            _heroes.AddPlayer(level: 1);
            _board = new Board(boardSize);
        }

        private GameMode SelectGameMode()
        {
            ConsoleUtilities.ResetColor();
            Console.WriteLine("Select Mode: ");
            Console.WriteLine("1 - Player vs Computer: ");
            Console.WriteLine("2 - Player vs Player: ");
            while (true)
            {
                var input = Console.ReadKey(true).KeyChar.ToString();
                if (int.TryParse(input, out int selection) && selection >= 1 && selection <= 2)
                {
                    return (GameMode)(selection - 1);
                }
                else
                {
                    ConsoleUtilities.SetWarningColor();
                    Console.WriteLine("Invalid selection");
                    ConsoleUtilities.ResetColor();
                }
            }
        }

        public void Run()
        {
            Introduction();
            _roundDifficulty = 0;
            while (_roundDifficulty <= 5 || !_gameOver)
            {
                BoardGenerator.GenerateTiles(_board, _roundDifficulty);
                RunRound();
                EndOfRound();
                _roundDifficulty++;
                // TODO: need something here to end the round when leaving the cave
            }
        }

        public void RunRound()
        {
            bool roundOver = false;
            while (!roundOver)
            {
                Console.WriteLine(_board.Status(_heroes.Position));
                ConsoleUtilities.SetGoodColor();
                Console.WriteLine($"Your party is in the room at (Row={_heroes.Position.Row}, Column={_heroes.Position.Col})");
                ConsoleUtilities.ResetColor();
                _board.GetRoom(_heroes.Position).RoomEvent(_board, _heroes);
                var neighbors = _board.GetNeighbors(_heroes.Position);
                foreach (var neighbor in neighbors) neighbor.NeighborEvent();

                if (_heroes.AreAllDead())
                {
                    _gameOver = true;
                    break;
                }

                ICommand command = GetCommand();
                Console.Clear();
                command.Execute(_board, _heroes, ref roundOver);
                // TODO: need something here to end the round when leaving the cave
            }
        }

        public void EndOfRound()
        {
            // TODO: Sumarize party experience gained, do level ups
            Console.WriteLine("Heroes gained 100xp!! Yayayayya");
            Console.WriteLine("Everyone in your party leveled up once, fuck you and your xp");
            foreach (var character in _heroes)
            {
                character.LevelUp(1);
            }
        }

        private void Introduction()
        {
            Console.WriteLine($"You enter the Cavern of the Evil Sorcerer {_bigBadGuy}.\n" +  
                $"The cavern is a maze of rooms filled with dangerous pits, maelstroms, and amaroks\n" +
                $"in search of secret treasures.\n" +
                $"Light is visible only in the entrance, and no other light is seen anywhere in the caverns.\n" +
                $"You must navigate the Caverns with other senses.\n" + 
                $"Find treasures and evacuate before the {_bigBadGuy} finds and kills you, \n" +
                $"or if you strong enough, try and eliminate the wizard to win the game!\n\n");
        }

        private ICommand GetCommand()
        {
            while (true)
            {
                Console.WriteLine("What do you want to do?");
                Console.WriteLine("1 - Move North");
                Console.WriteLine("2 - Move South");
                Console.WriteLine("3 - Move East");
                Console.WriteLine("4 - Move West");
                if (HeroesAreAtEntrance())
                    Console.WriteLine("5 - Leave");
                while (true)
                {
                    var input = Console.ReadKey(true).KeyChar.ToString();
                    if (input == "1")      return new MoveNorth();
                    else if (input == "2") return new MoveSouth();
                    else if (input == "3") return new MoveEast();
                    else if (input == "4") return new MoveWest();
                    else if (input == "5" && HeroesAreAtEntrance()) return new Leave();
                    else
                    {
                        Console.WriteLine("Invalid selection");
                    }
                }
                
            }
        }

        private bool HeroesAreAtEntrance()
        {
            return _board[_heroes.Position.Row, _heroes.Position.Col] is Entrance;
        }

        
    }
}



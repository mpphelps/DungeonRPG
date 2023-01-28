namespace DungeonRPG
{
    public class Battle
    {
        public Party _heroes { get; init; }
        public Party _monsters { get; init; }

        public Battle(Party heros, Party monsters)
        {
            _heroes = heros;
            _monsters = monsters;
        }

        public bool Fight()
        {
            bool didHeroDie;
            while (true)
            {
                PrintRoundStatus(_heroes, _monsters);
                _heroes.Turn(_monsters);
                if (_monsters.Size == 0)
                {
                    Console.WriteLine("The monsters have all been vanquished!");
                    Console.WriteLine("*********** YOU WON ***********");
                    didHeroDie = false;
                    break;
                }
                _monsters.Turn(_heroes);
                if (_heroes.Size == 0)
                {
                    Console.WriteLine("The monsters have slain the hero!");
                    Console.WriteLine("*********** GAME OVER ***********");
                    didHeroDie = true;
                    break;
                }
                _heroes.DecrementPartyBuffs();
                _monsters.DecrementPartyBuffs();
            }
            return didHeroDie;
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
}



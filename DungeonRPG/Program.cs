using DungeonRPG.Characters;
using DungeonRPG.Enums;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml.Linq;

namespace DungeonRPG
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var game = new Game(BoardSize.Large);
            game.Run();
        }
    }
}



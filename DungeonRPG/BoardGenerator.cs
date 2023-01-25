using DungeonRPG.Rooms;
using System.Drawing;

namespace DungeonRPG
{
    public static class BoardGenerator
    {
        public static void GenerateTiles(Board board, int difficultyLevel)
        {
            InitializeRooms(board);
            board[0, 0] = new Entrance();

            // TODO: change board based on difficultylevel
            var monsters = new Party(isPartyComputer: true);
            monsters.Add(new Skeleton(level: 1));
            monsters.Add(new Skeleton(level: 1));
            board[0, 2] = new MonsterRoom(monsters);
            board[2, 2] = new Pit();
            board[2, 0] = new TreasureRoom(new HealthPotion());
            
        }

        private static void InitializeRooms(Board board)
        {
            for (int i = 0; i < (int)board.Size; i++)
            {
                for (int j = 0; j < (int)board.Size; j++)
                {
                    board[i, j] = new NormalRoom();
                }
            }
        }
    }
}

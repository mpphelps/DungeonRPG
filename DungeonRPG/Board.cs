using DungeonRPG.Enums;
using DungeonRPG.Rooms;
using System.Text;

namespace DungeonRPG
{
    public class Board
    {
        private IRoom[,] _rooms;
        public BoardSize Size { get; private set; }

        public Board(BoardSize size)
        {
            _rooms = new IRoom[(int)size, (int)size];
            Size = size;
        }

        public IRoom GetRoom(Position position)
        {
            return _rooms[position.Row, position.Col];
        }

        public IRoom this[int row, int col]
        {
            get 
            {
                return _rooms[row, col];
            } 
            set
            {
                _rooms[row, col] = value;
            }
        }

        public List<IRoom> GetNeighbors(Position position)
        {
            List<IRoom> neighbors = new List<IRoom>();
            if (position.Row + 1 < (int)Size) neighbors.Add(_rooms[position.Row + 1, position.Col]);
            if (position.Row - 1 >= 0) neighbors.Add(_rooms[position.Row - 1, position.Col]);
            if (position.Col + 1 < (int)Size) neighbors.Add(_rooms[position.Row, position.Col + 1]);
            if (position.Col - 1 >= 0) neighbors.Add(_rooms[position.Row, position.Col - 1]);
            if (position.Row + 1 < (int)Size && position.Col + 1 < (int)Size)
                neighbors.Add(_rooms[position.Row + 1, position.Col + 1]);
            if (position.Row + 1 < (int)Size && position.Col - 1 >= 0)
                neighbors.Add(_rooms[position.Row + 1, position.Col - 1]);
            if (position.Row - 1 >= 0 && position.Col + 1 < (int)Size)
                neighbors.Add(_rooms[position.Row - 1, position.Col + 1]);
            if (position.Row - 1 >= 0 && position.Col - 1 >= 0)
                neighbors.Add(_rooms[position.Row - 1, position.Col - 1]);
            return neighbors;
        }

        public string Status(Position playerPosition)
        {
            string divider = "";
            if (Size == BoardSize.Small) divider = "-----------------";
            if (Size == BoardSize.Medium) divider = "-------------------------";
            if (Size == BoardSize.Large) divider = "---------------------------------";
            var board = new StringBuilder();
            board.AppendLine(divider);
            for (int i = 0; i < (int)Size; i++)
            {
                board.Append("|");
                for (int j = 0; j < (int)Size; j++)
                {
                    var currentRoom = _rooms[i, j];
                    if (playerPosition.Row == i && playerPosition.Col == j) board.Append($" X |");
                    else if (currentRoom is Entrance) board.Append($" ^ |");
                    else if (currentRoom is NormalRoom) board.Append($"   |");
                    //else if (currentRoom is Fountain) board.Append($" & |");
                    else if (currentRoom is Pit) board.Append($" * |");
                    else if (currentRoom is TreasureRoom) board.Append($" $ |");
                    else if (currentRoom is MonsterRoom) board.Append($" # |");
                    //else if (currentRoom is Maelstrom) board.Append($" @ |");
                    //else if (currentRoom is Amarok) board.Append($" A |");
                }
                board.AppendLine();
                board.AppendLine(divider);
            }

            return board.ToString();
        }
    }
}



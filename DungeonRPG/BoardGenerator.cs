using DungeonRPG.Characters;
using DungeonRPG.Enums;
using DungeonRPG.Rooms;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace DungeonRPG
{
    public static class BoardGenerator
    {
        private static Random _random = new Random();

        public static Board GenerateBoard(Board oldBoard, int difficultyLevel)
        {
            Board newBoard = new Board(oldBoard.Size);
            InitializeRooms(newBoard);
            PlaceEntrance(oldBoard, newBoard, difficultyLevel); ;
            RandomlyPlaceMonsterParties(newBoard, difficultyLevel);
            RandomlyPlacePits(newBoard, difficultyLevel);
            RandomlyPlaceTreasure(newBoard, difficultyLevel);
            // TODO: this magic number 5 is the max level of caverns.  Need to move magic number to an object property.
            if (difficultyLevel == 5)
                RandomlyPlaceSorcerer(newBoard, difficultyLevel);
            else
                RandomlyPlaceExit(newBoard);
            return newBoard;
        }

        private static void RandomlyPlaceExit(Board board)
        {
            bool exitPlaced = false;
            while (!exitPlaced)
            {
                var randRow = _random.Next(0, (int)board.Size);
                var randCol = _random.Next(0, (int)board.Size);
                if (board[randRow, randCol] is NormalRoom)
                {
                    board[randRow, randCol] = new Exit();
                    exitPlaced = true;
                }
            }
        }

        private static void RandomlyPlaceSorcerer(Board board, int difficultyLevel)
        {
            bool sorcererPlaced = false;
            while (!sorcererPlaced)
            {
                var randRow = _random.Next(0, (int)board.Size);
                var randCol = _random.Next(0, (int)board.Size);
                if (board[randRow, randCol] is NormalRoom)
                {
                    var SorcererParty = new Party();
                    SorcererParty.Add(new Skeleton(level: difficultyLevel));
                    SorcererParty.Add(new Skeleton(level: difficultyLevel));
                    SorcererParty.Add(new Sorcerer(level: 10));
                    board[randRow, randCol] = new MonsterRoom(SorcererParty);
                    sorcererPlaced = true;
                }
            }
        }

        private static void RandomlyPlaceTreasure(Board board, int difficultyLevel)
        {
            int quantity = 0;
            if (board.Size == BoardSize.Small) quantity = 1 + (5 - difficultyLevel);
            else if (board.Size == BoardSize.Medium) quantity = 2 + (5 - difficultyLevel);
            else if (board.Size == BoardSize.Large) quantity = 3 + (5 - difficultyLevel);

            for (int i = 0; i < quantity; i++)
            {
                bool treasurePlaced = false;
                while (!treasurePlaced)
                {
                    var randRow = _random.Next(0, (int)board.Size);
                    var randCol = _random.Next(0, (int)board.Size);
                    var treasure = GetRandomTreasure();
                    if (board[randRow, randCol] is NormalRoom)
                    {
                        board[randRow, randCol] = new TreasureRoom(treasure);
                        treasurePlaced = true;
                    }
                }
            }
        }

        private static IItem GetRandomTreasure()
        {
            Items item = (Items)_random.Next(0, 3);
            return item switch
            {
                Items.DefenseSpray => new DefenseSpray(),
                Items.HealthPotion => new HealthPotion(),
                Items.OffenseBoost => new OffenseBoost(),
                Items.SecretCandy => new SecretCandy(),
                _ => new HealthPotion()
            };
        }

        private static void RandomlyPlacePits(Board board, int difficultyLevel)
        {
            int quantity = 0;
            if (board.Size == BoardSize.Small) quantity = 1 + difficultyLevel / 2;
            else if (board.Size == BoardSize.Medium) quantity = 2 + difficultyLevel / 2;
            else if (board.Size == BoardSize.Large) quantity = 3 + difficultyLevel / 2;

            for (int i = 0; i < quantity; i++)
            {
                bool pitPlaced = false;
                while (!pitPlaced)
                {
                    var randRow = _random.Next(0, (int)board.Size);
                    var randCol = _random.Next(0, (int)board.Size);
                    if (board[randRow, randCol] is NormalRoom)
                    {
                        board[randRow, randCol] = new Pit();
                        pitPlaced = true;
                    }
                }
            }
        }

        private static void RandomlyPlaceMonsterParties(Board board, int difficultyLevel)
        {
            List<Party> monsterParties = RandomlyBuildParty(board.Size, difficultyLevel);
            foreach (Party party in monsterParties)
            {
                bool partyPlaced = false;
                while (!partyPlaced)
                {
                    var randRow = _random.Next(0, (int)board.Size);
                    var randCol = _random.Next(0, (int)board.Size);
                    if (board[randRow, randCol] is NormalRoom)
                    {
                        board[randRow, randCol] = new MonsterRoom(party);
                        partyPlaced = true;
                    }
                }
            }
        }

        private static List<Party> RandomlyBuildParty(BoardSize size, int difficultyLevel)
        {
            List<Party> monsterParties = new();
            int quantity = 0;
            if (size == BoardSize.Small) quantity = 1;
            else if (size == BoardSize.Medium) quantity = 2;
            else if (size == BoardSize.Large) quantity = 3;

            for (int i = 0; i < quantity; i++)
            {
                var monsterParty = new Party();
                var randomPartySize = _random.Next(0, difficultyLevel);
                for (int j = 0; j < randomPartySize; j++)
                {
                    monsterParty.Add(new Skeleton(level: difficultyLevel));
                }
                monsterParties.Add(monsterParty);
            }
            return monsterParties;
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

        static void PlaceEntrance(Board oldBoard, Board newBoard, int difficultyLevel)
        {
            if (difficultyLevel == 0)
            {
                newBoard[0, 0] = new Entrance();
            }
            
            else
            {
                newBoard[oldBoard.ExitPosition.Row, oldBoard.ExitPosition.Col] = new Entrance();
            }

        }
    }
}

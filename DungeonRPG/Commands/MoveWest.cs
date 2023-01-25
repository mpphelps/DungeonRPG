﻿namespace DungeonRPG.Commands
{
    public class MoveWest : ICommand
    {
        public bool Execute(Board board, Party party, bool roundOver)
        {
            if (party.Position.Col - 1 < 0)
                return false;
            party.Position.Col--;
            return true;
        }
    }
}



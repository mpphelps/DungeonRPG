namespace DungeonRPG
{
    public static class ConsoleUtilities
    {
        public static void SetGoodColor()
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static void SetWarningColor()
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static void SetDeadColor()
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static void SetMaelstromColor()
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.White;
        }

        public static void ResetColor()
        {
            Console.ResetColor();
        }
    }
}
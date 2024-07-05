namespace MoeBot.Tools {
    internal static class Logger {
        private static void Log(string message, ConsoleColor colour) {
            Console.ForegroundColor = colour;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        internal static void Log(string message) {
            DateTime dateTime = DateTime.Now;
            Log($"{dateTime.ToString("HH:mm:ss")} Log         {message}", ConsoleColor.White);
        }

        internal static void DiscordCommand(string message) {
            DateTime dateTime = DateTime.Now;
            Log($"{dateTime.ToString("HH:mm:ss")} Command     {message}", ConsoleColor.Blue);
        }

        internal static void DiscordMessage(string message) {
            DateTime dateTime = DateTime.Now;
            Log($"{dateTime.ToString("HH:mm:ss")} Message     {message}", ConsoleColor.Green);    
        }

        internal static void Error(string message) {
            DateTime dateTime = DateTime.Now;
            Log($"{dateTime.ToString("HH:mm:ss")} Error       {message}", ConsoleColor.Red);
        }

        internal static void Warning(string message) {
            DateTime dateTime = DateTime.Now;
            Log($"{dateTime.ToString("HH:mm:ss")} Warning     {message}", ConsoleColor.Yellow);
        }
    }
}
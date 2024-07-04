namespace MoeBot.Tools {
    internal static class Logger {
        static private void Log(string message, ConsoleColor colour) {
            Console.ForegroundColor = colour;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        static internal void Log(string message) {
            DateTime dateTime = DateTime.Now;
            Log($"{dateTime.ToString("HH:mm:ss")} Log         {message}", ConsoleColor.White);
        }

        static internal void Error(string message) {
            DateTime dateTime = DateTime.Now;
            Log($"{dateTime.ToString("HH:mm:ss")} Error       {message}", ConsoleColor.Red);
        }

        static internal void Warning(string message) {
            DateTime dateTime = DateTime.Now;
            Log($"{dateTime.ToString("HH:mm:ss")} Warning     {message}", ConsoleColor.Yellow);
        }
    }
}
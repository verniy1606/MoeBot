// See https://aka.ms/new-console-template for more information

namespace MoeBot {
    public class Program {
        public static async Task Main() {
            var _client = new MoeClient();
            await _client.StartAsync();

            await Task.Delay(-1);
        }
    }
}
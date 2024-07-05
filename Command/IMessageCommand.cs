using Discord.WebSocket;

namespace MoeBot.Command {
    internal interface IMessageCommand {
        string GetCommandName();
        bool WantExecute(string messageContent);
        Task Execute(SocketMessage socketMessage);
    }
}
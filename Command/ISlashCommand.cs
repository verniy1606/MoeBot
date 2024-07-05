using Discord;
using Discord.WebSocket;

namespace MoeBot.Command {
    internal interface ISlashCommand {
        /// <summary>
        /// Get the name of the command
        /// </summary>
        /// <returns>The Name</returns>
        string GetCommandName();

        /// <summary>
        /// Get the instance of the command
        /// </summary>
        /// <returns>The instance</returns>
        SlashCommandBuilder GetInstance();

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="slashCommand"></param>
        /// <returns></returns>
        Task Execute(SocketSlashCommand slashCommand);
    }
}
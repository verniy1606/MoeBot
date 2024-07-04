using Discord;
using Discord.WebSocket;

namespace MoeBot.Command.SlashCommands {
    internal class Talk : ISlashCommand {
        private readonly SlashCommandBuilder _slashCommandBuilder;
        private static readonly string _commandName = "talk";

        public Talk() {
            _slashCommandBuilder = new SlashCommandBuilder()
            .WithName(_commandName)
            .WithDescription("moeee yamabikoo")
            .AddOption("message", ApplicationCommandOptionType.String, "The message you want the bot to say", isRequired: true);
        }

        public string GetCommandName() {
            return _commandName;
        }

        public SlashCommandBuilder GetInstance() {
            return _slashCommandBuilder;
        }

        public async Task Execute(SocketSlashCommand slashCommand) {
            await slashCommand.Channel.SendMessageAsync(slashCommand.Data.Options.First().Value.ToString());
        }
    }
}
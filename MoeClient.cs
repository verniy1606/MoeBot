using Discord;
using Discord.WebSocket;
using MoeBot.Command;
using MoeBot.Command.SlashCommands;
using MoeBot.Ignore;
using MoeBot.Tools;

namespace MoeBot {
    internal class MoeClient : DiscordSocketClient {
        internal readonly List<ISlashCommand> SlashCommands;

        public MoeClient() {
            SlashCommands = new List<ISlashCommand>() {
                new Talk()
            };

            this.Log += ClientLog;
            this.Ready += ClientReady;
            this.SlashCommandExecuted += ClientSlashCommandExecuted;

            this.LoginAsync(TokenType.Bot, BotInfo.Token);
        }

        private async Task ClientSlashCommandExecuted(SocketSlashCommand socketSlashCommand) {
            if (socketSlashCommand.User.IsBot) return;

            string slashCommandName = socketSlashCommand.Data.Name;

            foreach (ISlashCommand slashCommand in SlashCommands) {
                if (slashCommand.GetInstance().Name != slashCommandName) return;

                Logger.Log($"{slashCommandName} caused !");
                await slashCommand.Execute(socketSlashCommand);

                break;
            }
        }

        public new Task StartAsync() {
            Logger.Log("Starting the bot.....");
            return base.StartAsync();
        }

        private async Task ClientReady() {

            var guild = this.GetGuild(BotInfo.GuildId);

            if (guild == null) {
                Logger.Warning("The guild is null!");
                return;
            }

            try {
                foreach (ISlashCommand slashCommand in SlashCommands) {
                    await guild.CreateApplicationCommandAsync(slashCommand.GetInstance().Build());
                };

            } catch (Exception ex) {
                Logger.Error($"An exception caused: {ex}");
            }
        }

        private Task ClientLog(LogMessage logMessage) {
            Console.WriteLine(logMessage.ToString());
            return Task.CompletedTask;
        }
    }
}
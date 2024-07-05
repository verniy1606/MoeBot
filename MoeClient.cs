using Discord;
using Discord.WebSocket;
using MoeBot.Command;
using MoeBot.Command.MessageCommands;
using MoeBot.Command.SlashCommands;
using MoeBot.Ignore;
using MoeBot.Tools;

namespace MoeBot {
    internal class MoeClient : DiscordSocketClient {
        internal readonly List<ISlashCommand> SlashCommands;
        internal readonly List<IMessageCommand> MessageCommands;

        public MoeClient() : base(new DiscordSocketConfig() {
            GatewayIntents =
            GatewayIntents.AllUnprivileged |
            GatewayIntents.MessageContent
        }) {
            SlashCommands = new List<ISlashCommand>() {
                new Talk()
            };

            MessageCommands = new List<IMessageCommand>() {
                new EmojiReplacer()
            };

            this.Log += ClientLog;
            this.Ready += ClientReady;
            this.SlashCommandExecuted += ClientSlashCommandExecuted;
            this.MessageReceived += ClientMessageReceived;

            this.LoginAsync(TokenType.Bot, BotInfo.Token);
        }

        private async Task ClientMessageReceived(SocketMessage socketMessage) {
            Logger.DiscordMessage($"{socketMessage.Content} ({socketMessage.Author})");
            if (socketMessage.Author.IsBot) return;

            foreach (IMessageCommand messageCommand in MessageCommands) {
                if (!messageCommand.WantExecute(socketMessage.Content)) return;

                Logger.DiscordCommand($"Execute {messageCommand.GetCommandName()}.....");
                await messageCommand.Execute(socketMessage);

                break;
            }
        }

        private async Task ClientSlashCommandExecuted(SocketSlashCommand socketSlashCommand) {
            Logger.DiscordCommand($"/{socketSlashCommand.Data.Name} ({socketSlashCommand.User})");
            if (socketSlashCommand.User.IsBot) return;

            string slashCommandName = socketSlashCommand.Data.Name;

            foreach (ISlashCommand slashCommand in SlashCommands) {
                if (slashCommand.GetInstance().Name != slashCommandName) return;

                Logger.DiscordCommand($"Execute {slashCommandName}.....");
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

            // Register bot commands
            if (guild != null) {
                try {
                    foreach (ISlashCommand slashCommand in SlashCommands) {
                        // await guild.CreateApplicationCommandAsync(slashCommand.GetInstance().Build());
                        await this.CreateGlobalApplicationCommandAsync(slashCommand.GetInstance().Build());
                    };
                } catch (Exception ex) {
                    Logger.Error($"An exception caused: {ex}");
                }
            } else {
                Logger.Warning("The guild is null!");
            }
        }

        private Task ClientLog(LogMessage logMessage) {
            Console.WriteLine(logMessage.ToString());
            return Task.CompletedTask;
        }
    }
}
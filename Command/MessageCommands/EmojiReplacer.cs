using System.Text.RegularExpressions;
using Discord;
using Discord.WebSocket;
using MoeBot.Tools;

namespace MoeBot.Command.MessageCommands {
    internal class EmojiReplacer : IMessageCommand {
        private static readonly string _commandName = "emojireplacer";

        public string GetCommandName() {
            return _commandName;
        }

        public bool WantExecute(string messageContent) {
            Logger.DiscordCommand($"Call WantExecute: {messageContent}");

            bool result = Regex.IsMatch(messageContent, @"^<:\w+:\d+>$");

            return result;
        }

        public async Task Execute(SocketMessage socketMessage) {
            string messageContent = socketMessage.Content;

            string emojiId = messageContent.Split(':')[2].TrimEnd('>');
            string urlId = $@"https://cdn.discordapp.com/emojis/{emojiId}.png";

            var embedBuilder = new EmbedBuilder()
            .WithAuthor($"{socketMessage.Author.GlobalName} ({socketMessage.Author.Username})", socketMessage.Author.GetAvatarUrl() ?? socketMessage.Author.GetDefaultAvatarUrl())
            .WithImageUrl(urlId)
            .WithColor(Color.Purple)
            .WithCurrentTimestamp();

            var reference = socketMessage.Reference;

            await socketMessage.Channel.SendMessageAsync(embed: embedBuilder.Build(), messageReference: reference);

            await socketMessage.DeleteAsync();
        }
    }
}
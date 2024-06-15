import { Client, Events, GatewayIntentBits, Message, EmbedBuilder, Partials, Collection } from 'discord.js';
import { Logger } from './utils/logger';

import * as fs from 'node:fs';
import * as path from 'node:path';

export class MoeClient extends Client {
    logger: Logger;
    commands: Collection<any, any>;

    constructor() {
        super({
            intents: [
                GatewayIntentBits.Guilds,
                GatewayIntentBits.GuildMessages,
                GatewayIntentBits.MessageContent
            ],
            partials: [Partials.Message],
        });

        this.logger = new Logger();
        this.commands = new Collection();

        this.once(Events.ClientReady, readyClient => {
            this.logger.log(`Logged in as ${readyClient.user.tag}`);
            return;
            const foldersPath = path.join(__dirname, 'commands'); // ./commands/
            const commandFolders = fs.readdirSync(foldersPath); // Read the contents of commands

            for (const folder of commandFolders) {
                const commandsPath = path.join(foldersPath, folder);
                const commandFiles = fs.readdirSync(commandsPath).filter(file => file.endsWith('.js'));
                for (const file of commandFiles) {
                    const filePath = path.join(commandsPath, file);
                    const command = require(filePath);

                    if ('data' in command && 'execute' in command) {
                        this.commands.set(command.data.name, command);
                    } else {
                        this.logger.warning(`${filePath} 内容が不足しているため、読み込みを停止しました。`);
                    }
                }
            }
        });

        this.on(Events.MessageCreate, async (message: Message) => {
            if (message.author.bot) return;

            this.logger.discord(`${message.content} [${message.createdAt.toLocaleString()}] ${message.author.displayName}@${message.author.username}`);

            const contents = message.content;

            if (contents.startsWith('<:') &&
                contents.endsWith('>')) {
                // 一部問題あり
                const emojiId = contents.split(':')[2].slice(0, -1);
                const url = `https://cdn.discordapp.com/emojis/${emojiId}.png`;

                // if (isValidURL())

                const embed = new EmbedBuilder()
                    .setTitle('Link')
                    .setURL(url)
                    .setAuthor({
                        name: `${message.author.displayName} (${message.author.username})`,
                        iconURL: message.author.displayAvatarURL({ extension: 'png' }) ?? 'Failed to get user icon'
                    })
                    .setImage(url)
                    // .setFields({ name: `${message.author.username}がアイコンを送信しました。`, value: `a` })
                    .setColor('Random')
                    .setTimestamp();

                message.channel.send({ embeds: [embed] });
                this.logger.log(`The converted emoji was sent to '${message.channel.id}'`);

                if (message.deletable) {
                    message.delete();
                    this.logger.log(`The original emoji message ( ${message.content} (${message.id}) from ${message.author.username} ) has been deleted`);
                }
            }
        });
    }

    start(token: string) {
        this.logger.log("Logging in...");
        this.login(token);
    }
}
import { Client, Events, GatewayIntentBits, Message, Partials, Collection } from 'discord.js';
import { Logger } from './utils/logger';
import { CommandBase } from './commandBase';

import * as fs from 'node:fs';
import * as path from 'node:path';

export class MoeClient extends Client {
    commands: Collection<string, CommandBase>;

    constructor() {
        super({
            intents: [
                GatewayIntentBits.Guilds,
                GatewayIntentBits.GuildMessages,
                GatewayIntentBits.MessageContent
            ],
            partials: [Partials.Message],
        });

        this.commands = new Collection();

        const directoryPath = path.join(__dirname, 'commands');
        fs.readdir(directoryPath, (err, files) => {
            if (err) {
                Logger.error(`Unable to scan directory: ${err}`);
                return;
            }

            files.forEach((file) => {
                if (path.extname(file) === '.ts') {
                    Logger.log(`Reading ${file}...`);

                    const modulePath = path.join(directoryPath, file);
                    const commandModule: CommandBase = require(modulePath).default;
                    this.commands.set(commandModule.name, commandModule);

                    Logger.log(`Successfully loaded ${commandModule.name}`);
                }
            });
        })

        this.once(Events.ClientReady, readyClient => {
            Logger.log(`Logged in as ${readyClient.user.tag}`);
        });

        this.on(Events.MessageCreate, async (message: Message) => {
            if (message.author.bot) return;
            if (!message.guild) return;

            Logger.discord(`${message.content} [${message.createdAt.toLocaleString()}] ${message.author.displayName}@${message.author.username}`);

            this.commands.forEach((command) => {
                if (command.wantExecute(message.content)) {
                    command.execute(message);
                }
            });
        }
        );
    }

    start(token: string) {
        Logger.log("Logging in...");
        this.login(token);
    }
}
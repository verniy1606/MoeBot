import { Message, EmbedBuilder } from 'discord.js';

import { CommandBase } from '../commandBase';
import { Logger } from '../utils/logger';

const command: CommandBase = {
    name: 'emojiReplacer',
    execute: (message: Message) => {
        const emojiId = message.content.split(':')[2].slice(0, -1);
        const url = `https://cdn.discordapp.com/emojis/${emojiId}.png`;

        const embed = new EmbedBuilder()
            // .setTitle('Link')
            // .setURL(url)
            .setAuthor({
                name: `${message.author.displayName} (${message.author.username})`,
                iconURL: message.author.displayAvatarURL({ extension: 'png' }) ?? 'Failed to get user icon'
            })
            .setImage(url)
            .setColor('Random')
            .setTimestamp();

        message.channel.send({ embeds: [embed] });
        Logger.log(`The converted emoji was sent to '${message.channel.id}'`);

        if (message.deletable) {
            message.delete();
            Logger.log(`The original emoji message ( ${message.content} (${message.id}) from ${message.author.username} ) has been deleted`);
        }
    },
    wantExecute: (contents: string) => {
        const pattern = /^<:\w+:\d+>$/;

        if (pattern.test(contents)) return true;

        return false;
    }
}

export default command;
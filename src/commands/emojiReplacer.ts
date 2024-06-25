import { Message, EmbedBuilder } from 'discord.js';

import { CommandBase } from '../commandBase';
import { Logger } from '../utils/logger';

const command: CommandBase = {
    name: 'emojiReplacer',
    execute: (message: Message) => {
        Logger.log(`execute executed! ${message.content}`);
        return;

        // const url = `https://cdn.discordapp.com/emojis/${match[1]}.png`;
        const url = '';

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
        Logger.log(`The converted emoji was sent to '${message.channel.id}'`);

        if (message.deletable) {
            message.delete();
            Logger.log(`The original emoji message ( ${message.content} (${message.id}) from ${message.author.username} ) has been deleted`);
        }
    },
    wantExecute: (contents: string) => {
        Logger.log(`wantExecute executed! ${contents}`);
        return false;
        
        const pattern = /.*:(.*)>/;

        if (contents.startsWith('<:') &&
            contents.endsWith('>')) {

            const match = contents.match(pattern);
            if (!match) return false;

        }
        return true;
    }
}

export default command;
import { Command } from './commandBase';
import { Message } from 'discord.js';

export class EmojiReplacer extends Command {
    execute() {

    }

    wantExecute(message: Message): (boolean | string)[] {
        // ここで来たメッセージを処理してこのクラスをexecuteするかどうか決める
        return [false, ''];
    }
}
import { Message } from 'discord.js';

export abstract class Command {
    abstract execute(): void;

    abstract wantExecute(message: Message): (boolean | string)[];
}
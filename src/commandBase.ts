import { Message } from 'discord.js';

export interface CommandBase {
    name: string,
    execute: (message: Message) => void,
    wantExecute: (contents: string) => boolean,
}
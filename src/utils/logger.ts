const Colour = {
    Black   : '\u001b[30m',
    Red     : '\u001b[31m',
    Green   : '\u001b[32m',
    Yellow  : '\u001b[33m',
    Blue    : '\u001b[34m',
    Magenta : '\u001b[35m',
    Cyan    : '\u001b[36m',
    White   : '\u001b[37m',

    Default : '\u001b[0m',
} as const;

type Colour = (typeof Colour)[keyof typeof Colour];

export class Logger {
    log(log: string) {
        console.log(Colour.White  + `LOG   : ${log}` + Colour.Default);
    }

    warning(log: string) {
        console.log(Colour.Yellow + `WARN  : ${log}` + Colour.Default);
    }
    
    error(log: string) {
        console.log(Colour.Red    + `ERR   : ${log}` + Colour.Default);
    }

    /** Use this when receiving a message from Discord */
    discord(log: string) {
        console.log(Colour.Blue   + `DISCO : ${log}` + Colour.Default);
    }
}
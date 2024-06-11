import { MoeClient } from './moeclient';
import * as botinfo from '../botinfo.json';

const client = new MoeClient();

client.start(botinfo.token);
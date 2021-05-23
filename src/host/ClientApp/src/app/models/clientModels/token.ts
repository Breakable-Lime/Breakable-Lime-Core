import * as luxon from 'luxon';
import { throwError } from 'rxjs';

export class Token {
    Type: TokenType;
    Token: string;
    Expiration: luxon.DateTime | undefined;

    /**
     *
     */
    constructor(token: string | undefined, type: TokenType, expiery: string | undefined) {

        if (token == undefined) {
            throwError;
        }

        this.Token = token as string;
        this.Type = type;
        
        if (expiery) {
            this.Expiration = luxon.DateTime.fromISO(expiery);
        }
    }

}

export enum TokenType {
    Authentication, Refresh
}


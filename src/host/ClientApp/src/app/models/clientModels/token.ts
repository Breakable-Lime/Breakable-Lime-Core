import * as luxon from 'luxon';

export default class Token {
    Type: TokenType;
    Token: string;
    Expiration: luxon.DateTime | undefined;

    /**
     *
     */
    constructor(token: string, type: TokenType, expiery: string | undefined) {
        this.Token = token;
        this.Type = type;
        
        if (expiery) {
            this.Expiration = luxon.DateTime.fromISO(expiery);
        }
    }

}

export enum TokenType {
    Authentication, Refresh
}


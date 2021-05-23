import { Injectable } from '@angular/core';
import { Token } from '../app/models/clientModels/token';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor() { }

  public SignRequestHeader(): void {
    //TODO: fix
  }

  public StoreTokenPair(authenticationToken: Token, refreshToken: Token | undefined): void {
    //TODO: fix
  }

  public DeAuthenticate(): void {
    //TODO: fix
  }

  public IsAuthenticated(): boolean {
    return false;
  }
}

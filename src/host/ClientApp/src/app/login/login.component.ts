import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from '../authentication.service'
import axios, { AxiosResponse } from 'axios';
import PasswordAuthenticationRequest from '../models/requests/PasswordAuthenticationRequest';
import {TokenType, Token} from '../models/clientModels/token';
import TokenPair from '../models/responses/tokenPair/tokenPair';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  private readonly _authenticationService: AuthenticationService;

  constructor(authenticationService: AuthenticationService, private router: Router) { 
    this._authenticationService = authenticationService;
  }

  ngOnInit(): void {
  }

  async tryAuthenticate(): Promise<void> {
    console.log("tried to authenticate")
    await axios.post("/Authenticate.whatever", 
        new PasswordAuthenticationRequest(this.email.value, this.password.value)).catch(() => {
          this.error = "Wrong email or password!"
        }).then(result => {
          try {
            var res = result as AxiosResponse<TokenPair>
            let data = res.data;
            
            var authenticationToken = 
            new Token(data.AuthenticationToken?.Token, TokenType.Authentication, data.AuthenticationToken?.TokenIsoExpiry);

            var refreshToken = 
            new Token(data.RefreshToken?.Token, TokenType.Refresh, data.RefreshToken?.TokenIsoExpiry);

            this._authenticationService.StoreTokenPair(authenticationToken, refreshToken);

            this.router.navigate(['']); //redirect if all good
          } catch {
            this.error = "Wrong email or password!"
          } 
        });
    console.log("Done!!!")
  }

  email = new FormControl('');
  password = new FormControl('');

  error: string | null = null;

}

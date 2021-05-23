import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { AuthenticationService } from '../authentication.service'

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  private readonly _authenticationService: AuthenticationService;

  constructor(authenticationService: AuthenticationService) { 
    this._authenticationService = authenticationService;
  }

  ngOnInit(): void {
  }

  email = new FormControl('');
  password = new FormControl('');

}

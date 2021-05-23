import { Component, NgModule } from '@angular/core';
import { RouterOutlet, Router } from '@angular/router'
import { NavbarComponent } from 'src/app/navbar/navbar.component';

import { AuthenticationService } from '../app/authentication.service'



@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'BreakableLimeGUI';
  showNavbar = true;

  /**
   *
   */
  constructor(private authenticationService: AuthenticationService, private router: Router) {
    
  }

  ngOnInit() {
    this.checkAuth();
  }

  private checkAuth(): void {
    if (!this.authenticationService.IsAuthenticated()) {
      this.showNavbar = false;
      this.router.navigate(['login']); //route to login
    }
  }
}

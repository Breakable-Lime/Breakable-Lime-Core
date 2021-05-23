import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  NavigationItems: NavigationItem[] = [
    {path: "login", title: "test"},
    {path: "login", title: "test"},
    {path: "login", title: "test"}
  ]

  constructor() { }

  ngOnInit(): void {
  }

}

class NavigationItem {
  public path: string | undefined;
  public title: string | undefined;
}
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { LoginComponent } from './login/login.component';
import { ImageListComponent } from './image-list/image-list.component';
import { ImageFetchComponent } from './image-fetch/image-fetch.component';
import { ContainerListComponent } from './container-list/container-list.component';
import { ContainerLogsComponent } from './container-logs/container-logs.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    LoginComponent,
    ImageListComponent,
    ImageFetchComponent,
    ContainerListComponent,
    ContainerLogsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

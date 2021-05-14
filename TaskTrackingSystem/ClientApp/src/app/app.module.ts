import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { LoginComponent } from "./LoginComponent/login.component";
import { RegisterComponent } from "./RegisterComponent/register.component"

import { ReactiveFormsModule } from '@angular/forms';

import { NgModule } from '@angular/core';

import { Routes, RouterModule } from "@angular/router";

const appRoutes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'reg', component: RegisterComponent },
];


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot(appRoutes),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

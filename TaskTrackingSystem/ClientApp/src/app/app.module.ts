import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { LoginComponent } from "./LoginComponent/login.component";
import { RegisterComponent } from "./RegisterComponent/register.component"

import { ReactiveFormsModule } from '@angular/forms';

import { NgModule } from '@angular/core';

import { Routes, RouterModule } from "@angular/router";
import { AdminComponent } from './AdminComponent/admin.component';

const adminRoutes: Routes = [
  { path: '', component: ProjectsComponent },
  { path: 'addproject', component: AddProjectComponent },
  { path: 'deleteproject', component: DeleteProjectComponent }
]

const appRoutes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'reg', component: RegisterComponent },
  { path: 'admin', component: AdminComponent, children: adminRoutes },
  { path: 'manager', component: ManagerComponent, children: managerRoutes },
  { path: 'employee', component: EmployeeComponent, children: employeeRoutes },
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

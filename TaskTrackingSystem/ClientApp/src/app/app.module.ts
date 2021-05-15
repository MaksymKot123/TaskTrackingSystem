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
import { NotFoundComponent } from './NotFoundComponent/notFound.component';
import { ManagerComponent } from './ManagerComponent/manager.component';
import { EmployeeComponent } from './EmployeeComponent/employee.component';
import { ProjectComponent } from './project/project.component';
//import { ProjectComponent } from './ProjectComponent/project.component';

const adminRoutes: Routes = [
  { path: '', component: ProjectComponent },
  //{ path: 'addproject', component: AddProjectComponent },
  //{ path: 'deleteproject', component: DeleteProjectComponent }
]

const appRoutes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'reg', component: RegisterComponent },
  { path: 'manager', component: ManagerComponent },
  { path: 'admin', component: AdminComponent, children: adminRoutes },
  { path: 'employee', component: EmployeeComponent },
  { path: "**", component: NotFoundComponent },
  //{ path: 'manager', component: ManagerComponent, children: managerRoutes },
  //{ , children: employeeRoutes },
];


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    AdminComponent,
    NotFoundComponent,
    ManagerComponent,
    EmployeeComponent,
    ProjectComponent,
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

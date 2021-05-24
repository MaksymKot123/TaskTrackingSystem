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
import { AddProjectComponent } from './add-project/add-project.component';
import { EditProjectComponent } from './edit-project/edit-project.component';
import { AddTaskComponent } from './add-task/add-task.component';
import { ViewTasksComponent } from './view-tasks/view-tasks.component';
import { AllUsersComponent } from './all-users/all-users.component';
import { AddUsersToProjectComponent } from './add-users-to-project/add-users-to-project.component';
import { ViewProjectsOfUserComponent } from './view-projects-of-user/view-projects-of-user.component';
import { AllEmployeesComponent } from './all-employees/all-employees.component';
import { ChangeUsersRoleComponent } from './change-users-role/change-users-role.component';
//import { ProjectComponent } from './ProjectComponent/project.component';

const adminRoutes: Routes = [
  { path: '', component: ProjectComponent },
  { path: 'users', component: AllUsersComponent },
  //{ path: 'deleteproject', component: DeleteProjectComponent }
];

const managerRoutes: Routes = [
  { path: "", component: ProjectComponent },
  { path: "users", component: AllEmployeesComponent }
];

const appRoutes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'reg', component: RegisterComponent },
  { path: 'manager', component: ManagerComponent, children: managerRoutes },
  { path: 'admin', component: AdminComponent, children: adminRoutes },
  { path: 'employee', component: EmployeeComponent  },
  { path: "**", component: NotFoundComponent },
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
    AddProjectComponent,
    EditProjectComponent,
    AddTaskComponent,
    ViewTasksComponent,
    AllUsersComponent,
    AddUsersToProjectComponent,
    ViewProjectsOfUserComponent,
    AllEmployeesComponent,
    ChangeUsersRoleComponent,
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

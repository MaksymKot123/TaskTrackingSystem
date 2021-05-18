import { Component, OnInit } from '@angular/core';
import { GetUsersByRoleService } from "src/app/Services/getUsersByRoleService";
import { IUser } from '../Interfaces/iuser';

const URL = "https://localhost:44351/account";
const roles = ["Admin", "Manager", "Employee"];

@Component({
  selector: 'app-all-users',
  templateUrl: './all-users.component.html',
  styleUrls: ['./all-users.component.css']
})
export class AllUsersComponent implements OnInit {

  constructor(private usrService: GetUsersByRoleService) { }

  users: IUser[] = [];
  tempUsers: IUser[] = [];


  ngOnInit() {
    for (let role of roles) {
      this.showUsers(role);
    }
  }

  showUsers(roleName: string) {
    let res = this.usrService.getUsers(URL, roleName).subscribe(x => this.tempUsers = x);
    this.users.concat(this.tempUsers);
  }

}

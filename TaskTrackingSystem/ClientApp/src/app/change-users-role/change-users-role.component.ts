import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { RoleService } from "src/app/Services/roleService";

const URL = "https://localhost:44351/account";


@Component({
  selector: 'change-role',
  templateUrl: './change-users-role.component.html',
  styleUrls: ['./change-users-role.component.css']
})
export class ChangeUsersRoleComponent implements OnInit {

  constructor(private service: RoleService) { }

  @Input() usersEmail: string;
  @Input() changeRole: { val: boolean };
  @Output() changeRoleChange = new EventEmitter<{ val: boolean }>();

  error: any;
  selectedRole: string;
  isClosedForm = false;
  msg: string;

  Roles = ["Admin", "Manager", "Employee"];

  ngOnInit() {
  }

  changeUsersRole() {
    if (this.selectedRole != undefined) {
      this.service.changeRole(URL, this.usersEmail, this.selectedRole).subscribe(() => {
        this.msg = "Role has been changed";
      }, error => { this.error = error });
    }
  }

  closeForm(value: { val: boolean }) {
    this.changeRole = value;
    this.changeRoleChange.emit(value);
    this.isClosedForm = !this.isClosedForm;
  }
}

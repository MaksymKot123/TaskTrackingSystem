import { AbstractControl } from "@angular/forms";

export function TaskDateValidator(control: AbstractControl): { [key: string]: boolean } {
  let deadline = control.get("endTime").value;
  let projDeadline = control.get("projEndTime").value;
  let now = new Date();
  if (new Date(deadline) < now || new Date(deadline) > new Date(projDeadline)) {
    return {
      deadlineError: true
    }
  }
  return {};
}

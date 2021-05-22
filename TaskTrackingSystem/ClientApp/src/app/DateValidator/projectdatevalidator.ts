import { AbstractControl } from "@angular/forms";

export function ProjectDateValidator(control: AbstractControl): { [key: string]: boolean } {
  let deadline = control.get("endTime").value;
  let now = new Date();
  if (new Date(deadline) < now) {
    return {
      deadlineError: true
    }
  }
  return {};
}

"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.TaskDateValidator = void 0;
function TaskDateValidator(control) {
    var deadline = control.get("endTime").value;
    var projDeadline = control.get("projEndTime").value;
    var now = new Date();
    if (new Date(deadline) < now || new Date(deadline) > new Date(projDeadline)) {
        return {
            deadlineError: true
        };
    }
    return {};
}
exports.TaskDateValidator = TaskDateValidator;
//# sourceMappingURL=taskdatevalidator.js.map
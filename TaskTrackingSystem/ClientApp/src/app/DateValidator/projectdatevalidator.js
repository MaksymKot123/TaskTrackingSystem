"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ProjectDateValidator = void 0;
function ProjectDateValidator(control) {
    var deadline = control.get("endTime").value;
    var now = new Date();
    if (new Date(deadline) < now) {
        return {
            deadlineError: true
        };
    }
    return {};
}
exports.ProjectDateValidator = ProjectDateValidator;
//# sourceMappingURL=projectdatevalidator.js.map
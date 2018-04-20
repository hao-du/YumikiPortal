"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Project = (function () {
    function Project(projectName, projectDescription) {
        this.projectName = projectName;
        this.projectDescription = projectDescription;
    }
    Project.prototype.setStatus = function (isActive) {
        this.isActive = isActive;
    };
    return Project;
}());
exports.Project = Project;
//# sourceMappingURL=project.js.map
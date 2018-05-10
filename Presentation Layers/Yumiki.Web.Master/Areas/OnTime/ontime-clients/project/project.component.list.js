"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var guid_js_1 = require("../common/guid.js");
var project_model_js_1 = require("../models/project.model.js");
var project_service_js_1 = require("./project.service.js");
var ProjectListComponent = (function () {
    function ProjectListComponent(service) {
        this.service = service;
    }
    ProjectListComponent.prototype.ngOnInit = function () {
        var _this = this;
        yumiki.message.displayLoadingDialog(true);
        this.service.getProjects().subscribe(function (projects) {
            _this.projects = projects;
            console.log(projects);
            yumiki.message.displayLoadingDialog(false);
        });
    };
    ProjectListComponent.prototype.onSelect = function (project) {
        var _this = this;
        if (!project) {
            project = new project_model_js_1.Project();
            project.ID = guid_js_1.Guid.empty;
            this.selectedProject = project;
            console.log("Add with ID: " + project.ID);
        }
        else {
            if (project && project.ID != guid_js_1.Guid.empty) {
                yumiki.message.displayLoadingDialog(true);
                this.service.getProject(project.ID).subscribe(function (result) {
                    console.log(project);
                    project.ProjectName = result.ProjectName;
                    project.AssignedNumber = result.AssignedNumber;
                    project.Prefix = result.Prefix;
                    project.Descriptions = result.Descriptions;
                    project.IsActive = result.IsActive;
                    project.LastUpdateDateUI = result.LastUpdateDateUI;
                    console.log(project);
                    _this.selectedProject = project;
                    console.log("Edit with ID: " + project.ID);
                    yumiki.message.displayLoadingDialog(false);
                });
            }
        }
        console.log("Selected ID: " + project.ID);
    };
    return ProjectListComponent;
}());
ProjectListComponent = __decorate([
    core_1.Component({
        selector: 'ontime',
        templateUrl: '/Apps/OnTime/Project/List',
    }),
    __metadata("design:paramtypes", [project_service_js_1.ProjectService])
], ProjectListComponent);
exports.ProjectListComponent = ProjectListComponent;
//# sourceMappingURL=project.component.list.js.map
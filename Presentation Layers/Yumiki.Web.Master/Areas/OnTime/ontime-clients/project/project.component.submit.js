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
var project_model_js_1 = require("../models/project.model.js");
var project_service_js_1 = require("./project.service.js");
var ProjectSubmitComponent = (function () {
    function ProjectSubmitComponent(service) {
        this.service = service;
        this.messageEvent = new core_1.EventEmitter();
    }
    ProjectSubmitComponent.prototype.ngOnInit = function () {
    };
    ProjectSubmitComponent.prototype.onClose = function () {
        this.project = undefined;
    };
    ProjectSubmitComponent.prototype.onSave = function () {
        var _this = this;
        yumiki.message.displayLoadingDialog(true);
        this.service.saveProject(this.project).subscribe(function (result) {
            console.log(result);
            _this.messageEvent.emit('ok');
            _this.onClose();
            yumiki.message.displayLoadingDialog(false);
        });
    };
    return ProjectSubmitComponent;
}());
__decorate([
    core_1.Input(),
    __metadata("design:type", project_model_js_1.Project)
], ProjectSubmitComponent.prototype, "project", void 0);
__decorate([
    core_1.Output(),
    __metadata("design:type", Object)
], ProjectSubmitComponent.prototype, "messageEvent", void 0);
ProjectSubmitComponent = __decorate([
    core_1.Component({
        selector: 'project-submit',
        templateUrl: '/Apps/OnTime/Project/Submit',
    }),
    __metadata("design:paramtypes", [project_service_js_1.ProjectService])
], ProjectSubmitComponent);
exports.ProjectSubmitComponent = ProjectSubmitComponent;
//# sourceMappingURL=project.component.submit.js.map
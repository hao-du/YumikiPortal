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
var router_1 = require("@angular/router");
var constants_js_1 = require("../common/constants.js");
var user_model_js_1 = require("../models/user.model.js");
var assignment_service_js_1 = require("./assignment.service.js");
var AssignmentComponent = (function () {
    function AssignmentComponent(route, service) {
        this.route = route;
        this.service = service;
        this.user = new user_model_js_1.User();
    }
    AssignmentComponent.prototype.ngOnInit = function () {
        this.getAssignments();
    };
    AssignmentComponent.prototype.onProjectChange = function (project) {
        this.service.saveProjectAssignments(this.user.ID, project.ID, project.IsAssigned).subscribe(function () { }, function (err) {
            yumiki.message.clientMessage(err, '', constants_js_1.Constants.ErrorType);
        });
    };
    AssignmentComponent.prototype.onPhaseChange = function (phase) {
        this.service.savePhaseAssignments(this.user.ID, phase.ID, phase.IsAssigned).subscribe(function () { }, function (err) {
            yumiki.message.clientMessage(err, '', constants_js_1.Constants.ErrorType);
        });
    };
    AssignmentComponent.prototype.getAssignments = function () {
        var _this = this;
        var id = this.route.snapshot.paramMap.get('id');
        this.service.getAssignments(id).subscribe(function (result) {
            _this.user = result;
            yumiki.message.displayLoadingDialog(false);
        }, function (err) {
            yumiki.message.displayLoadingDialog(false);
            yumiki.message.clientMessage(err, '', constants_js_1.Constants.ErrorType);
        });
    };
    return AssignmentComponent;
}());
AssignmentComponent = __decorate([
    core_1.Component({
        selector: 'assignment',
        templateUrl: '/Apps/OnTime/UserAssignment/AssignmentList',
    }),
    __metadata("design:paramtypes", [router_1.ActivatedRoute,
        assignment_service_js_1.AssignmentService])
], AssignmentComponent);
exports.AssignmentComponent = AssignmentComponent;
//# sourceMappingURL=assignment.component.assignment.js.map
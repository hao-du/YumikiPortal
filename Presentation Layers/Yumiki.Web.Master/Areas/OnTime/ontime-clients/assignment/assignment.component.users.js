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
var constants_js_1 = require("../common/constants.js");
var assignment_service_js_1 = require("./assignment.service.js");
var UsersComponent = (function () {
    function UsersComponent(service) {
        this.service = service;
    }
    UsersComponent.prototype.ngOnInit = function () {
        this.getUsers();
    };
    UsersComponent.prototype.getUsers = function () {
        var _this = this;
        yumiki.message.displayLoadingDialog(true);
        this.service.getUsers().subscribe(function (result) {
            _this.users = result;
            yumiki.message.displayLoadingDialog(false);
        }, function (err) {
            yumiki.message.displayLoadingDialog(false);
            yumiki.message.clientMessage(err, '', constants_js_1.Constants.ErrorType);
        });
    };
    return UsersComponent;
}());
UsersComponent = __decorate([
    core_1.Component({
        selector: 'ontime',
        templateUrl: '/Apps/OnTime/UserAssignment/UserList',
    }),
    __metadata("design:paramtypes", [assignment_service_js_1.AssignmentService])
], UsersComponent);
exports.UsersComponent = UsersComponent;
//# sourceMappingURL=assignment.component.users.js.map
"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var assignment_component_user_js_1 = require("./assignment.component.user.js");
var assignment_component_assignment_js_1 = require("./assignment.component.assignment.js");
var routes = [
    { path: '', redirectTo: '/User', pathMatch: 'full' },
    { path: 'User', component: assignment_component_user_js_1.UserComponent },
    { path: 'Assignment/:id', component: assignment_component_assignment_js_1.AssignmentComponent }
];
var AssignmentRouting = (function () {
    function AssignmentRouting() {
    }
    return AssignmentRouting;
}());
AssignmentRouting = __decorate([
    core_1.NgModule({
        imports: [router_1.RouterModule.forRoot(routes)],
        exports: [router_1.RouterModule]
    })
], AssignmentRouting);
exports.AssignmentRouting = AssignmentRouting;
//# sourceMappingURL=assignment.rounting.js.map
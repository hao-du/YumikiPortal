"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var platform_browser_1 = require("@angular/platform-browser");
var forms_1 = require("@angular/forms");
var http_1 = require("@angular/common/http");
var assignment_component_list_js_1 = require("./assignment.component.list.js");
var assignment_component_user_js_1 = require("./assignment.component.user.js");
var assignment_component_assignment_js_1 = require("./assignment.component.assignment.js");
var assignment_service_js_1 = require("./assignment.service.js");
var assignment_rounting_js_1 = require("./assignment.rounting.js");
var AppModule = (function () {
    function AppModule() {
    }
    return AppModule;
}());
AppModule = __decorate([
    core_1.NgModule({
        imports: [platform_browser_1.BrowserModule, forms_1.FormsModule, http_1.HttpClientModule, assignment_rounting_js_1.AssignmentRouting],
        declarations: [assignment_component_list_js_1.ListComponent, assignment_component_user_js_1.UserComponent, assignment_component_assignment_js_1.AssignmentComponent],
        bootstrap: [assignment_component_list_js_1.ListComponent],
        providers: [assignment_service_js_1.AssignmentService]
    })
], AppModule);
exports.AppModule = AppModule;
//# sourceMappingURL=assignment.module.js.map
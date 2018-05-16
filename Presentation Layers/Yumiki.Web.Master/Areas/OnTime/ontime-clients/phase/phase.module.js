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
var ngx_bootstrap_1 = require("ngx-bootstrap");
var phase_component_list_js_1 = require("./phase.component.list.js");
var phase_component_submit_js_1 = require("./phase.component.submit.js");
var phase_service_js_1 = require("./phase.service.js");
var AppModule = (function () {
    function AppModule() {
    }
    return AppModule;
}());
AppModule = __decorate([
    core_1.NgModule({
        imports: [platform_browser_1.BrowserModule, forms_1.FormsModule, http_1.HttpClientModule, ngx_bootstrap_1.BsDatepickerModule.forRoot()],
        declarations: [phase_component_list_js_1.PhaseListComponent, phase_component_submit_js_1.PhaseSubmitComponent],
        bootstrap: [phase_component_list_js_1.PhaseListComponent],
        providers: [phase_service_js_1.PhaseService]
    })
], AppModule);
exports.AppModule = AppModule;
//# sourceMappingURL=phase.module.js.map
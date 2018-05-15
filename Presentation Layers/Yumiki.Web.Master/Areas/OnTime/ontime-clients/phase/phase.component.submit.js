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
var phase_model_js_1 = require("../models/phase.model.js");
var phase_service_js_1 = require("./phase.service.js");
var PhaseSubmitComponent = (function () {
    function PhaseSubmitComponent(service) {
        this.service = service;
        this.messageEvent = new core_1.EventEmitter();
        this.isControlInitialized = false;
    }
    PhaseSubmitComponent.prototype.ngAfterViewChecked = function () {
        if (!this.isControlInitialized && this.phase) {
            console.log(this);
            yumiki.ontime.initDateTimePicker();
            console.log("ngAfterContentChecked");
            this.isControlInitialized = true;
        }
    };
    PhaseSubmitComponent.prototype.onClose = function () {
        this.phase = undefined;
        this.isControlInitialized = false;
    };
    PhaseSubmitComponent.prototype.onSave = function () {
        var _this = this;
        yumiki.message.displayLoadingDialog(true);
        console.log(this.phase);
        this.service.savePhase(this.phase).subscribe(function () {
            _this.messageEvent.emit('ok');
            _this.onClose();
            yumiki.message.displayLoadingDialog(false);
        }, function (err) {
            yumiki.message.displayLoadingDialog(false);
            yumiki.message.clientMessage(err, '', constants_js_1.Constants.ErrorType);
        });
    };
    return PhaseSubmitComponent;
}());
__decorate([
    core_1.Input(),
    __metadata("design:type", phase_model_js_1.Phase)
], PhaseSubmitComponent.prototype, "phase", void 0);
__decorate([
    core_1.Output(),
    __metadata("design:type", Object)
], PhaseSubmitComponent.prototype, "messageEvent", void 0);
PhaseSubmitComponent = __decorate([
    core_1.Component({
        selector: 'phase-submit',
        templateUrl: '/Apps/OnTime/Phase/Submit',
    }),
    __metadata("design:paramtypes", [phase_service_js_1.PhaseService])
], PhaseSubmitComponent);
exports.PhaseSubmitComponent = PhaseSubmitComponent;
//# sourceMappingURL=phase.component.submit.js.map
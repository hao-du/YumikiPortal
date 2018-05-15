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
var constants_js_1 = require("../common/constants.js");
var phase_model_js_1 = require("../models/phase.model.js");
var phase_service_js_1 = require("./phase.service.js");
var PhaseListComponent = (function () {
    function PhaseListComponent(service) {
        this.service = service;
        this.showActiveList = true;
    }
    PhaseListComponent.prototype.ngOnInit = function () {
        this.getPhases();
    };
    PhaseListComponent.prototype.onSelect = function (phase) {
        var _this = this;
        if (!phase) {
            phase = new phase_model_js_1.Phase();
            phase.ID = guid_js_1.Guid.empty;
            phase.IsActive = true;
            phase.Status = "";
            this.selectedPhase = phase;
            console.log(phase);
        }
        else {
            if (phase && phase.ID != guid_js_1.Guid.empty) {
                yumiki.message.displayLoadingDialog(true);
                this.service.getPhase(phase.ID).subscribe(function (result) {
                    _this.selectedPhase = phase = result;
                    yumiki.message.displayLoadingDialog(false);
                }, function (err) {
                    yumiki.message.displayLoadingDialog(false);
                    yumiki.message.clientMessage(err, '', constants_js_1.Constants.ErrorType);
                });
            }
        }
    };
    PhaseListComponent.prototype.onShowList = function () {
        this.showActiveList = !this.showActiveList;
        this.getPhases();
    };
    PhaseListComponent.prototype.onRefreshData = function (message) {
        if (message == 'ok') {
            this.getPhases();
        }
    };
    PhaseListComponent.prototype.getPhases = function () {
        var _this = this;
        yumiki.message.displayLoadingDialog(true);
        this.service.getPhases(this.showActiveList).subscribe(function (result) {
            _this.phases = result;
            yumiki.message.displayLoadingDialog(false);
        }, function (err) {
            yumiki.message.displayLoadingDialog(false);
            yumiki.message.clientMessage(err, '', constants_js_1.Constants.ErrorType);
        });
    };
    return PhaseListComponent;
}());
PhaseListComponent = __decorate([
    core_1.Component({
        selector: 'ontime',
        templateUrl: '/Apps/OnTime/Phase/List',
    }),
    __metadata("design:paramtypes", [phase_service_js_1.PhaseService])
], PhaseListComponent);
exports.PhaseListComponent = PhaseListComponent;
//# sourceMappingURL=phase.component.list.js.map
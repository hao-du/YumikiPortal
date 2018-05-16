"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var http_1 = require("@angular/common/http");
var rxjs_1 = require("rxjs");
var operators_1 = require("rxjs/operators");
require("rxjs/add/observable/throw");
var httpOptions = {
    headers: new http_1.HttpHeaders({ 'Content-Type': 'application/json' })
};
var BaseService = (function () {
    function BaseService(httpClient) {
        this.httpClient = httpClient;
    }
    BaseService.prototype.doGet = function (url) {
        return this.httpClient.get(url).pipe(operators_1.catchError(this.errorHandler));
    };
    BaseService.prototype.doPost = function (url, obj) {
        return this.httpClient.post(url, obj, httpOptions).pipe(operators_1.catchError(this.errorHandler));
    };
    BaseService.prototype.errorHandler = function (response) {
        return rxjs_1.Observable.throw(response.error || 'Server error');
    };
    return BaseService;
}());
exports.BaseService = BaseService;
//# sourceMappingURL=base.service.js.map
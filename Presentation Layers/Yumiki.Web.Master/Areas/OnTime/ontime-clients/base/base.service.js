"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var http_1 = require("@angular/common/http");
var Observable_1 = require("rxjs/Observable");
require("rxjs/add/operator/catch");
require("rxjs/add/observable/throw");
var httpOptions = {
    headers: new http_1.HttpHeaders({ 'Content-Type': 'application/json' })
};
var BaseService = (function () {
    function BaseService(httpClient) {
        this.httpClient = httpClient;
    }
    BaseService.prototype.doGet = function (url) {
        return this.httpClient.get(url)
            .catch(this.errorHandler);
    };
    BaseService.prototype.doPost = function (url, obj) {
        return this.httpClient.post(url, obj, httpOptions)
            .catch(this.errorHandler);
    };
    BaseService.prototype.errorHandler = function (response) {
        return Observable_1.Observable.throw(response.error || 'Server error');
    };
    return BaseService;
}());
exports.BaseService = BaseService;
//# sourceMappingURL=base.service.js.map
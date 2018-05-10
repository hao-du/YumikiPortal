"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Guid = (function () {
    function Guid(value) {
        this.value = this.empty;
        if (value) {
            if (Guid.isValid(value)) {
                this.value = value;
            }
        }
    }
    Guid.newGuid = function () {
        return new Guid('xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0;
            var v = (c == 'x') ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        }));
    };
    Object.defineProperty(Guid, "empty", {
        get: function () {
            return '00000000-0000-0000-0000-000000000000';
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(Guid.prototype, "empty", {
        get: function () {
            return Guid.empty;
        },
        enumerable: true,
        configurable: true
    });
    Guid.isValid = function (str) {
        var validRegex = /^[0-9a-f]{8}-[0-9a-f]{4}-[1-5][0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}$/i;
        return validRegex.test(str);
    };
    Guid.prototype.toString = function () {
        return this.value;
    };
    Guid.prototype.toJSON = function () {
        return this.value;
    };
    return Guid;
}());
exports.Guid = Guid;
//# sourceMappingURL=guid.js.map
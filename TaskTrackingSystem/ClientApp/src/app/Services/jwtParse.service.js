"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.JwtParseService = void 0;
var core_1 = require("@angular/core");
core_1.Injectable({
    providedIn: "root"
});
var JwtParseService = /** @class */ (function () {
    function JwtParseService() {
    }
    JwtParseService.prototype.parseJwt = function (token) {
        var base64Url = token.split('.')[1];
        var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
        var jsonPayload = decodeURIComponent(atob(base64).split('.').map(function (c) {
            return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
        }).join(''));
        return JSON.parse(jsonPayload);
    };
    return JwtParseService;
}());
exports.JwtParseService = JwtParseService;
//# sourceMappingURL=jwtParse.service.js.map
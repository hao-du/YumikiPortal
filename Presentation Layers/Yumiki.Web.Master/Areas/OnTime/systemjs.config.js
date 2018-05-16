(function (global) {
    System.config({
        paths: {
            'npm:': '/areas/ontime/node_modules/'
        },
        map: {
            'app': 'ontime-clients',

            '@angular/core': 'npm:@angular/core/bundles/core.umd.js',
            '@angular/common': 'npm:@angular/common/bundles/common.umd.js',
            '@angular/compiler': 'npm:@angular/compiler/bundles/compiler.umd.js',
            '@angular/platform-browser': 'npm:@angular/platform-browser/bundles/platform-browser.umd.js',
            '@angular/platform-browser-dynamic': 'npm:@angular/platform-browser-dynamic/bundles/platform-browser-dynamic.umd.js',
            '@angular/http': 'npm:@angular/http/bundles/http.umd.js',
            '@angular/router': 'npm:@angular/router/bundles/router.umd.js',
            '@angular/forms': 'npm:@angular/forms/bundles/forms.umd.js',

            '@angular/common/http': 'npm:@angular/common/bundles/common-http.umd.js',

            'ngx-bootstrap': 'npm:ngx-bootstrap',

            'tslib': 'npm:tslib/tslib.js',

            'rxjs': 'npm:rxjs',
            "rxjs-compat": "npm:rxjs-compat",
        },
        packages: {
            app: {
                defaultExtension: 'js'
            },
            rxjs: {
                main: 'bundles/rxjs.umd.min.js',
                defaultExtension: 'js'
            },
            'ngx-bootstrap': {
                main: 'bundles/ngx-bootstrap.umd.min.js',
                defaultExtension: 'js'
            },
            'rxjs/operators': {
                main: 'index.js',
                defaultExtension: 'js'
            },
            'rxjs/internal-compatibility': {
                main: 'index.js',
                defaultExtension: 'js'
            },
            "rxjs-compat": {
                main: "index.js",
                defaultExtension: "js"
            }
        }
    });
})(this);

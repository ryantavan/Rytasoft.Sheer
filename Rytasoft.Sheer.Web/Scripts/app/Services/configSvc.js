'use strict';

app.service('configSvc', function () {
    if (window.location.host.match(/localhost/)) {
        return this.API = 'http://localhost:65044/';
    } else {
        return this.API = 'http://test.sheer.com/';
    }
});
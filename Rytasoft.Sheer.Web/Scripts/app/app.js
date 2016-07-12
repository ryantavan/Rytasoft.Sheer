'use strict'

var app = angular.module('app', []);
app.config(['$httpProvider', function ($httpProvider) {
    // enable http caching
    $httpProvider.defaults.cache = true;
}]);

app.factory('MyCache', function ($cacheFactory) {
    return $cacheFactory('myCache');
});

(function () {
    'use strict';

    config.$inject = ['$routeProvider', '$locationProvider'];

    angular.module('moviesApp', [
        'ngRoute',
        'moviesServices'
    ]).config(config);

    function config($routeProvider, $locationProvider) {
        $routeProvider
            .when('/', {
                templateUrl: '/Views/MovieApp/list.html',
                controller: 'MoviesListController'
            })
            .when('/movies/add', {
                templateUrl: '/Views/MovieApp/add.html',
                controller: 'MoviesAddController'
            })
            .when('/movies/edit/:id', {
                templateUrl: '/Views/MovieApp/edit.html',
                controller: 'MoviesEditController'
            })
            .when('/movies/delete/:id', {
                templateUrl: '/Views/MovieApp/delete.html',
                controller: 'MoviesDeleteController'
            });

        $locationProvider.html5Mode(true);
    }
})();
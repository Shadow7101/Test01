angular.module("App1", ["ngRoute"]);

angular.module("App1").controller("RootController", function($scope){

    $('#page-top').removeClass('fixed-nav sticky-footer bg-dark');
    $('#page-top').addClass('bg-dark');
    $('#mainNav').hide();
    $('footer').hide();
    $('#content-wrapper').removeClass('content-wrapper');
    $('#container-fluid').removeClass('container-fluid');

    
}); 

angular.module("App1").config(function($routeProvider) {
    $routeProvider
    .when("/", {
        templateUrl : "/ng/views/blank.html"
    })
    .when("/login", {
        templateUrl : "/ng/views/login.html"
    })
    .when("/register", {
        templateUrl : "/ng/views/register.html"
    })
    .when("/forgot-password", {
        templateUrl : "/ng/views/forgot-password.html"
    });
});
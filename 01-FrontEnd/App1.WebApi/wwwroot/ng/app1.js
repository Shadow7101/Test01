angular.module("App1", ["ngRoute"]);

angular.module("App1").config(function($routeProvider) {
    $routeProvider
    .when("/", {
        templateUrl : "/ng/views/blank.html",
        controller: "BlankController"
    })
    .when("/login", {
        templateUrl : "/ng/views/login.html",
        controller: "LoginController"
    })
    .when("/register", {
        templateUrl : "/ng/views/register.html",
        controller: "RegisterController"
    })
    .when("/forgot-password", {
        templateUrl : "/ng/views/forgot-password.html",
        controller: "ForgotPasswordController"
    });
});
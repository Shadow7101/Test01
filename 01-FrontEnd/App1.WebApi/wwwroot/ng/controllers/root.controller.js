angular.module("App1").controller("RootController", function ($scope, $rootScope, $location) {


    function _logout() {
        $('#page-top').removeClass('fixed-nav sticky-footer bg-dark');
        $('#page-top').addClass('bg-dark');
        $('#mainNav').hide();
        $('footer').hide();
        $('#content-wrapper').removeClass('content-wrapper');
        $('#container-fluid').removeClass('container-fluid');

        sessionStorage.removeItem('currentUser');

        $location.path('/login');
    }

    function _login() {
        var data = sessionStorage.getItem('currentUser');
        if (data == null) {
            console.log('usuario inválido!');
            return;
        }


        $('#page-top').removeClass('bg-dark');
        $('#page-top').addClass('fixed-nav sticky-footer bg-dark');
        $('#mainNav').show();
        $('footer').show();
        $('#content-wrapper').addClass('content-wrapper');
        $('#container-fluid').addClass('container-fluid');

        $location.path('/');
    }

    _logout();

    $scope.Logout = function () { _logout(); }

    $scope.Login = function () { _login(); }


}); 
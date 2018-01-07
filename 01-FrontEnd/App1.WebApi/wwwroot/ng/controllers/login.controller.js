angular.module("App1").controller("LoginController", function ($scope, $location, UsersService) {

    $scope.ErroNoLogin = false;
    $scope.ErroNoLoginMessage = 'Nome de usu�rio ou senha incorreta!';


    $scope.Login = function (model) {
        UsersService.Login(model)
            .then(function (response) {

                if (response.success) {
                    $scope.ErroNoLogin = false;
                   var data = JSON.stringify(response.data);
                   sessionStorage.setItem('currentUser', data);
                   $scope.$parent.Login();
                }


                //nome de usu�rio ou senha incorretas
                $scope.CadastroSucesso = false;
                $scope.ErroNoLogin = true;
                $scope.ErroNoLoginMessage = response.message;
            }, function (response) {
                $scope.ErroNoLogin = true;
                $scope.ErroNoLoginMessage = "Tente novamente, se o error persistir entre em contato com o suporte";
                console.error(response);
            });
    }

});
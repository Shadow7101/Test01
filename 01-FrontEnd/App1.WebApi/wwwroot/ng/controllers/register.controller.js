angular.module("App1").controller("RegisterController", function ($scope, UsersService) {

    $scope.genders = [{ id: 3, name: "Não informar" }, { id: 6, name: "Masculino" }, { id: 9, name: "Feminino" }];
    $scope.CadastroSucesso = false;
    $scope.ErroNoCadastro = false;
    $scope.ErroNoCadastroMessage = "Tente novamente, se o error persistir entre em contato com o suporte";
    $scope.model = { gender1: $scope.genders[0] };



    $scope.Register = function (model) {

        model.gender = model.gender1.id;

        UsersService.Register(model)
            .then(function (response) {
                if (response.success) {
                    $scope.CadastroSucesso = true;
                    $scope.ErroNoCadastro = false;
                }
                else {
                    $scope.CadastroSucesso = false;
                    $scope.ErroNoCadastro = true;
                    $scope.ErroNoCadastroMessage = response.message;
                }

            }, function (response) {
                $scope.CadastroSucesso = false;
                $scope.ErroNoCadastro = true;
                $scope.ErroNoCadastroMessage = "Tente novamente, se o error persistir entre em contato com o suporte";
                console.error(response);
            });

    };

});
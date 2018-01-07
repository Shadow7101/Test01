angular.module('App1').factory('UsersService', function ($http) {
    var _service = {};

    _service.Login = function (model) {
        var envelope = {
            method: 'POST',
            url: "/api/Users",
            data: model
        };
        return $http(envelope)
          .then(function (res) {
              return res.data;
          });
    };

    _service.Register = function (model) {
        var envelope = {
            method: 'POST',
            url: "/api/Users/Register",
            data: model
        };
        return $http(envelope)
          .then(function (res) {
              return res.data;
          });
    };

    _service.ConfirmEmail = function (id) {
        var TokenId = $('#userId').val();
        var envelope = {
            method: 'GET',
            url: "/api/Accounts/ConfirmEmail",
            params: { Id: id }
        };
        return $http(envelope)
          .then(function (res) {
              return res.data;
          });
    };


    return _service;
});

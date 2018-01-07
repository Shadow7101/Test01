angular.module("App1").controller("BlankController", function($scope, $location){

    console.log('BlankController');

    var data =  sessionStorage.getItem('currentUser');
    if(data==null){

    }

});
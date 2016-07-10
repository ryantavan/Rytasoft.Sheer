"use strict"

app.controller('TestAPICtrl', ['$scope', 'sheerSvc', '$log', function ($scope, sheerSvc, $log) {

    sheerSvc.getSchema("TestAPI", function (API) {
        $log.info(API);
        $scope.API = API;
        API.Get.Exec(function (data) {
            $log.info(data);
        });


            $scope.API.GetSpecial.Exec("{FAA60CD4-24A1-46F4-A35B-FECEE6573C27}", function (data) {
                alert(data);
                $log.info(data);
            });
        
    });


    $scope.onclick = function () {
        $scope.API.GetSpecial.Exec("{1833D84A-393E-4155-9390-A07862B00A3C}", function (data) {
            alert(data);
            $log.info(data);
        }, function (error) {
            $log.info(error);
        });
    }

    $scope.postItem = function () {
        $scope.API.Delete.Exec(8 , function () {
            alert("Its Posted");
        });
    }
}]);
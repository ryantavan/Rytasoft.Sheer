"use strict"

app.controller('TestAPICtrl', ['$scope', 'sheerSvc', '$log', function ($scope, sheerSvc, $log) {
    $scope.Original = [];

    // We start page by getting the api schema
    sheerSvc.getSchema("TestAPI", function (API) {
        $log.info(API);
        // We put API schema inside scope to be reused everywhere
        // It can be even be stored inside the rootScope and be accessible through every single controller
        $scope.API = API;
        $scope.GetLabels();


    });

    // We try to get the model label if there is any in model properties
    $scope.GetLabels = function()
    {
        $scope.Labels = {};
        for (var i = 0; i < $scope.API.Properties.length; i++) {
            var property = $scope.API.Properties[i];
            for (var j = 0; j < property.Attributes.length; j++) {
                if (property.Attributes[j].AttributeName == "DisplayAttribute") {
                    for (var k = 0; k < property.Attributes[j].AttributeProperties.length; k++) {
                        if (property.Attributes[j].AttributeProperties[k].Key == "Name") {
                            $scope.Labels[property.Name] = property.Attributes[j].AttributeProperties[k].Value
                        }
                    }
                }
            }
        }
    }


    $scope.GetStudents = function () {
        $scope.API.Get.Exec(function (data) {
            $scope.StudentList = data;
            $scope.Original = angular.copy($scope.StudentList);

            $log.info(data);
        }, function (error) {
            $log.info(error);
        });
    }

    $scope.GetAPI = function () {
        sheerSvc.getSchema("TestAPI", function (API) {
            $log.info(API);
            // We put API schema inside scope to be reused everywhere
            // It can be even be stored inside the rootScope and be accessible through every single controller
            $scope.API = API;
        });
    };

    $scope.Delete = function (row, index) {
        $scope.API.Delete.Exec(row.Id, function () {
            $scope.StudentList.splice(index, 1);
            $scope.Original.splice(index, 1);
            alert(row.FirstName + " is deleted");
        }); // for simplicity we can remove error handlers and handle that in app level with $httpProvider. 
    }

    $scope.AddStudent = function () {
        var student = { Id: 0, FirstName: "", LastName: "", Score: 0 };
        student.$edit = true;
        $scope.StudentList.push(student);
    }

    $scope.CancelChanges = function (row, index) {
        if (row.Id == 0) {
            $scope.StudentList.splice(index, 1);
        }
        else
        {
            $scope.StudentList[index] = angular.copy( $scope.Original[index]);
        }
        row.$edit = false;
    }

    $scope.SaveChanges = function (row) {
        if (row.Id == 0) {
            // new row inserted
            $scope.API.Post.Exec(row, function (data) {
                row.Id = data;
                $scope.Original = angular.copy($scope.StudentList);
            }, function () {
                // somthing went wrong
            });
        }
        else
        {
            $scope.API.Put.Exec(row, function (data) {
                $scope.Original = angular.copy($scope.StudentList);
            }, function () {
                // somthing went wrong
            });
        }

        row.$edit = false;
    }


}]);


app.factory('sheerSvc', ['$log','$http', '$q', 'configSvc', function ($log,$http, $q, configSvc) {
    return ({
        getSchema: getSchema,
    });



    function executer(structure, execFunction) {
        this.structure = structure;
        this.Exec = execFunction;
    }

    function getSchema(apiName, returSchemaObject) {
        var request = $http({
            method: "get",
            url: configSvc.API + "api/"+apiName+"/GetSchema"
        });
        return (request.then(
            function (schema) {
                var outPutObject = {};

                for (var i = 0; i < schema.data.Functions.length; i++) {

                    var funcn = schema.data.Functions[i];

                    var executerObj = new executer(funcn,
                       function () {
                           $log.info(this);
                           var returnFunc = null;
                           var errorFunc = null;
                           var parameters = "";
                           var dataObject = null;
                           var dataObjectVarName = null;
                           if (arguments.length - 2 > 0 && typeof arguments[arguments.length - 2] === "function") {
                               if (arguments.length > 1) {
                                   //looping through parameters and add them to url
                                   for (var i = 0; i < arguments.length - 2; i++) {
                                       if (this.structure.Parameters[i].Level == 0) {
                                           parameters = parameters + "/" + arguments[i];
                                       }
                                       else
                                       {
                                           dataObject = arguments[i];
                                           dataObjectVarName = this.structure.Parameters[i].ParameterName;
                                       }
                                   }
                               }
                               
                               returnFunc = arguments[arguments.length - 2];
                               errorFunc = arguments[arguments.length - 1];
                           }
                           else
                           {
                               if (arguments.length > 1) {
                                   //looping through parameters and add them to url
                                   for (var i = 0; i < arguments.length - 1; i++) {
                                       if (this.structure.Parameters[i].Level == 0) {
                                           parameters = parameters + "/" + arguments[i];
                                       }
                                       else {
                                           dataObject = arguments[i];
                                           dataObjectVarName = this.structure.Parameters[i].ParameterName;
                                       }
                                   }
                               }
                               returnFunc = arguments[arguments.length - 1];
                           }

                           if (this.structure.Name.toUpperCase().startsWith("GET")) {
                               var getReq = $http({
                                   method: "get",
                                   url: configSvc.API + "api/" + apiName + "/" + this.structure.Name + parameters
                               });
                               getReq.then(function (receivedData) {
                                   returnFunc(receivedData.data);

                               }, function (error) {
                                   if (errorFunc != null) {
                                       errorFunc(error);
                                   }
                               });
                           }

                           
                           if (this.structure.Name.toUpperCase().startsWith("POST")) {
                               var request = $http({
                                   method: "post",
                                   url: configSvc.API +"api/"+ apiName + "/" + this.structure.Name + parameters,
                                   data: dataObject 

                               });
                               request.then(function (receivedData) {
                                   returnFunc(receivedData.data);

                               }, function (error) {
                                   if (errorFunc != null) {
                                       errorFunc(error);
                                   }
                               });
                           }

                           if (this.structure.Name.toUpperCase().startsWith("PUT")) {
                               var request = $http({
                                   method: "put",
                                   url: configSvc.API +"api/"+ apiName + "/" + this.structure.Name + parameters,
                                   data: dataObject 

                               });
                               request.then(function (receivedData) {
                                   returnFunc(receivedData.data);

                               }, function (error) {
                                   if (errorFunc != null) {
                                       errorFunc(error);
                                   }
                               });
                           }

                           if (this.structure.Name.toUpperCase().startsWith("DELETE")) {
                               var request = $http({
                                   method: "delete",
                                   url: configSvc.API + 'api/' +apiName + "/" + this.structure.Name + parameters
                               });
                               request.then(function (receivedData) {
                                   returnFunc(receivedData.data);

                               }, function (error) {
                                   if (errorFunc != null) {
                                       errorFunc(error);
                                   }
                               });
                           }
                       });

                    outPutObject[funcn.Name] = executerObj;

                }


                returSchemaObject(outPutObject);
            })
        );
    }
}]);
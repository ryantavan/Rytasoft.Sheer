using Rytasoft.Seer.API.Attributes;
using Rytasoft.Seer.API.Classes;
using Rytasoft.Seer.API.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;


namespace Rytasoft.Seer.API
{
    public class SheerAPIController: ApiController
    {
        [System.Web.Http.ActionName("GetSchema")]
        [CacheWebApi(Duration = int.MaxValue)]
        public APISchema GetSchema()
        {
            APISchema Schema = new APISchema();

            Type typ = this.GetType();
            Schema.Name = typ.Name;
            List<APIFunction> functions = new List<APIFunction>();
            foreach (var method in typ.GetMethods())
            {
                if (method.Name == "GetSchema")
                {
                    break;
                }
                APIFunction func = new APIFunction();
                func.Name = method.Name;
                func.ReturnType = method.ReturnType.Name;
                var parameters = method.GetParameters();
                List<APIParameter> funcParameters = new List<APIParameter>();

                foreach (var param in parameters)
                {
                    var prm = new APIParameter() { ParameterName = param.Name, ParameterType = param.ParameterType.Name };
                    var paramLevel = param.CustomAttributes.Where(c => c.AttributeType.Name == "FromBodyAttribute").ToList();
                    if (paramLevel.Any())
                    {
                        prm.Level = ParameterLevel.Body;
                    }
                    else
                    {
                        prm.Level = ParameterLevel.Url;
                    }
                    funcParameters.Add(prm);
                }
                func.Parameters = funcParameters;
                functions.Add(func);
            }
            Schema.Functions = functions;
            return Schema;
        }
    }
}

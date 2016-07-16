using Rytasoft.Seer.API.Attributes;
using Rytasoft.Seer.API.Classes;
using Rytasoft.Seer.API.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            List<APIProperty> apiParameters = new List<APIProperty>();
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
                        Type paramtype = param.ParameterType;
                        Type modelType = null;
                        if (typeof(ICollection<>).IsAssignableFrom(paramtype))
                        {
                            // get a Model type
                            modelType = paramtype.GetGenericArguments()[0];

                        }
                        else
                        {
                            modelType = paramtype;
                        }

                        PropertyInfo[] properties = modelType.GetProperties();
                        List<APIProperty> propers = new List<APIProperty>();
                        foreach (PropertyInfo property in properties)
                        {
                            APIProperty prp = new APIProperty()
                            {
                                Name = property.Name,
                                Attributes = property.GetCustomAttributes().Select(x => x.GetType().Name).ToList()
                            };

                            var attrs = property.GetCustomAttributes();
                            propers.Add(prp);
                            if (prp.Attributes != null && !apiParameters.Where(x => x.Name == prp.Name).Any()) {
                                apiParameters.Add(prp);
                            }
                        }
                        prm.Properties = propers;
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
            Schema.Properties = apiParameters;
            return Schema;
        }
    }
}

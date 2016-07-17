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
            // Create the API Schema
            APISchema Schema = new APISchema();
            List<APIProperty> apiParameters = new List<APIProperty>();
            Type typ = this.GetType();
            Schema.Name = typ.Name;
            List<APIFunction> functions = new List<APIFunction>();
            // Loop through all api methods
            foreach (var method in typ.GetMethods())
            {
                //skip the getSchema
                if (method.Name == "GetSchema")
                {
                    break;
                }
                APIFunction func = new APIFunction();
                func.Name = method.Name;
                func.ReturnType = method.ReturnType.Name;
                // get the list of parameters
                var parameters = method.GetParameters();
                List<APIParameter> funcParameters = new List<APIParameter>();
                // loop through parameters
                foreach (var param in parameters)
                {
                    var prm = new APIParameter() { ParameterName = param.Name, ParameterType = param.ParameterType.Name };
                    // identify the FromBody parameters
                    var paramLevel = param.CustomAttributes.Where(c => c.AttributeType.Name == "FromBodyAttribute").ToList();
                    if (paramLevel.Any())
                    {
                        prm.Level = ParameterLevel.Body;
                        Type paramtype = param.ParameterType;
                        Type modelType = null;
                        // get the parameter class type single or collection of classes
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
                        // loop through class properties
                        foreach (PropertyInfo property in properties)
                        {
                            List<APIAttribute> attribs = new List<APIAttribute>();
                            // get the property attributes
                            var attributes = property.CustomAttributes;
                            // loop through property attributes
                            foreach (var attribute in attributes)
                            {

                                var nameArgs = new List<KeyValuePair<string, string>>();
                                // loop through attributes arguments
                                foreach (var namedAttr in attribute.NamedArguments)
                                {
                                    nameArgs.Add(new KeyValuePair<string, string>(namedAttr.MemberName, namedAttr.TypedValue.Value.ToString()));
                                }
                                var attr = new APIAttribute() { AttributeName = attribute.AttributeType.Name, AttributeProperties = nameArgs};

                                attribs.Add(attr);
                            }

                            APIProperty prp = new APIProperty()
                            {
                                Name = property.Name,
                                Attributes =attribs
                            };

                            propers.Add(prp);
                            // also add the property to api attributes if they have attributes
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

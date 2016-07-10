using Rytasoft.Seer.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Rytasoft.Sheer.Web.Controllers
{
    public class Test {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class TestAPIController : SheerAPIController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        [ActionName("GetSpecial")]
        public string GetSpecial(Guid Id) {
            if (Id.ToString().Equals("1833D84A-393E-4155-9390-A07862B00A3C")) 
            {

                return "some value";
            }

            return Id.ToString();
        }

        // POST api/<controller>
        public void Post([FromBody]Test value)
        {
            
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]Test value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}
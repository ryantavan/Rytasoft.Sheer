using Rytasoft.Sheer.API;
using Rytasoft.Sheer.Web.Data;
using Rytasoft.Sheer.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Rytasoft.Sheer.Web.Controllers
{

    public class TestAPIController : SheerAPIController
    {
        Repository repo = new Repository(); 
        // GET api/<controller>
        public IEnumerable<Student> Get()
        {
            return repo.GetStudents();
        }

        // GET api/<controller>/5
        public Student Get(int id)
        {
            return repo.GetStudent(id);
        }

        [ActionName("GetScore")]
        public int GetScore(int id) {
          return Get(id).Score;
        }

        [ActionName("SetScore")]
        public int SetScore(int id,int score)
        {
            return Get(id).Score;
        }


        // POST api/<controller>
        public int Post([FromBody]Student value)
        {
            return repo.AddStudent(value);
        }

        // PUT api/<controller>/5
        public void Put([FromBody]Student value)
        {
            repo.UpdateStudent(value);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            repo.DeleteStudent(id);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rytasoft.Sheer.API.Classes
{
    public class APIFunction
    {
        public string Name { get; set; }
        public List<APIParameter> Parameters { get; set; }
        public string ReturnType { get; set; }
    }
}

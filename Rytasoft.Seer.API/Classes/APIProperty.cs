using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rytasoft.Sheer.API.Classes
{
    public class APIProperty
    {
        public string Name { get; set; }
        public List<APIAttribute> Attributes { get; set; }
    }
}

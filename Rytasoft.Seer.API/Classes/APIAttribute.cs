using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rytasoft.Sheer.API.Classes
{
    public class APIAttribute
    {
        public string AttributeName { get; set; }
        public List<KeyValuePair<string, string>> AttributeProperties { get; set; }
    }
}

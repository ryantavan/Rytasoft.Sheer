using Rytasoft.Seer.API.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rytasoft.Seer.API.Classes
{
    public class APIParameter
    {
        public string ParameterName { get; set; }
        public string ParameterType { get; set; }
        public ParameterLevel Level { get; set; }
        public List<APIProperty> Properties { get; set; }
    }
}

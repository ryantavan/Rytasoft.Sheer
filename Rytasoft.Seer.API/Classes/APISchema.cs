﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rytasoft.Seer.API.Classes
{
    public class APISchema
    {
        public string Name { get; set; }
        public List<APIFunction> Functions { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceCompetition.Models
{
    public class Mine
    {
        public Stop Location { get; set; }
        public List<Resource> Resources { get; set; }
    }

    
}
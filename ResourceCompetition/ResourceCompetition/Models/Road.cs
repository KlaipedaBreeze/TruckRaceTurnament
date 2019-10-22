using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceCompetition.Models
{
    public class Road
    {
        public Stop FromStop { get; set; }
        public Stop ToStop { get; set; }
        public int Weight { get; set; }
    }
}
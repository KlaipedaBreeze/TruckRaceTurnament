using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceCompetition.Models.ViewModels
{
    public class RoadDTO
    {
        public int FromStopId { get; set; }
        public int ToStopId { get; set; }
        public double Weight { get; set; }
    }
}
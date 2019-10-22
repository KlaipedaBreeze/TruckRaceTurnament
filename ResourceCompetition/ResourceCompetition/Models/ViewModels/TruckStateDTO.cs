using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceCompetition.Models.ViewModels
{
    public class TruckStateDTO
    {
        public List<RoadDTO> InterconnectionRoads { get; set; }
        public int CurrentStopId { get; set; }
        public List<RoadDTO> UnhiddenMazeRoads { get; set; }
        public List<MinesDTO> Mines  { get; set; }
        public int Fuel { get; set; }
        public double DistanceToStart { get; set; }
        public List<ResourceDTO> Cargo { get; set; }
    }
}
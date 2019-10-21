using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceCompetition.Models.ViewModels
{
    public class MazeDTO
    {
        public MazeDTO(){}

        public List<Stop> StopsList { get; set; } = new List<Stop>();
        public List<Road> RoadsList { get; set; } = new List<Road>();
        public List<Stop> MineList { get; set; } = new List<Stop>();
        public Stop StartStop { get; set; }

    }
}
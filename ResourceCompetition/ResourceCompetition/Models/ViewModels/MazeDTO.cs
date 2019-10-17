using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceCompetition.Models.ViewModels
{
    public class MazeDTO
    {
        public MazeDTO(){}

        public MazeDTO(int StopId, int ToStopId, double Weight)
        {
            this.Weight = Weight;
            this.StopId = StopId;
            this.ToStopId = ToStopId;
        }
        //public MazeDTO(int StopId, int ToStopId, double Weight)
        //{
        //    this.Weight = Weight;
        //    this.StopId = StopId;
        //    this.ToStopId = ToStopId;
        //}

        public int StopId { get; set; }
        public int StopX { get; set; }
        public int StopY { get; set; }
        public int ToStopId { get; set; }
        public int ToStopX { get; set; }
        public int ToStopY { get; set; }
        public double Weight { get; set; }  
    }
}
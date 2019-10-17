using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceCompetition.Models
{
    public class Maze
    {
        public List<Stop> StopsList { get; set; } = new List<Stop>();
        public List<Road> RoadsList { get; set; } = new List<Road>();

        public Maze GenerateMaze()
        {
            //Rectagle size of maze
            int with = 15;
            int hight = 10;

            //reset ID of stop
            Stop.Reset();

            //creating all stops into maze
            for (int i = 0; i < with; i++)
            {
                for (int j = 0; j < hight; j++)
                {
                    var stop = new Stop()
                    {
                        CordY = j,
                        CordX = i,
                        Name = $"Stop-{i}-{j}"
                    };
                    StopsList.Add(stop);
                }
            }


            //Creating connection roads between neighbor stops
            //bidirectional connection
            var tmpList = new List<Stop>(StopsList);
            foreach (var fromStop in StopsList)
            {
                tmpList.Remove(fromStop);
                var neighbors = tmpList.Where(x => 
                    Math.Sqrt(Math.Pow(fromStop.CordX - x.CordX, 2) + Math.Pow(fromStop.CordY - x.CordY, 2))
                    <= 1);

                foreach (var toStop in neighbors)
                {
                    var dir1 = new Road()
                    {
                        FromStop = fromStop,
                        ToStop = toStop,
                        Weight = 1
                    };
                    var dir2 = new Road()
                    {
                        FromStop = toStop,
                        ToStop = fromStop,
                        Weight = 1
                    };
                    RoadsList.Add(dir1);
                    RoadsList.Add(dir2);
                }

            }




            return this;
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ResourceCompetition.Models
{
    public class Maze
    {
        public List<Stop> StopsList { get; set; } = new List<Stop>();
        public List<Road> RoadsList { get; set; } = new List<Road>();

        public Maze GenerateMaze(int with = 30, int height = 20)
        {
            //Rectagle size of maze
            //int with = 30;
            //int height = 20;

            //reset ID of stop
            Stop.Reset();

            //creating all stops into maze
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < with; i++)
                {
                    var stop = new Stop()
                    {
                        CordY = j,
                        CordX = i,
                        Name = $"Stop-{i}-{j}",
                        IsHidden = i > ((with/6) *6)
                    };
                    StopsList.Add(stop);
                }
            }


            //Creating connection roads between neighbor stops
            //bidirectional connection
            //var tmpList = new List<Stop>(StopsList);
            //foreach (var fromStop in StopsList)
            //{
            //    tmpList.Remove(fromStop);
            //    var neighbors = tmpList.Where(x => 
            //        Math.Sqrt(Math.Pow(fromStop.CordX - x.CordX, 2) + Math.Pow(fromStop.CordY - x.CordY, 2))
            //        <= 1);

            //    foreach (var toStop in neighbors)
            //    {
            //        var dir1 = new Road()
            //        {
            //            FromStop = fromStop,
            //            ToStop = toStop,
            //            Weight = 1
            //        };
            //        var dir2 = new Road()
            //        {
            //            FromStop = toStop,
            //            ToStop = fromStop,
            //            Weight = 1
            //        };
            //        RoadsList.Add(dir1);
            //        RoadsList.Add(dir2);
            //    }

            //}

            //Generating real maze
            var unvisitedStops = new List<Stop>(StopsList);

            //get start Stop
            var rnd = new Random();
            Stack myStack = new Stack();
            var current = unvisitedStops.SingleOrDefault(x => x.CordX == 0 && x.CordY == 0);
            if (current is null)
            {
                return this;
            }

            unvisitedStops.Remove(current);

            while (unvisitedStops.Any())
            {
                var neighbors = unvisitedStops.Where(x =>
                        Math.Sqrt(Math.Pow(current.CordX - x.CordX, 2) + Math.Pow(current.CordY - x.CordY, 2))
                        <= 1).ToList();
                if (!neighbors.Any())
                {
                    if (myStack.Count <= 0)
                    {
                        return this;
                    }


                    #region //connecting death end
                    var lastneighbors = StopsList.Where(x =>
                        Math.Abs(Math.Sqrt(Math.Pow(current.CordX - x.CordX, 2) + Math.Pow(current.CordY - x.CordY, 2)) - 1) < 0.0001
                        && !RoadsList.Any(s => s.ToStop.Id == current.Id && s.FromStop.Id == x.Id)
                        ).ToList();

                    if (lastneighbors.Any() && (lastneighbors.Count() > 2 || rnd.Next(101) < 20))
                    {
                        var randStop2 = rnd.Next(lastneighbors.Count());
                        InterConnectStops(current, lastneighbors[randStop2]);
                    }
                    #endregion

                    current = (Stop)myStack.Pop();
                    continue;
                }
                var randStop = rnd.Next(neighbors.Count());
                var nextStop = neighbors[randStop];
                InterConnectStops(current, nextStop);
                myStack.Push(current);
                current = nextStop;
                unvisitedStops.Remove(current);
            }

            return this;
        }

        private void InterConnectStops(Stop from, Stop to, bool twoWay = true)
        {
            RoadsList.Add(new Road()
            {
                FromStop = from,
                ToStop = to,
                Weight = 1
            });

            if (twoWay)
            {
                RoadsList.Add(new Road()
                {
                    FromStop = to,
                    ToStop = from,
                    Weight = 1
                });
            }

        }
    }
}
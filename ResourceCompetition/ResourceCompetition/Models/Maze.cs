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
        public List<Stop> MineLocations { get; set; } = new List<Stop>();
        public Stop StartStop { get; set; }
        public int intenconectionChance { get; set; } = 25;

        private Random _rnd = new Random();
        public Maze GenerateMaze(int with = 30, int height = 20)
        {
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
                        IsHidden = i > (with * 0.4)
                    };
                    StopsList.Add(stop);
                }
            }
            StartStop = StopsList[0];

            //Generating real maze
            #region
            var unvisitedStops = new List<Stop>(StopsList);

            //get start Stop
            
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

                    if (lastneighbors.Any() && (lastneighbors.Count() > 2 || _rnd.Next(101) < intenconectionChance))
                    {
                        var randStop2 = _rnd.Next(lastneighbors.Count());
                        InterConnectStops(current, lastneighbors[randStop2]);
                    }
                    #endregion

                    current = (Stop)myStack.Pop();
                    continue;
                }
                var randStop = _rnd.Next(neighbors.Count());
                var nextStop = neighbors[randStop];
                InterConnectStops(current, nextStop);
                myStack.Push(current);
                current = nextStop;
                unvisitedStops.Remove(current);
            }
            #endregion

            //genarating mines location
            for (int i = 0; i < 4; i++)
            {
                MineLocations.Add(StopsList[_rnd.Next(StopsList.Count-10*i)+10*i]);
            }

            return this;
        }

        private void InterConnectStops(Stop from, Stop to, bool twoWay = true)
        {
            RoadsList.Add(new Road()
            {
                FromStop = from,
                ToStop = to,
                Weight = _rnd.Next(3)+1
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
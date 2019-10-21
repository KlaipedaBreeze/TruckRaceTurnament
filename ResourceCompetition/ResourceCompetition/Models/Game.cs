using System;
using System.Collections.Generic;

namespace ResourceCompetition.Models
{
    public static class Game
    {
        private static Random rnd = new Random();
        public static List<Truck> Trucks { get; set; }
        public static Maze Maze { get; set; }
        public static bool IsInitiated { get; set; } = false;



        public static void Init()
        {
            Maze = new Maze().GenerateMaze();

            //creating trucks
            Trucks = new List<Truck>();
            Truck.Init();
            for (int i = 0; i < 5; i++)
            {
                Trucks.Add(
                    new Truck()
                    {
                        Id = i,
                        Token = i.ToString(),
                        Location = Maze.StartStop
                    }
                    );
            }


            //Gold resources for each Mine
            var goldIntial = new List<Resource>();
            for (int i = 0; i < 20; i++)
            {
                goldIntial.Add(
                    new Resource()
                    {
                        Type = ResourceType.Gold,
                        Value = rnd.Next(20) + 1,
                        Weight = rnd.Next(20) + 1
                    }
                    );
            }


            foreach (var truck in Trucks)
            {
                foreach (var minelocation in Maze.MineLocations)
                {
                    truck.Mines.Add(new Mine()
                    {
                        Location = minelocation,
                        Resources = new List<Resource>(goldIntial)
                    });

                }

            }

            IsInitiated = true;
        }
    }
}
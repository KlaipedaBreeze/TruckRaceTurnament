using ResourceCompetition.Models;
using ResourceCompetition.Models.ViewModels;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ResourceCompetition.Controllers
{
    public class TruckController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Move(string token, int toStopId)
        {
            var truck = Game.Trucks.SingleOrDefault(x => x.Token == token);
            if (truck == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new HttpError("No valid truck"));
            }

            //check if stops is interconnected
            var roadToGo =
                Game.Maze.RoadsList.SingleOrDefault(x => x.FromStop.Id == truck.Location.Id && x.ToStop.Id == toStopId);
            if (roadToGo == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new HttpError("No road"));
            }

            if (roadToGo.Weight > truck.Fuel)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new HttpError("No road"));
            }

            //move truck to new location
            truck.Fuel -= roadToGo.Weight;
            truck.Location = Game.Maze.StopsList.SingleOrDefault(x => x.Id == toStopId);
            return Request.CreateResponse(HttpStatusCode.OK, "Success");
        }

        [HttpGet]
        public HttpResponseMessage GetState(string token)
        {
            var truck = Game.Trucks.SingleOrDefault(x => x.Token == token);
            if (truck == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new HttpError("No valid truck"));
            }

            var state = new TruckStateDTO()
            {
                CurrentStopId = truck.Location.Id,
                InterconnectionRoads = Game.Maze.RoadsList.Where(x => x.FromStop.Id == truck.Location.Id)
                    .Select(s => new RoadDTO()
                    {
                        FromStopId = truck.Location.Id,
                        ToStopId = s.ToStop.Id,
                        Weight = s.Weight
                    })
                    .ToList(),
                Mines = truck.Mines.Select(c => new MinesDTO()
                {
                    Id = c.Id,
                    ResourcesLeft = c.Resources.Select(d => new ResourceDTO()
                    {
                        Id = d.Id,
                        Weight = d.Weight,
                        Value = d.Value,
                        Type = d.Type.ToString()
                    }).ToList(),
                    Distance = Math.Round(Math.Sqrt(Math.Pow(c.Location.CordX - truck.Location.CordX, 2) + Math.Pow(c.Location.CordY - truck.Location.CordY, 2)), 1)
                }).ToList(),
                UnhiddenMazeRoads = Game.Maze.RoadsList.Select(v => new RoadDTO()
                {
                    FromStopId = v.FromStop.Id,
                    ToStopId = v.ToStop.Id,
                    Weight = v.Weight
                }).ToList(),
                Fuel = truck.Fuel,
                Cargo = truck.Cargo.Select(cargo => new ResourceDTO()
                {
                    Id = cargo.Id,
                    Weight = cargo.Weight,
                    Value = cargo.Value,
                    Type = cargo.Type.ToString()
                }).ToList(),
                DistanceToStart = Math.Round(Math.Sqrt(Math.Pow(Game.Maze.StartStop.CordX - truck.Location.CordX, 2) + Math.Pow(Game.Maze.StartStop.CordY - truck.Location.CordY, 2)), 1)
            };


            return Request.CreateResponse(HttpStatusCode.OK, state);
        }


        [HttpGet]
        public HttpResponseMessage Load(string token, int resourceId)
        {
            var truck = Game.Trucks.SingleOrDefault(x => x.Token == token);
            if (truck == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new HttpError("No valid truck"));
            }

            var mine = truck.Mines.SingleOrDefault(x => x.Location.Id == truck.Location.Id);
            if (mine == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new HttpError("Truck not in Mine"));
            }

            var resource = mine.Resources.SingleOrDefault(x => x.Id == resourceId);
            if (resource == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new HttpError("No such resource in Mine"));
            }

            var cargoWeight = truck.Cargo.Sum(x => x.Weight);
            if (truck.MaxWeight - cargoWeight < resource.Weight)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new HttpError("Resource can't fit in thr Truck"));
            }

            mine.Resources.Remove(resource);
            truck.Cargo.Add(resource);
            return Request.CreateResponse(HttpStatusCode.OK, "Success");
        }

        [HttpGet]
        public HttpResponseMessage UnLoad(string token, int resourceId)
        {
            var truck = Game.Trucks.SingleOrDefault(x => x.Token == token);
            if (truck == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new HttpError("No valid truck"));
            }

            var resource = truck.Cargo.SingleOrDefault(x => x.Id == resourceId);
            if (resource == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new HttpError("No such resource in Truck"));
            }

            if (truck.Location.Id != Game.Maze.StartStop.Id)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new HttpError("Track not on the Start point"));
            }

            truck.Cargo.Remove(resource);
            truck.Score += resource.Value;

            //ToDo: check for the end

            return Request.CreateResponse(HttpStatusCode.OK, "Success");
        }

    }
}

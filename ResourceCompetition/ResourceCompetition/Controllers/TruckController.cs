using System;
using ResourceCompetition.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ResourceCompetition.Models.ViewModels;

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
            if (!Game.Maze.RoadsList.Exists(x => x.FromStop.Id == truck.Location.Id && x.ToStop.Id == toStopId))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new HttpError("No road"));
            }

            //move truck to new location
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
                InterconnectionRoads = Game.Maze.RoadsList.Where(x=>x.FromStop.Id == truck.Location.Id)
                    .Select(s=> new RoadDTO()
                    {
                        FromStopId = truck.Location.Id,
                        ToStopId = s.ToStop.Id,
                        Weight = s.Weight
                    })
                    .ToList(),
                Mines = truck.Mines.Select(c=> new MinesDTO()
                {
                    Id = c.Id,
                    ResourcesLeft = c.Resources.Select(d=> new ResourceDTO()
                    {
                        Id = d.Id,
                        Weight = d.Weight,
                        Value = d.Value,
                        Type = d.Type.ToString()
                    }).ToList(),
                    Distance = Math.Sqrt(Math.Pow(c.Location.CordX - truck.Location.CordX, 2) + Math.Pow(c.Location.CordY - truck.Location.CordY, 2))
                }).ToList(),
                UnhiddenMazeRoads = Game.Maze.RoadsList.Select(v=> new RoadDTO()
                {
                    FromStopId = v.FromStop.Id,
                    ToStopId = v.ToStop.Id,
                    Weight = v.Weight
                }).ToList()
            };


            return Request.CreateResponse(HttpStatusCode.OK, state);
        }



    }
}

using ResourceCompetition.Models;
using System;
using System.Collections.Generic;
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
            if (!Game.Maze.RoadsList.Exists(x=>x.FromStop.Id == truck.Location.Id && x.ToStop.Id == toStopId))
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

            return Request.CreateResponse(HttpStatusCode.OK, "Success");
        }



        }
}

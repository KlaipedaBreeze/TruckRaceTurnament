using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ResourceCompetition.Models;
using ResourceCompetition.Models.ViewModels;

namespace ResourceCompetition.Controllers
{
    public class GameWorldController : ApiController
    {
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        public IHttpActionResult GetMaze()
        {
            //var maze = new Maze().GenerateMaze();
            //var cons = maze.RoadsList.Select(x => new MazeDTO(x.FromStop.Id, x.ToStop.Id, x.Weight));
            //Game.Init();
            var maze = Game.Maze;
            var cons = new MazeDTO();
            if (maze == null) return Ok();
            cons.StopsList = maze?.StopsList.Where(x => !x.IsHidden).ToList();
            cons.RoadsList = maze?.RoadsList.Where(x => !x.FromStop.IsHidden).ToList();
            cons.MineList = maze?.MineLocations;
            cons.StartStop = maze?.StartStop;
            return Ok(cons);
        }

        public IHttpActionResult GetTrucks()
        {
            var trucks = Game.Trucks;

            return Ok(trucks);
        }

    }
}
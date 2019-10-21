using ResourceCompetition.Models;
using System.Web.Http;

namespace ResourceCompetition.Controllers
{
    public class GameControlController : ApiController
    {
        [HttpGet]
        public IHttpActionResult InitGame()
        {
            Game.Init();
            return Ok("OK");
        }

        public IHttpActionResult StartGame()
        {

            return Ok();
        }

        public IHttpActionResult StopGame()
        {

            return Ok();
        }

    }
}
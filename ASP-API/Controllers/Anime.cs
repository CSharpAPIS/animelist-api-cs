using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ASP_API.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Anime : ControllerBase
    {

        // GET: api/<Anime>
        [HttpGet]
        public IEnumerable<AnimeList> Get()
        {
            // string jsonString = new System.IO.StreamReader(@"E:\gits\ASP-API\ASP-API\data\AnimeList.json").ReadToEnd();
            var animeList = AnimeList.FromJson(Startup.jsonData);
            return animeList;
        }

        // GET api/<Anime>/5
        [HttpGet("{id}")]
        public dynamic Get(int id)
        {

            var animeList = AnimeList.FromJson(Startup.jsonData);
            foreach (var anime in animeList) {
                if ((anime.AnimeId) == id)
                {
                    return anime;
                }
            }
            return @"{'error': 'not found'}";
        }

        // POST api/<Anime>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Anime>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Anime>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShuffle.Data;
using MovieShuffle.Utilities;
using MovieShuffle.Utilities.Db.Providers;

namespace MovieShuffle.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private MovieDbProvider movieDbProvider;
        private TmdbRequestUtility tmdbRequestUtility;

        public MoviesController(MovieDbProvider movieDbProvider, TmdbRequestUtility tmdbRequestUtility)
        {
            this.movieDbProvider = movieDbProvider;
            this.tmdbRequestUtility = tmdbRequestUtility;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetMovies()
        {
            return Ok(movieDbProvider.Get());
        }

        [HttpGet]
        public ActionResult GetMovie([FromQuery]int id)
        {
            return Ok(movieDbProvider.GetBy(Tuple.Create("id", id)));
        }

        [HttpGet]
        public async Task<ActionResult> GetExternalMovies([FromQuery] string movieTitle)
        {
            if (movieTitle.Length < 3)
                return Ok();

            return Ok(await tmdbRequestUtility.SearchMovie(movieTitle));
        }

        [HttpPost]
        public ActionResult Sync([FromBody] Movie movie)
        {
            try
            {
                movieDbProvider.Update(movie);
                return Ok(movie);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }

    }
}
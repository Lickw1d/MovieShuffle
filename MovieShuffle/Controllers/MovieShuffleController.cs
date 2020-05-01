using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieShuffle.Data;
using MovieShuffle.Factories;
using MovieShuffle.Factories.Interfaces;
using MovieShuffle.Models;
using MovieShuffle.Utilities.Db.Providers;
using Newtonsoft.Json;

namespace MovieShuffle.Controllers
{
    public class MovieShuffleController : Controller
    {
        private RemainingMovieItemDbProvider dbProvider;
        private IGroupedRemainingMovieItemFactory groupedRemainingMovieItemFactory;
        public MovieShuffleController(RemainingMovieItemDbProvider dbProvider, IGroupedRemainingMovieItemFactory groupedRemainingMovieItemFactory)
        {
            this.dbProvider = dbProvider;
            this.groupedRemainingMovieItemFactory = groupedRemainingMovieItemFactory;
        }
        // GET: MovieShuffle
        public ActionResult Index()
        {        
            return View();
        }

        public ActionResult Get()
        {
            IEnumerable<RemainingMovieItem> movieItems = dbProvider.Get();
            IEnumerable<GroupedRemainingMovieItem> movieGroups = groupedRemainingMovieItemFactory.CreateList(movieItems);
            return Ok(JsonConvert.SerializeObject(movieGroups));
        }

        [HttpPost]
        public ActionResult SetNext([FromBody]GroupedRemainingMovieItem movieItem)
        {
            return Ok();
        }
    }
}
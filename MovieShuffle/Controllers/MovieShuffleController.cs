using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieShuffle.Data;
using MovieShuffle.Factories;
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
        // GET: MovieShuffle/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MovieShuffle/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MovieShuffle/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MovieShuffle/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MovieShuffle/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MovieShuffle/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MovieShuffle/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
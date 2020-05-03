using System;
using System.Collections;
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
        private RemainingMovieItemDbProvider remainingMovieDbProvider;
        private SelectedQuestionDbProvider selectedQuestionDbProvider;
        private IGroupedRemainingMovieItemFactory groupedRemainingMovieItemFactory;
        public MovieShuffleController(
            RemainingMovieItemDbProvider dbProvider, 
            IGroupedRemainingMovieItemFactory groupedRemainingMovieItemFactory,
            SelectedQuestionDbProvider selectedQuestionDbProvider)
        {
            this.remainingMovieDbProvider = dbProvider;
            this.groupedRemainingMovieItemFactory = groupedRemainingMovieItemFactory;
            this.selectedQuestionDbProvider = selectedQuestionDbProvider;
        }
        // GET: MovieShuffle
        public ActionResult Index()
        {        
            return View();
        }

        public ActionResult Get()
        {
            IEnumerable<RemainingMovieItem> movieItems = remainingMovieDbProvider.Get();
            IEnumerable<GroupedRemainingMovieItem> movieGroups = groupedRemainingMovieItemFactory.CreateList(movieItems);
            return Ok(JsonConvert.SerializeObject(movieGroups));
        }

        [HttpPost]
        public ActionResult SetNext([FromBody]GroupedRemainingMovieItem movieItem)
        {
            //TODO: Could be made more efficient with long standing sql transactions / batch inserts
            IEnumerable<SelectedQuestion> prevSelectedQuestions= selectedQuestionDbProvider.GetBy(Tuple.Create("watching", true));

                foreach (var selectedQuestion in prevSelectedQuestions)
                {
                        selectedQuestion.Watching = false;
                        selectedQuestionDbProvider.Update(selectedQuestion);
                }

                IEnumerable<SelectedQuestion> newSelectedQuestions =
                    groupedRemainingMovieItemFactory.ToSelectedQuestion(movieItem);

                foreach (var selectedQuestion in newSelectedQuestions)
                {
                    selectedQuestion.Watching = true;
                    selectedQuestionDbProvider.Insert(selectedQuestion);
                }


                return Ok();
        }
    }
}
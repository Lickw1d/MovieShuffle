using MovieShuffle.Data;
using MovieShuffle.Factories.Interfaces;
using MovieShuffle.Models;
using MovieShuffle.Utilities.Db.Providers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MovieShuffle.Factories
{
    public class GroupedRemainingMovieItemFactory : IGroupedRemainingMovieItemFactory
    {
        private QuestionDbProvider questionDbProvider;
        private IConfiguration configuration;
        public GroupedRemainingMovieItemFactory(QuestionDbProvider questionDbProvider, IConfiguration configuration)
        {
            this.questionDbProvider = questionDbProvider;
            this.configuration = configuration;
        }

        public GroupedRemainingMovieItem Create(IList<RemainingMovieItem> movieItems, Question question)
        {
            if (movieItems.Count == 0)
                return new GroupedRemainingMovieItem();

            //TODO: Check if all are same question. log if not 
            var item =  new GroupedRemainingMovieItem()
            {
                Question = question,
                QuestionResponses = movieItems.OrderBy(mi => mi.UserName).ToList(),
                Watched = movieItems.Any(mi => mi.Watched),
                Watching = movieItems.Any(mi=>mi.Watching)
            };

            return item;

        }

        public IEnumerable<GroupedRemainingMovieItem> CreateList(IEnumerable<RemainingMovieItem> movieItems)
        {
            var questionDictionary = new Dictionary<int, Question>();

            foreach (Question question in questionDbProvider.Get())
            {
                questionDictionary.Add(question.Id,question);
            }

            questionDbProvider.Get();
            IEnumerable<IGrouping<int,RemainingMovieItem>> movieItemGroups = movieItems.GroupBy(mi => mi.QuestionResponse.QuestionId);
            var results = new List<GroupedRemainingMovieItem>();


                foreach (var movieItemGroup in movieItemGroups)
                {
                    Question question = questionDictionary.ContainsKey(movieItemGroup.First().QuestionResponse.QuestionId) ? 
                        questionDictionary[movieItemGroup.First().QuestionResponse.QuestionId] 
                        : 
                        null;

                    results.Add(Create(movieItemGroup.ToList(), question));
                }

                return results;
        }

        public IEnumerable<SelectedQuestion> ToSelectedQuestion(GroupedRemainingMovieItem groupedRemainingMovieItem)
        {
            var responses = new List<SelectedQuestion>();

            groupedRemainingMovieItem.QuestionResponses.ForEach(qr =>
            {
                responses.Add(new SelectedQuestion()
                {
                    Questionid = groupedRemainingMovieItem.Question.Id,
                    Questionresponseid = qr.QuestionResponse.Id,
                    Watching = false
                });

            });

            return responses;
        }
    }
}

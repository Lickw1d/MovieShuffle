﻿using MovieShuffle.Data;
using MovieShuffle.Factories.Interfaces;
using MovieShuffle.Models;
using MovieShuffle.Utilities.Db.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShuffle.Factories
{
    public class GroupedRemainingMovieItemFactory : IGroupedRemainingMovieItemFactory
    {
        private QuestionDbProvider questionDbProvider;

        public GroupedRemainingMovieItemFactory(QuestionDbProvider questionDbProvider)
        {
            this.questionDbProvider = questionDbProvider;
        }
        public GroupedRemainingMovieItem Create(IList<RemainingMovieItem> movieItems)
        {
            if (movieItems.Count == 0)
                return new GroupedRemainingMovieItem();

            //TODO: Check if all are same question. log if not 
            return new GroupedRemainingMovieItem()
            {
                Question = questionDbProvider.GetBy(Tuple.Create("id", movieItems[0].QuestionResponse.QuestionId), "usp_Question_GetBy").FirstOrDefault(),
                QuestionResponses = movieItems.OrderBy(mi => mi.UserName).ToList(),
                Watched = movieItems.Any(mi => mi.Watched),
                Watching = movieItems.Any(mi=>mi.Watching)
            };

        }

        public IEnumerable<GroupedRemainingMovieItem> CreateList(IEnumerable<RemainingMovieItem> movieItems)
        {
            IEnumerable<IGrouping<int,RemainingMovieItem>> movieItemGroups = movieItems.GroupBy(mi => mi.QuestionResponse.QuestionId);
            return movieItemGroups.Select(mig =>Create(mig.ToList()));
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

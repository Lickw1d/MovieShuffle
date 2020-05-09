using MovieShuffle.Data;
using MovieShuffle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShuffle.Factories.Interfaces
{
    public interface IGroupedRemainingMovieItemFactory
    {
        public GroupedRemainingMovieItem Create(IList<RemainingMovieItem> movieItems, Question question);
        public IEnumerable<GroupedRemainingMovieItem> CreateList(IEnumerable<RemainingMovieItem> items);
        public IEnumerable<SelectedQuestion> ToSelectedQuestion(GroupedRemainingMovieItem groupedRemainingMovieItem);
    }
}

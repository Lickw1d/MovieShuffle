using MovieShuffle.Data;
using MovieShuffle.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShuffle.Models
{
    public class MovieShuffleViewModel
    {
        public IEnumerable<GroupedRemainingMovieItem> RemainingMovieGroups { get; set; }
        public MovieShuffleViewModel(IEnumerable<GroupedRemainingMovieItem> remainingMovieGroups)
        {
            this.RemainingMovieGroups = remainingMovieGroups;
        }

        
    }
}

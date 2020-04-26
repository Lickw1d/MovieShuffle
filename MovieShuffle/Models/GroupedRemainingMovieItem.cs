using MovieShuffle.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShuffle.Models
{
    public class GroupedRemainingMovieItem
    {
        public Question Question;
        public IList<RemainingMovieItem> UserResponses;
        public bool Watched;
    }
}

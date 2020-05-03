using MovieShuffle.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShuffle.Models
{
    public class GroupedRemainingMovieItem
    {
        public Question Question { get; set; }
        public List<RemainingMovieItem> QuestionResponses { get; set; }
        public bool Watched { get; set; }
        public bool Watching { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShuffle.Data
{
    public class RemainingMovieItem
    {
        public QuestionResponse QuestionResponse { get; set; }
        public string UserName { get; set; }
        public bool Watched { get; set; }
        public bool Watching { get; set; }
    }
}

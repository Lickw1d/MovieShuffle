using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShuffle.Data
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string PosterUrl { get; set; }

        public DateTime CreatedTimeStamp { get; set; }

        public DateTime UpdatedTimeStamp { get; set; }

        public long? TmdbId { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public string Description { get; set; }

    }

}

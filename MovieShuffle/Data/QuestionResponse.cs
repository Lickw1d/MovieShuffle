using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShuffle.Data
{
    public class QuestionResponse
    {
        public int Id { get; set; }

        public int QuestionId { get; set; }

        public int UserId { get; set; }

        public Movie Movie { get; set; }

        public DateTime CreatedTimestamp { get; set; }

    }

}

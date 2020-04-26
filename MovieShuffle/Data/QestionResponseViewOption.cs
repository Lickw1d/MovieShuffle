using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShuffle.Data
{
    public class QuestionResponseViewOption
    {
        public int Id { get; set; }

        public int? QuestionResponseId { get; set; }

        public int? ViewOptionId { get; set; }

        public DateTime CreatedTimestamp { get; set; }

    }
}

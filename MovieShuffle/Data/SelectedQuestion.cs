using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShuffle.Data
{
    public class SelectedQuestion
    {
        public int Id { get; set; }

        public int? Questionid { get; set; }

        public int? Questionresponseid { get; set; }

        public DateTime CreatedTimeStamp { get; set; }

        public DateTime UpdatedTimeStamp { get; set; }
        public bool? Watching { get; set; }

    }

}

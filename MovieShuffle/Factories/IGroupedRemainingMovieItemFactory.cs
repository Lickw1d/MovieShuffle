﻿using MovieShuffle.Data;
using MovieShuffle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShuffle.Factories
{
    public interface IGroupedRemainingMovieItemFactory
    {
        public GroupedRemainingMovieItem Create(IList<RemainingMovieItem> movieItems);
        public IEnumerable<GroupedRemainingMovieItem> CreateList(IEnumerable<RemainingMovieItem> items);
    }
}

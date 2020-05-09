using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace MovieShuffle.Utilities
{
    public class TmdbRequestUtility
    {
        private string apiKey { get; set; }
        private string language { get; set; }

        public TmdbRequestUtility(IConfiguration configuration)
        {
            apiKey = configuration.GetValue<string>("ExternalApiKeys:tmdb");
            language = "en-US";
        }

        public async Task<string> SearchMovie(string movieTitle)
        {
            var getUrl = $"https://api.themoviedb.org/3/search/movie?api_key={apiKey}&language={language}&query={movieTitle}&page=1&include_adult=false";
            var client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(getUrl);

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return await response.Content.ReadAsStringAsync();
        }
    }
}

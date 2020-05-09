using Microsoft.Extensions.Configuration;
using MovieShuffle.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using MovieShuffle.Utilities.Db.Providers.AbstractClasses;

namespace MovieShuffle.Utilities.Db.Providers
{
    public class QuestionResponseDbProvider : ADbProvider<QuestionResponse>
    {
        private MovieDbProvider movieDbProvider;
        public QuestionResponseDbProvider(IConfiguration configuration, MovieDbProvider movieDbProvider) : base(configuration)
        {
            getByProc = "usp_QuestionResponse_GetBy";
            dbObjectName = "QuestionResponse";
            this.movieDbProvider = movieDbProvider;
        }

        public override QuestionResponse GetFromDataRow(DataRow row, Dictionary<string, Dictionary<string,string>> overrides)
        {
            if (overrides.Count == 0)
            {
                overrides.Add("movie", new Dictionary<string,string>()
                {
                    { "id", "MovieId"},
                    {"CreatedTimeStamp", "MovieCreatedTimeStamp"},
                });

            }

            return new QuestionResponse()
            {
                Id = row.FieldOverride<int>("Id", overrides.GetDictionaryValue(dbObjectName)),
                QuestionId = row.FieldOverride<int>("QuestionId", overrides.GetDictionaryValue(dbObjectName)),
                UserId = row.FieldOverride<int>("UserId", overrides.GetDictionaryValue(dbObjectName)),
                Movie = movieDbProvider.GetFromDataRow(row,overrides),
                CreatedTimestamp = row.FieldOverride<DateTime>("CreatedTimeStamp", overrides.GetDictionaryValue(dbObjectName)),
                
            };
        }
    }
}

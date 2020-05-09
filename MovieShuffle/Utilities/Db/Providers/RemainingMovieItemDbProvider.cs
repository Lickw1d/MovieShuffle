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
    public class RemainingMovieItemDbProvider : ADbProvider<RemainingMovieItem>
    {
        private QuestionResponseDbProvider questionResponseDbProvider;

        public RemainingMovieItemDbProvider(
            QuestionResponseDbProvider questionResponseDbProvider,
            IConfiguration configuration
            ) : base(configuration)
        {
            getByProc = "usp_GetRemainingMovieItems";
            dbObjectName = "remainingMovieItem";
            this.questionResponseDbProvider = questionResponseDbProvider;
        }

        public override RemainingMovieItem GetFromDataRow(DataRow row, Dictionary<string, Dictionary<string,string>> overrides)
        {
            if (overrides.Count == 0)
            {
                overrides.AddLowerCase("Movie", new Dictionary<string, string>()
                {
                    {"id", "MovieId"}
                });
                overrides.AddLowerCase("QuestionResponse", new Dictionary<string, string>()
                {
                    {"id", "QuestionResponseId"},
                    {"CreatedTimeStamp", "QuestionResponseCreatedTimestamp"}
                });
            }

            return new RemainingMovieItem()
            {
                QuestionResponse = questionResponseDbProvider.GetFromDataRow(row, overrides),
                UserName = row.FieldOverride<string>("UserName", overrides.GetDictionaryValue(dbObjectName)),
                Watched = row.FieldOverride<bool>("Watched", overrides.GetDictionaryValue(dbObjectName)),
                Watching = row.FieldOverride<bool>("Watching",overrides.GetDictionaryValue(dbObjectName))
            };
        }
    }
}

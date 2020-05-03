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
            this.questionResponseDbProvider = questionResponseDbProvider;
        }

        public override RemainingMovieItem GetFromDataRow(DataRow row, Dictionary<string, string> overrides)
        {
            //since proc returns 
            Dictionary<string, string> subOverrides = new Dictionary<string, string>();
            subOverrides.AddLowerCase("id", "QuestionResponseId");
            subOverrides.AddLowerCase("CreatedTimeStamp", "QuestionResponseCreatedTimestamp");
            var questionResponse = questionResponseDbProvider.GetFromDataRow(row, subOverrides);

            return new RemainingMovieItem()
            {
                QuestionResponse = questionResponse,
                UserName = row.FieldOverride<string>("UserName", overrides),
                Watched = row.FieldOverride<bool>("Watched", overrides),
                Watching = row.FieldOverride<bool>("Watching",overrides)
            };
        }
    }
}

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
        public QuestionResponseDbProvider(IConfiguration configuration) : base(configuration)
        {
            getByProc = "usp_QuestionResponse_GetBy";
        }

        public override QuestionResponse GetFromDataRow(DataRow row, Dictionary<string, string> overrides)
        {
            return new QuestionResponse()
            {
                Id = row.FieldOverride<int>("Id", overrides),
                QuestionId = row.FieldOverride<int>("QuestionId", overrides),
                UserId = row.FieldOverride<int>("UserId", overrides),
                Response = row.FieldOverride<string>("Response", overrides),
                CreatedTimestamp = row.FieldOverride<DateTime>("CreatedTimeStamp", overrides)
            };
        }
    }
}

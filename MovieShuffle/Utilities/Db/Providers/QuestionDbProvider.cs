using Microsoft.Extensions.Configuration;
using MovieShuffle.Data;
using MovieShuffle.Utilities.Db.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using MovieShuffle.Utilities.Db.Providers.AbstractClasses;

namespace MovieShuffle.Utilities.Db.Providers
{
    public class QuestionDbProvider : ADbProvider<Question>
    {
        public QuestionDbProvider(IConfiguration configuration) : base(configuration)
        {
            getByProc = "usp_Question_GetBy";
            dbObjectName = "Question";
        }

        public override Question GetFromDataRow(DataRow row, Dictionary<string, Dictionary<string,string>> overrides)
        {
            return new Question()
            {
                Id = row.FieldOverride<int>("Id", overrides.GetDictionaryValue(dbObjectName)),
                Text = row.FieldOverride<string>("Text", overrides.GetDictionaryValue(dbObjectName)),
                Ordinal = row.FieldOverride<int>("Ordinal", overrides.GetDictionaryValue(dbObjectName)),
                CreatedTimestamp = row.FieldOverride<DateTime>("CreatedTimeStamp", overrides.GetDictionaryValue(dbObjectName))
            };
        }
    }
}

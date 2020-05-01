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
            this.questionResponseDbProvider = questionResponseDbProvider;
        }

        public override IEnumerable<RemainingMovieItem> GetBy<T>(IList<Tuple<string, T>> fields)
        {
            var proc = "usp_GetRemainingMovieItems";

            using (SqlConnection conn = new SqlConnection(configuration.GetConnectionString("movieShuffle")))
            {
                using (SqlCommand comm = new SqlCommand(proc, conn))
                {
                    comm.CommandType = CommandType.StoredProcedure;
                    fields.ToList().ForEach(f => comm.Parameters.Add(f.Item1, f.Item2.GetSqlType()).Value = f.Item2);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(comm))
                    {
                        DataTable t = new DataTable();
                        conn.Open();
                        adapter.Fill(t);
                        conn.Close();

                       return t.AsEnumerable().Select(r => GetFromDataRow(r));
                    }
                }
            }
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
                Watched = row.FieldOverride<bool>("Watched", overrides)
            };
        }
    }
}

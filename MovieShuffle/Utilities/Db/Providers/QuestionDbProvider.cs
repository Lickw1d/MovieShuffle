using Microsoft.Extensions.Configuration;
using MovieShuffle.Data;
using MovieShuffle.Utilities.Db.AbstractClasses;
using MovieShuffle.Utilities.Db.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShuffle.Utilities.Db.Providers
{
    public class QuestionDbProvider : ADbProvider<Question>
    {
        public QuestionDbProvider(IConfiguration configuration) : base(configuration) { }

        public override IEnumerable<Question> GetBy<T2>(IList<Tuple<string, T2>> fields)
        {
            var proc = "usp_Question_GetBy";

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
        public override Question GetFromDataRow(DataRow row, Dictionary<string, string> overrides)
        {
            return new Question()
            {
                Id = row.FieldOverride<int>("Id", overrides),
                Text = row.FieldOverride<string>("Text", overrides),
                Ordinal = row.FieldOverride<int>("Ordinal", overrides),
                CreatedTimestamp = row.FieldOverride<DateTime>("CreatedTimeStamp", overrides)
            };
        }
    }
}

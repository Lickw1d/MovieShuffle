using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MovieShuffle.Data;
using MovieShuffle.Utilities.Db.Providers.AbstractClasses;

namespace MovieShuffle.Utilities.Db.Providers
{
    public class MovieDbProvider : ADbProvider<Movie>
    {
        public MovieDbProvider(IConfiguration configuration) : base(configuration)
        {
            getByProc = "usp_Movie_GetBy";
            updateProc = "usp_Movie_Update";
            dbObjectName = "Movie";
        }

        public override bool Update(Movie obj, string procName)
        {
            using (SqlConnection conn = new SqlConnection(configuration.GetConnectionString("movieShuffle")))
            {
                using (SqlCommand comm = new SqlCommand(procName, conn))
                {
                    comm.CommandType = CommandType.StoredProcedure;

                    comm.Parameters.Add("id", SqlDbType.Int).Value = obj.Id;
                    comm.Parameters.Add("Title", SqlDbType.VarChar).Value = obj.Title;
                    comm.Parameters.AddNullable("PosterUrl", SqlDbType.VarChar, obj.PosterUrl);
                    comm.Parameters.AddNullable("TmdbId", SqlDbType.BigInt, obj.TmdbId);
                    comm.Parameters.AddNullable("ReleaseDate", SqlDbType.DateTime, obj.ReleaseDate);
                    comm.Parameters.AddNullable("Description",SqlDbType.VarChar, obj.Description);


                    conn.Open();

                    var wasUpdated = comm.ExecuteNonQuery() > 0;

                    conn.Close();

                    return wasUpdated;
                }
            }
        }

        public override Movie GetFromDataRow(DataRow row, Dictionary<string, Dictionary<string,string>> overrides)
        {
            return new Movie()
            {
                Id = row.FieldOverride<int>("id", overrides.GetDictionaryValue(dbObjectName)),
                Title = row.FieldOverride<string>("Title", overrides.GetDictionaryValue(dbObjectName)),
                PosterUrl = row.FieldOverride<string>("PosterUrl", overrides.GetDictionaryValue(dbObjectName)),
                CreatedTimeStamp = row.FieldOverride<DateTime>("CreatedTimeStamp", overrides.GetDictionaryValue(dbObjectName)),
                UpdatedTimeStamp = row.FieldOverride<DateTime>("UpdatedTimeStamp", overrides.GetDictionaryValue(dbObjectName)),
                TmdbId = row.FieldOverride<long?>("TmdbId", overrides.GetDictionaryValue(dbObjectName)),
                ReleaseDate = row.FieldOverride<DateTime?>("ReleaseDate",overrides.GetDictionaryValue(dbObjectName)),
                Description = row.FieldOverride<string>("Description",overrides.GetDictionaryValue(dbObjectName))
            };
        }
    }
}

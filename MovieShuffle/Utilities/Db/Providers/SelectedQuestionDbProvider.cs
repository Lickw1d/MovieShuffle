using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using MovieShuffle.Data;
using MovieShuffle.Utilities.Db.Providers.AbstractClasses;

namespace MovieShuffle.Utilities.Db.Providers
{
    public class SelectedQuestionDbProvider : ADbProvider<SelectedQuestion>
    {
        public SelectedQuestionDbProvider(IConfiguration configuration) : base(configuration)
        {
            insertProc = "usp_SelectedQuestion_Insert";
            getByProc = "usp_SelectedQuestion_GetBy";
            updateProc = "usp_SelectedQuestion_Update";
            dbObjectName = "SelectedQuestion";
        }

        public override bool Insert(SelectedQuestion obj, string procName)
        {
            using (SqlConnection conn = new SqlConnection(configuration.GetConnectionString("movieShuffle")))
            {
                using (SqlCommand comm = new SqlCommand(procName, conn))
                {
                    comm.CommandType = CommandType.StoredProcedure;

                    comm.Parameters.Add("QuestionId", SqlDbType.Int).Value = obj.Questionid;
                    comm.Parameters.Add("QuestionResponseId", SqlDbType.Int).Value = obj.Questionresponseid;
                    comm.Parameters.Add("Watching", SqlDbType.Bit).Value = obj.Watching;


                    conn.Open();

                    var wasInserted = comm.ExecuteNonQuery() > 0;

                    conn.Close();

                    return wasInserted;
                }
            }
        }

        public override bool Update(SelectedQuestion obj, string procName)
        {
            using (SqlConnection conn = new SqlConnection(configuration.GetConnectionString("movieShuffle")))
            {
                using (SqlCommand comm = new SqlCommand(procName, conn))
                {
                    comm.CommandType = CommandType.StoredProcedure;

                    comm.Parameters.Add("id", SqlDbType.Int).Value = obj.Id;
                    comm.Parameters.Add("QuestionId", SqlDbType.Int).Value = obj.Questionid;
                    comm.Parameters.Add("QuestionResponseId", SqlDbType.Int).Value = obj.Questionresponseid;
                    comm.Parameters.Add("Watching", SqlDbType.Bit).Value = obj.Watching;

                    conn.Open();

                    var wasUpdated  = comm.ExecuteNonQuery() > 0;

                    conn.Close();

                    return wasUpdated;
                }
            }
        }

        public override SelectedQuestion GetFromDataRow(DataRow row, Dictionary<string, Dictionary<string,string>> overrides)
        {
            return new SelectedQuestion()
            {
                Id = row.FieldOverride<int>("id", overrides.GetDictionaryValue(dbObjectName)),
                Questionid = row.FieldOverride<int>("QuestionId", overrides.GetDictionaryValue(dbObjectName)),
                Questionresponseid = row.FieldOverride<int>("questionResponseId", overrides.GetDictionaryValue(dbObjectName)),
                CreatedTimeStamp = row.FieldOverride<DateTime>("CreatedTimeStamp", overrides.GetDictionaryValue(dbObjectName)),
                Watching = row.FieldOverride<bool?>("Watching", overrides.GetDictionaryValue(dbObjectName)),
                UpdatedTimeStamp = row.FieldOverride<DateTime>("UpdatedTimeStamp", overrides.GetDictionaryValue(dbObjectName))
            };
        }
    }
}

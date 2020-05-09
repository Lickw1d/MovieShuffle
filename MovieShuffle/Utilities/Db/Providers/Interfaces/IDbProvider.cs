using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShuffle.Utilities.Db.Providers.Interfaces
{
    interface IDbProvider<T>
    {
        IEnumerable<T> Get();
        IEnumerable<T> Get(string procName, SqlConnection conn);
        IEnumerable<T> GetBy<T2>(Tuple<string, T2> field, SqlConnection conn);
        IEnumerable<T> GetBy<T2>(Tuple<string,T2> field, string procName, SqlConnection conn);
        IEnumerable<T> GetBy<T2>(IList<Tuple<string, T2>> fields, string procName, SqlConnection conn);
        T GetFromDataRow(DataRow row);
        T GetFromDataRow(DataRow row, Dictionary<string, Dictionary<string,string>> overrides);
        bool Insert(T obj);
        bool Update(T obj);

    }
}

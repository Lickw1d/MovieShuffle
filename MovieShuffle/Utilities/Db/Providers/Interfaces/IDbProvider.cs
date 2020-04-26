using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShuffle.Utilities.Db.Providers.Interfaces
{
    interface IDbProvider<T>
    {
        IEnumerable<T> Get();
        IEnumerable<T> GetBy<T2>(Tuple<string,T2> field);
        IEnumerable<T> GetBy<T2>(IList<Tuple<string, T2>> fields);
        T GetFromDataRow(DataRow row);
        T GetFromDataRow(DataRow row, Dictionary<string, string> overrides);
    }
}

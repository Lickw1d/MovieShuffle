using Microsoft.Extensions.Configuration;
using MovieShuffle.Utilities.Db.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShuffle.Utilities.Db.AbstractClasses
{
    public abstract class ADbProvider<T> : IDbProvider<T>
    {
        protected IConfiguration configuration;
        public ADbProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IEnumerable<T> Get()
        {
            return GetBy(new Tuple<string,T>[0]);
        }
        public IEnumerable<T> GetBy<T2>(Tuple<string, T2> field)
        {
            return GetBy(new Tuple<string, T2>[] { field });
        }

        public virtual IEnumerable<T> GetBy<T2>(IList<Tuple<string, T2>> fields)
        {
            throw new NotImplementedException();
        }

        public T GetFromDataRow(DataRow row)
        {
            return GetFromDataRow(row, new Dictionary<string, string>());
        }

        public virtual T GetFromDataRow(DataRow row, Dictionary<string, string> overrides)
        {
            return default;
        }

    }
}

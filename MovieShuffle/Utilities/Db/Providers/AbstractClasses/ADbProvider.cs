using Microsoft.Extensions.Configuration;
using MovieShuffle.Utilities.Db.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShuffle.Utilities.Db.Providers.AbstractClasses
{
    public abstract class ADbProvider<T> : IDbProvider<T>
    {
        protected IConfiguration configuration;
        protected string insertProc { get; set; }
        protected string updateProc { get; set; }
        protected string getByProc { get; set; }
        protected string dbObjectName { get; set; }

        public ADbProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IEnumerable<T> Get()
        {
            return Get(getByProc,null);
        }

        public IEnumerable<T> Get(string procName, SqlConnection conn = null)
        {
            var isExternal = conn != null;
            conn ??= new SqlConnection(configuration.GetConnectionString("movieShuffle"));

            if(!isExternal) conn.Open();

            IEnumerable<T> resultObjects = GetBy(new Tuple<string, T>[0], procName, conn);

            if(!isExternal) conn.Dispose();

            return resultObjects;
        }

        public IEnumerable<T> GetBy<T2>(Tuple<string, T2> field, SqlConnection conn = null)
        {
            var isExternal = conn != null;
            conn ??= new SqlConnection(configuration.GetConnectionString("movieShuffle"));

            if (!isExternal) conn.Open();

            IEnumerable<T> resultObjects = GetBy<T2>(field, getByProc, conn);

            if (!isExternal) conn.Dispose();

            return resultObjects;
        }

        public IEnumerable<T> GetBy<T2>(Tuple<string, T2> field, string procName, SqlConnection conn = null)
        {
            var isExternal = conn != null;
            conn ??= new SqlConnection(configuration.GetConnectionString("movieShuffle"));

            if (!isExternal) conn.Open();

            IEnumerable<T> resultObjects = GetBy(new[] { field }, procName, conn);

            if (!isExternal) conn.Dispose();

            return resultObjects;

        }

        public IEnumerable<T> GetBy<T2>(IList<Tuple<string, T2>> fields, string procName, SqlConnection conn)
        {
            if(string.IsNullOrEmpty(procName))
                throw new Exception("db procedure not set");
            
            using (SqlCommand comm = new SqlCommand(procName, conn))
            {
                    comm.CommandType = CommandType.StoredProcedure;
                    fields.ToList().ForEach(f => comm.Parameters.Add(f.Item1, f.Item2.GetSqlType()).Value = f.Item2);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(comm))
                    {
                        DataTable t = new DataTable();

                        adapter.Fill(t);

                        return t.AsEnumerable().Select(GetFromDataRow);
                    }
            }
        }

        public T GetFromDataRow(DataRow row)
        {
            return GetFromDataRow(row, new Dictionary<string, Dictionary<string,string>>());
        }

        /// <summary>
        /// Gets object from DataRow.  Can be called from higher nested context
        /// </summary>
        /// <param name="row">DataRow</param>
        /// <param name="overrides"> Dictionary of table alias overrides</param>
        /// <returns></returns>
        public virtual T GetFromDataRow(DataRow row, Dictionary<string, Dictionary<string,string>> overrides)
        {
            return default;
        }

        public virtual bool Insert(T obj)
        {
            return Insert(obj, insertProc);
        }

        public virtual bool Insert(T obj, string procName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// used to bulk insert into database
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public bool Insert(IEnumerable<T> objList)
        {
            return Insert(objList, insertProc);
        }

        public virtual bool Insert(IEnumerable<T> objList, string procName)
        {
            throw new NotImplementedException();
        }

        public bool Update(T obj)
        {
            return Update(obj, updateProc);
        }

        public virtual bool Update(T obj, string procName)
        {
            throw new NotImplementedException();
        }

    }
}

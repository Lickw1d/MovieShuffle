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
        public ADbProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IEnumerable<T> Get()
        {
            return Get(getByProc);
        }

        public IEnumerable<T> Get(string procName)
        {
            return GetBy(new Tuple<string,T>[0], procName);
        }

        public IEnumerable<T> GetBy<T2>(Tuple<string, T2> field)
        {
            return GetBy<T2>(field, getByProc);
        }

        public IEnumerable<T> GetBy<T2>(Tuple<string, T2> field, string procName)
        {
            return GetBy(new[] { field }, procName);
        }

        public IEnumerable<T> GetBy<T2>(IList<Tuple<string, T2>> fields)
        {
            return GetBy(fields, getByProc);
        }

        public IEnumerable<T> GetBy<T2>(IList<Tuple<string, T2>> fields, string procName)
        {
            if(string.IsNullOrEmpty(procName))
                throw new Exception("db procedure not set");

            using (SqlConnection conn = new SqlConnection(configuration.GetConnectionString("movieShuffle")))
            {
                using (SqlCommand comm = new SqlCommand(procName, conn))
                {
                    comm.CommandType = CommandType.StoredProcedure;
                    fields.ToList().ForEach(f => comm.Parameters.Add(f.Item1, f.Item2.GetSqlType()).Value = f.Item2);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(comm))
                    {
                        DataTable t = new DataTable();
                        conn.Open();
                        adapter.Fill(t);
                        conn.Close();

                        return t.AsEnumerable().Select(GetFromDataRow);
                    }
                }
            }
        }

        public T GetFromDataRow(DataRow row)
        {
            return GetFromDataRow(row, new Dictionary<string, string>());
        }

        public virtual T GetFromDataRow(DataRow row, Dictionary<string, string> overrides)
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

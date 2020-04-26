using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShuffle.Utilities
{
    public static class SqlUtility
    {
        public static Dictionary<Type, SqlDbType> SqlTypeConvertDictionary;

        static SqlUtility(){
            SqlTypeConvertDictionary = new Dictionary<Type, SqlDbType>();

            SqlTypeConvertDictionary.Add(typeof(int), SqlDbType.Int);
            SqlTypeConvertDictionary.Add(typeof(int?), SqlDbType.Int);
            SqlTypeConvertDictionary.Add(typeof(string), SqlDbType.VarChar);
            SqlTypeConvertDictionary.Add(typeof(DateTime), SqlDbType.DateTime);
            SqlTypeConvertDictionary.Add(typeof(DateTime?), SqlDbType.DateTime);
            SqlTypeConvertDictionary.Add(typeof(bool), SqlDbType.Bit);
            SqlTypeConvertDictionary.Add(typeof(bool?), SqlDbType.Bit);
            SqlTypeConvertDictionary.Add(typeof(char), SqlDbType.VarChar);
            SqlTypeConvertDictionary.Add(typeof(char?), SqlDbType.VarChar);
            SqlTypeConvertDictionary.Add(typeof(DataTable), SqlDbType.Structured);
        }
        public static T GetValueOrDefault<T>(this DataRow row, string field)
        {
            if (!row.Table.Columns.Contains(field))
                return default;

            return row.Field<T>(field);
        }

        public static SqlDbType GetSqlType<T>(this T value)
        {
            if (SqlTypeConvertDictionary.ContainsKey(value.GetType()))
                return SqlTypeConvertDictionary[value.GetType()];

            return SqlDbType.VarChar;
        }
    }
}

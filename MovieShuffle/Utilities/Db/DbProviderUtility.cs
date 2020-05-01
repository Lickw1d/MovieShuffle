using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShuffle.Utilities.Db
{
    public static class DbProviderUtility
    {

        public static T FieldOverride<T>(
            this DataRow row, 
            string fieldName, 
            Dictionary<string, string> overrides
            )
        {
            string keyVal = overrides.ContainsKey(fieldName.ToLower()) ? overrides[fieldName.ToLower()] : fieldName;
                
            
            return row.Table.Columns.Contains(keyVal) ?  row.Field<T>(keyVal) : default;


        }
    }
}

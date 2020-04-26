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
                return row.Field<T>(
                    overrides.ContainsKey(fieldName.ToLower()) ?
                    overrides[fieldName.ToLower()] :
                    fieldName
                    );


        }
    }
}

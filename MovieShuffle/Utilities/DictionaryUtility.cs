using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace MovieShuffle.Utilities
{
    public static class DictionaryUtility
    {
        /// <summary>
        /// Add string key to dictionary as all lower case
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void AddLowerCase<T>(this Dictionary<string, T> dictionary, string key, T value) {
            dictionary.Add(key.ToLower(), value);
        }

        public static Dictionary<T1, T2> GetDictionaryValue<T1,T2>(this Dictionary<string, Dictionary<T1, T2>> parentDictionary,string key)
        {

            if (parentDictionary.ContainsKey(key.ToLower()))
                return parentDictionary[key.ToLower()];
            

            return new Dictionary<T1, T2>();
        }
    }
}

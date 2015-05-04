using System.Collections.Generic;

public static partial class MZ {

    public class Dictionaries {

        public static void SetValueToDict<T>(Dictionary<string, object> dict, string key, object value) {
            if (dict == null) {
                dict = new Dictionary<string, object>();
            }

            if (value == null) {
                value = Values.DefaultValue<T>();
            }

            if (dict.ContainsKey(key)) {
                dict[key] = (T)value;
            } else {
                dict.Add(key, (T)value);
            }
        }

        public static T SetDefaultValueInDictIfNotContainKey<T>(Dictionary<string, object> dict, string key) {
            return Dictionaries.ValueFromDictWithKey<T>(dict, key);
        }

        public static T ValueFromDictWithKey<T>(Dictionary<string, object> dict, string key) {
            if (!dict.ContainsKey(key)) {
				SetValueToDict<T>(dict, key, Values.DefaultValue<T>());
            }
        
            return (T)Values.ValueFormObject<T>(dict[key]);
        }

        public static object ValueFromDictToDict(Dictionary<string, object> fromDict, string fromKey, 
                                             Dictionary<string, object> destDict, string destKey) {
            MZ.Debugs.AssertIfNullWithMessage(fromDict, "from dict can't be null");

            object value = (fromDict.ContainsKey(fromKey))? fromDict[fromKey] : new object();
            SetValueToDict<object>(destDict, destKey, value);

            return value;
        }

        public static object ValueFromDictToDict(Dictionary<string, object> fromDict, Dictionary<string, object> destDict, string key) {
            return ValueFromDictToDict(fromDict, key, destDict, key);
        }

        public static T ValueFromDictToDict<T>(Dictionary<string, object> fromDict, string fromKey, 
                                           Dictionary<string, object> destDict, string destKey) {
            T value = (fromDict.ContainsKey(fromKey))? Values.ValueFormObject<T>(fromDict[fromKey]) : 
                                                   Values.DefaultValue<T>();
            SetValueToDict<object>(destDict, destKey, value);

            return value;
        }

        public static T ValueFromDictToDict<T>(Dictionary<string, object> fromDict, Dictionary<string, object> destDict, string key) {
            return ValueFromDictToDict<T>(fromDict, key, destDict, key);
        }
    }

    // want to replace Dictionary<string, object> ... :(
    public class MZDictionary : Dictionary<string, object> {
    }

}
using System;
using System.Collections.Generic;

public static partial class MZ {

    public class Dictionaries {
    
//        public static T ValueFromDictWithKey<T>(Dictionary<string, object> dict, string key) {
//			if (!dict.ContainsKey(key)) SetValueToDict<T>(dict, key, Values.DefaultValue<T>());
//        
//            return (T)Values.ValueFormObject<T>(dict[key]);
//        }

		// keep, but rename ... test me
        public static object ValueFromDictToDict(Dictionary<string, object> fromDict, string fromKey, 
	                                             Dictionary<string, object> destDict, string destKey) {
            MZ.Debugs.AssertIfNullWithMessage(fromDict, "from dict can't be null");

            object value = (fromDict.ContainsKey(fromKey))? fromDict[fromKey] : new object(); 
			destDict[destKey] = value;
            
            return value;
        }

//		 keep, but rename
        public static object ValueFromDictToDict(Dictionary<string, object> fromDict, Dictionary<string, object> destDict, string key) {
            return ValueFromDictToDict(fromDict, key, destDict, key);
        }

		// keep, but rename ... test me 
        public static T ValueFromDictToDict<T>(
        	Dictionary<string, object> fromDict, string fromKey, 
			Dictionary<string, object> destDict, string destKey
		) {
			destDict[destKey] = fromDict.AutoGetValue<string, object>(fromKey);
			return (T)destDict[destKey];
        }

        public static T ValueFromDictToDict<T>(Dictionary<string, object> fromDict, Dictionary<string, object> destDict, string key) {
            return ValueFromDictToDict<T>(fromDict, key, destDict, key);
        }
    }
}

public static class MZDictionaryAutoValueExtensions {

	public static V AutoSetValue<K, V>(this Dictionary<K, V> dict, K key, V value) {
		V _value = value;
		if (_value == null) _value = MZ.Values.DefaultValue<V>();
		
		dict[key] = _value;
		
		return _value;
	}
	
	public static V AutoGetValue<K, V>(this Dictionary<K, V> dict, K key) {
		if (!dict.ContainsKey(key)) dict[key] = MZ.Values.DefaultValue<V>();
		return dict[key];
	}
	
	public static V AutoSetValueForKey<K, V>(this Dictionary<K, V> dict, K key, Func<V> defaultValueFunc = null) {
		if (!dict.ContainsKey(key)) {
			if (defaultValueFunc != null) dict[key] = defaultValueFunc();
			else dict[key] = MZ.Values.DefaultValue<V>();
		}
		return dict[key];
	}
}
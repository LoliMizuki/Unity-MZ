using System;
using System.Collections.Generic;

public static partial class MZ {

    public class Dictionaries {
    
    	// ** need view and rename below **
	
        public static object ValueFromDictToDict(Dictionary<string, object> fromDict, string fromKey, 
	                                             Dictionary<string, object> destDict, string destKey) {
            MZ.Debugs.AssertIfNullWithMessage(fromDict, "from dict can't be null");

            object value = (fromDict.ContainsKey(fromKey))? fromDict[fromKey] : new object(); 
			destDict[destKey] = value;
            
            return value;
        }

        public static object ValueFromDictToDict(Dictionary<string, object> fromDict, Dictionary<string, object> destDict, string key) {
            return ValueFromDictToDict(fromDict, key, destDict, key);
        }

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
        
        // ** end **
        
        
    }
}

public static class MZDictionaryCommonExtensions {

	public static List<string> KeysList<TKey, TValue>(this Dictionary<TKey, TValue> dict) {
		var keyNames = new List<string>();
		foreach (var k in dict.Keys) keyNames.Add(k.ToString());
	
		return keyNames;
	}
	
	public static string KeyNamesString<TKey, TValue>(this Dictionary<TKey, TValue> dict) {
		string allKeys = "";
		foreach (var key in dict.Keys) allKeys += key.ToString() + ", ";
		
		return MZ.Collections.ForamtListString(allKeys, "[", "]");
	}
	
	public static void LogAllKeyNames<TKey, TValue>(this Dictionary<TKey, TValue> dict) {
		MZ.Debugs.Log(dict.KeyNamesString());
	}
	
	public static string KeyValuesString<TKey, TValue>(this Dictionary<TKey, TValue> dict) {
		string allKVStr = "";
		foreach (KeyValuePair<TKey, TValue> keyValue in dict) {
			allKVStr += keyValue.Key.ToString() + ": " + keyValue.Value.ToString() + ", ";
		}
		
		return MZ.Collections.ForamtListString(allKVStr, "[", "]");
	}
	
	public static void LogAllKeyValues<TKey, TValue>(this Dictionary<TKey, TValue> dict) {
		MZ.Debugs.Log(dict.KeyValuesString());
	}
}

/// <summary>
/// if dictionary not contains target key or values, auto set to default into dictionary and return it.
/// </summary>
public static class MZDictionaryAutoValueExtensions {

	public static TValue AutoSetValue<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value) {
		TValue _value = value;
		if (_value == null) _value = MZ.Values.DefaultValue<TValue>();
		
		dict[key] = _value;
		
		return _value;
	}
	
	public static TValue AutoGetValue<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key) {
		if (!dict.ContainsKey(key)) dict[key] = MZ.Values.DefaultValue<TValue>();
		return dict[key];
	}
		
	public static TValue AutoSetValueForKey<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, Func<object> defaultValueFunc = null) {
		if (!dict.ContainsKey(key)) {
			if (defaultValueFunc != null) dict[key] = (TValue)defaultValueFunc();
			else dict[key] = MZ.Values.DefaultValue<TValue>();
		}
		return dict[key];
	}
}
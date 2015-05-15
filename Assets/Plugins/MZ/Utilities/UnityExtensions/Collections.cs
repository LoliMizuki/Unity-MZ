using System.Collections.Generic;

public static partial class MZ {

    public class Collections {

        public static List<string> KeysListFromDict<K, V>(Dictionary<K, V> dict) {
            List<string> keys = new List<string>();
            foreach (K k in dict.Keys) keys.Add(k.ToString());
            
            return keys;
        }

        public static void PrintKeysInDict<K,V>(Dictionary<K,V> dict) {
            MZ.Debugs.Log(KeysStringInDict(dict));
        }

        public static void PrintKeysInDictWithMessage<K,V>(string message, Dictionary<K,V> dict) {
            MZ.Debugs.Log(message + ": " + KeysStringInDict(dict));
        }

        public static string KeysStringInDict<K, V>(Dictionary<K, V> dict) {
            string allKeys = "";
            foreach (K key in dict.Keys) allKeys += key.ToString() + ", ";
           
            return ForamtListString(allKeys, "[", "]");
        }

        public static void PrintKeysAndValuesInDict<K,V>(Dictionary<K,V> dict) {
            string allKVStr = "";
            foreach (K key in dict.Keys) allKVStr += key.ToString() + ": " + dict[key].ToString() + ", ";
            
            MZ.Debugs.Log(ForamtListString(allKVStr, "[", "]"));
        }

        public static void DictSetKeyValue(Dictionary<string, object> dict, string key, object value) {
            if (dict == null) dict = new Dictionary<string, object>();

            if (!dict.ContainsKey(key)) {
                dict.Add(key, value);
                return;
            }

            dict[key] = value;
        }

        public static void PrintElemnetsInList<T>(List<T> list) {
            MZ.Debugs.Log(ElemnetsStringInList<T>(list));
        }

        public static string ElemnetsStringInList<T>(List<T> list) { 
            string s = "";
            foreach (T e in list) s += e.ToString() + ",";

            return ForamtListString(s, "[", "]");
        }
        
        

        static string ForamtListString(string listString, string leftCloseSymbol, string rightCloseSymbol) {
            int lastIndexOfComma = (listString.LastIndexOf(",") >= 0)? listString.LastIndexOf(",") : 0;
            return leftCloseSymbol + listString.Substring(0, lastIndexOfComma) + rightCloseSymbol;
        }
    }
}
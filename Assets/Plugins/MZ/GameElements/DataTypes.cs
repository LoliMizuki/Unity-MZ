using UnityEngine;
using System.Collections.Generic;

public static partial class MZ {

	public class LevelRateInt {
	
		public int baseValue = 0;
		
		public int ValueWithLevelRate(int level, float rate) {
			return (int)(baseValue + baseValue * ((level - 1) * rate));
		}
		
		public void AddToDict(Dictionary<string, object> dict, string key) {
			dict.Add(key, baseValue);
		}
		
		public void FromDict(Dictionary<string, object> dict, string key) {	
			baseValue = MZ.Dictionaries.ValueFromDictWithKey<int>(dict, key);
		}
	}

	public class Tuple<T> {
    	public T v1;
		public T v2;
        
        public string key = "";
        
        public string v1Prefix = "";
        
        public string v2Prefix = "";
        
        
        public Tuple(T v1, T v2) {
            Set(v1, v2);
        }
        
        public void Set(T v1, T v2) {
            this.v1 = v1;
            this.v2 = v2;
        }
        
        public void AddToDict(Dictionary<string, object> dict, string key = null, string v1Prefix = null, string v2Prefix = null) {
            string _key = ( key != null )? key : this.key;
            string _v1Prefix = ( v1Prefix != null )? v1Prefix : this.v1Prefix;
            string _v2Prefix = ( v2Prefix != null )? v2Prefix : this.v2Prefix;
        
            MZ.Dictionaries.SetValueToDict<T>(dict, _v1Prefix + _key, v1);
            MZ.Dictionaries.SetValueToDict<T>(dict, _v2Prefix + _key, v2);
        }
        
        public void FromDict(Dictionary<string, object> dict, string key = null, string v1Prefix = null, string v2Prefix = null) {
            string _key = ( key != null )? key : this.key;
            string _v1Prefix = ( v1Prefix != null )? v1Prefix : this.v1Prefix;
            string _v2Prefix = ( v2Prefix != null )? v2Prefix : this.v2Prefix;
        
            v1 = MZ.Dictionaries.ValueFromDictWithKey<T>(dict, _v1Prefix + _key);
            v2 = MZ.Dictionaries.ValueFromDictWithKey<T>(dict, _v2Prefix + _key);
        }
	}
}
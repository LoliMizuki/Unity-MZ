using UnityEngine;
using System;
using System.Collections.Generic;

static public partial class MZ {

	public class MinMaxInt {
	
		static public int IntValueFromMinMaxAndLevel(int min, int max, int currLevel, int maxLevel) {
			MZ.Debugs.Assert(min <= max, "max{0} can not less than min{1}", max, min);
		
			if(currLevel == 0 || maxLevel == 0) return 0;
			
			if(currLevel == 1 || maxLevel <= 1) return min;
			
			if(currLevel >= maxLevel) return max;
		
			float interval = IntervalFromMinMaxAndMaxLevel(min, max, maxLevel);
			return min + (int)(interval*(currLevel - 1));
		}
		
		static public float IntervalFromMinMaxAndMaxLevel(int min, int max, int maxLevel) {
			return (float)(max - min)/(maxLevel - 1);
		}
	
		public int min = 1;
		
		public int max = 1;
		
		public MinMaxInt() {
			SetValue(1, 1);
		}
        
        public MinMaxInt(int min, int max) {
        	SetValue(min, max);
        }
        
        public void SetValue(int min, int max) {
			this.min = min;
			this.max = max;
        }
		
		public int ValueWithLevel(int level, int maxLevel) {
			return IntValueFromMinMaxAndLevel(min, max, level, maxLevel);
		}
        
        public void AddToDict(Dictionary<string, object> dict, string key) {
            MZ.Dictionaries.SetValueToDict<int>(dict, "min-" + key, min);
            MZ.Dictionaries.SetValueToDict<int>(dict, "max-" + key, max);
        }
        
		public void FromDict(Dictionary<string, object> dict, string key) {
			min = MZ.Dictionaries.ValueFromDictWithKey<int>(dict, "min-" + key);
			max = MZ.Dictionaries.ValueFromDictWithKey<int>(dict, "max-" + key);
		}
	}
}
using System;
using System.Collections.Generic;

public static partial class MZ {

    public class MapReduce {
    
		public static void MapToList<T>(List<T> list, Action<T> action) {
            foreach (T t in list) action(t);
        }
        
		public static void MapToListWithIndex<T>(List<T> list, Action<int, T> action) {
			for (var index = 0; index < list.Count; index++) action(index, list[index]);
		}
        
        public static void MapToArray<T>(T[] array, Action<T> action) {
			for (int i = 0; i < array.Length; i++) action(array[i]);
        }
        
		public static void MapToDictionary<K, T>(Dictionary<K, T> dict, Action<T> action) {
			foreach (KeyValuePair<K, T> kv in dict) action(kv.Value);
		}

		public static void MapToLists<T1, T2>(List<T1> list1, List<T2> list2, Action<T1, T2> action) {
            int minCount = (list1.Count < list2.Count)? list1.Count : list2.Count;
            for (int i = 0; i < minCount; i++) action(list1[i], list2[i]);
        }
    }
}
using System;
using System.Collections.Generic;

static public partial class MZ {

    public class MapReduce {
    
        static public void Map<T>(List<T> list, Action<T> action) {
            foreach (T t in list) {
                action(t);
            }
        }

        // TODO: improve me!!!
        static public void Map<T1, T2>(List<T1> list1, List<T2> list2, Action<T1,T2> action) {
            int minCount = (list1.Count < list2.Count)? list1.Count : list2.Count;
            for (int i = 0; i < minCount; i++) {
                action(list1[i], list2[i]);
            }
        }

        static public void MapWithTimes<T>(List<T> list, int times, Action<T> action) {
            for (int i = 0; i < times; i++) {
                action(list[i]);
            }
        }
    }
}
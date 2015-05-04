using UnityEngine;
using System.Collections.Generic;

public static partial class MZ {

    public class Verifiers {

        public static bool String(string s) {
            return (s != null && s.Length > 0);
        }

        public static bool List<T>(List<T> l) {
            return (l != null && l.Count > 0);
        }

        public static bool Dictionary<K, V>(Dictionary<K, V> dict) {
            return (dict != null && dict.Count > 0);
        }
    }
}
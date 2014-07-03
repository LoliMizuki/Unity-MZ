using UnityEngine;
using System.Collections.Generic;

static public partial class MZ {

    public class Verifiers {

        static public bool String(string s) {
            return (s != null && s.Length > 0);
        }

        static public bool List<T>(List<T> l) {
            return (l != null && l.Count > 0);
        }

        static public bool Dictionary<K, V>(Dictionary<K, V> dict) {
            return (dict != null && dict.Count > 0);
        }
    }
}
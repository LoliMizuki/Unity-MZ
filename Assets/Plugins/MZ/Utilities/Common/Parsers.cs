using UnityEngine;
using System;
using System.Collections.Generic;

public static partial class MZ {

	public static class Parsers {
	
		public static List<T> ValuesListFromString<T>(string s, Func<string, T> parseFunc, char sliptter = ',') {
			List<T> list = new List<T>();
			
			string[] terms = s.Split(sliptter);
			
			foreach (string t in terms) {
				string _t = t.Trim().Trim(new char[]{ '\n', '\r' });
				if (_t == "") continue;
				
				list.Add(parseFunc(_t));
			}
			
			return list;
		}
	}
}
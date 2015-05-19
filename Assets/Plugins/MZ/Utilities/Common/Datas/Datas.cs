using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using MiniJSON;

public static partial class MZ {

	public static partial class Datas {
		
		public interface IDictionarySerializable {
			Dictionary<string, object> ToDictionary();
			void FromDictionary(Dictionary<string, object> dict);
		}
		
		public static List<T> TypeListFromObject<T>(object objectsList) {
			List<object> objsList = (List<object>)objectsList;
			
			List<T> list = new List<T>();
			foreach (object obj in objsList) list.Add((T)obj);
			
			return list;
		}
	}
}

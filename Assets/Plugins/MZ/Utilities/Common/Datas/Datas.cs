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
		
		public static class Converts {
			
			public static Dictionary<Type, Func<Type, object, object>> valueFromTypedObjectDict = new Dictionary<Type, Func<Type, object, object>> {
				{ typeof(System.Int32), (type, rawObj)  => int.Parse(rawObj.ToString()) },
				{ typeof(System.Single), (type, rawObj) => float.Parse(rawObj.ToString()) },
				
				{ typeof(UnityEngine.Vector2), (type, rawObj) => MZ.Vectors.Vector2FromString(rawObj.ToString()) },
				{ typeof(UnityEngine.Vector3), (type, rawObj) => MZ.Vectors.Vector3FromString(rawObj.ToString()) },
				
				{ typeof(UnityEngine.Color), (type, rawObj) => MZ.Colors.ColorFromString(rawObj.ToString()) },
				{ typeof(UnityEngine.Rect),  (type, rawObj) => MZ.Rects.RectFromString(rawObj.ToString()) },
			};
			
			public static Dictionary<Type, Func<Type, object, object>> objectFromTypedValueDict = new Dictionary<Type, Func<Type, object, object>> {
				{ typeof(UnityEngine.Color), (type, rawObj) => rawObj.ToString() },
				{ typeof(UnityEngine.Rect), (type, objValue) => objValue.ToString() },
			};
			
			public static bool HasValueFromTypedObjectFunc(Type type) {
				return valueFromTypedObjectDict.ContainsKey(type);
			}
			
			public static bool HasObjectFromTypedValueFunc(Type type) {
				return objectFromTypedValueDict.ContainsKey(type);
			}
			
			public static object ValueFromTypedObject(Type type, object rawObject) {
				return (valueFromTypedObjectDict.ContainsKey(type))? valueFromTypedObjectDict[type](type, rawObject) : null;
			}
			
			public static object ObjectFromTypedValue(Type type, object rawObject) {
				return (objectFromTypedValueDict.ContainsKey(type))? objectFromTypedValueDict[type](type, rawObject) : null;
			}
		}
	}
}
using UnityEngine;
using System.Collections;

public static partial class MZ {

	public class Components {
	
		public static T Apply<T>(GameObject gameObject) where T : Component {
			var comp = gameObject.GetComponent<T>();
			if (gameObject.GetComponent<T>() == null) comp = gameObject.AddComponent<T>();
			
			return comp;
		}
	}
}
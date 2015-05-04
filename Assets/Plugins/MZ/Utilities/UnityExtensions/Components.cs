using UnityEngine;
using System.Collections;

public static partial class MZ {

	public class Components {
	
		public static T Apply<T>(GameObject gameObject) where T : Component {
			if(gameObject.GetComponent<T>() == null) {
				gameObject.AddComponent<T>();
			}
	
			return gameObject.GetComponent<T>();
		}
	}
}
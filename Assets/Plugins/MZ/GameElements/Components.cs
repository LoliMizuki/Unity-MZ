using UnityEngine;
using System.Collections;

static public partial class MZ {

	public class Components {
	
		static public T Apply<T>(GameObject gameObject) where T : Component {
			if(gameObject.GetComponent<T>() == null) {
				gameObject.AddComponent<T>();
			}
	
			return gameObject.GetComponent<T>();
		}
	}
}
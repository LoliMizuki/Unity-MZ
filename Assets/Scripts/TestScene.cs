using UnityEngine;
using System.Collections.Generic;

public class TestScene : MonoBehaviour {

	void Awake() {
		var d = new Dictionary<string, Vector2>();
		
//		d.AutoSetValueForKey<string, Vector2>("up");
		d.AutoSetValueForKey<string, Vector2>("up"); // old c# issue?

//		
		Debug.Log(d["up"]);
		
		Debug.Log(d.KeysList()); // why ... this can do it?
		
		var vvv = d.AutoGetValue("down");
		
		Debug.Log(vvv);
		
//		d.AutoSetValueForKey("right");
		d.AutoSetValueForKey("right");
		
		
		Debug.Log(d["right"]);
	}
}

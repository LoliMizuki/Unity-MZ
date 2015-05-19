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
		
		var parent = new GameObject("p");
		for (var i = 0; i < 10; i++) {
			var c = new GameObject("c");
			c.transform.parent = parent.transform;
		}
		
		_p = parent;
		
		AddSprite();
	}
	
	GameObject _p;
	
	void AddSprite() {		
		var tex = Resources.Load<Texture2D>("pot/sev0001a");
		if (tex == null) { Debug.Log("texture is null"); return; }
		
		MZ.Transforms.MapToChildren(
			_p.transform,
			new System.Action<Transform>((t) => {
				var go = t.gameObject;
				if (go == null) { Debug.Log("null"); return; }
				
				var sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), MZ.Vectors.HALF_OF_ONE_V2, 100);
				if (sprite == null) { Debug.Log("sprite is null"); return; }
			
				go.AddComponent<SpriteRenderer>().sprite = sprite;
				go.AddComponent<BoxCollider2D>();
				go.AddComponent<Rigidbody2D>();
				go.GetComponent<Rigidbody2D>().gravityScale = 0;
			})
		);		
	}
}

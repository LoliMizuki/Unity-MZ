using UnityEngine;
using System.Collections;

public static partial class MZ {

	public static class Physics2D {
	
		public static BoxCollider2D SetBoxColliderToSpriteObject(GameObject gameObj) {
			BoxCollider2D collider = MZ.Components.Apply<BoxCollider2D>(gameObj);
		
			SpriteRenderer sr = gameObj.GetComponent<SpriteRenderer>();
			if (sr == null) return collider;
			
			Vector2 spriteSize = new Vector2(sr.sprite.texture.width, sr.sprite.texture.height);
			collider.size = spriteSize;
			
			return collider;
		}
	}
}
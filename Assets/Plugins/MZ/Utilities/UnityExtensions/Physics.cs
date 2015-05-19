using UnityEngine;
using System.Collections;

public static partial class MZ {

	public static class Physics {
	
		public static BoxCollider2D SetBoxCollider2DFitSpriteToObject(GameObject gameObject) {
			var collider = MZ.Components.Apply<BoxCollider2D>(gameObject);
		
			var spriteRender = gameObject.GetComponent<SpriteRenderer>();
			if (spriteRender == null) return collider;
			
			var spriteSize = new Vector2(spriteRender.sprite.texture.width, spriteRender.sprite.texture.height);
			collider.size = spriteSize;
			
			return collider;
		}
	}
}
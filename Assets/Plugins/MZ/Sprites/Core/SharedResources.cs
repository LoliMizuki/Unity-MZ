using UnityEngine;
using System.Collections;

public static partial class MZ {
	
	public static partial class Sprites {

		public class SharedResources {
	
			public static SharedResources instance {
				get {
					if(_instance == null) {
						_instance = new SharedResources();
					}
	
					return _instance;
				}
			}
	
			public Material additiveMaterial {
				get {
					if(_sharedAdditiveMaterial == null) {
						_sharedAdditiveMaterial = new Material(Shader.Find("Sprites/Additive"));
					}
	
					return _sharedAdditiveMaterial;
				}
			}
			Material _sharedAdditiveMaterial;
	
			#region - private
	
			static SharedResources _instance = null;
	
			private SharedResources() {
				_sharedAdditiveMaterial = new Material(Shader.Find("Sprites/Additive"));
			}
	
			#endregion
		}
	}
}
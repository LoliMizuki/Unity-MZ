using UnityEngine;
using System.Collections;

static public partial class MZ {
	
	static public partial class Sprites {

		public class SharedResources {
	
			static public SharedResources instance {
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
using UnityEngine;
using System;
using System.Collections;

static public partial class MZ {

	static public partial class Sprites {

		static public string SPRITESHEETS_DIRECTORY_IN_RESOURCES = "Atlases";
		
		static public string ANIMATIONS_DIRECTORY_IN_RESOURCES = "Animations";

		static public string SPRITESHEET_EXT_NAME = "txt";
		
		static public string ANIMATION_EXT_NAME = "json";

		static public float PIXEL_TO_UNIT = 1;

		public enum ShaderMode {
			Default,
			Additive
		}
	}
}
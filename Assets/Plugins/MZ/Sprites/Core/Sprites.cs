using UnityEngine;
using System;
using System.Collections;

public static partial class MZ {

	public static partial class Sprites {

		public static string SPRITESHEETS_DIRECTORY_IN_RESOURCES = "Atlases";
		
		public static string ANIMATIONS_DIRECTORY_IN_RESOURCES = "Animations";

		public static string SPRITESHEET_EXT_NAME = "txt";
		
		public static string ANIMATION_EXT_NAME = "json";

		public static float PIXEL_TO_UNIT = 1;

		public enum ShaderMode {
			Default,
			Additive
		}
	}
}
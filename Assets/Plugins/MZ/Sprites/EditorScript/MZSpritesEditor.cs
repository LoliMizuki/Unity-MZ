// TODO: apply new MZProjecter struct

using System;
using System.Collections;

public static partial class MZ {
	
	public static partial class Sprites {

		public static class Editor {
		
			public static string RESOURCES_DIRECTORY_FULL_PATH = Environment.CurrentDirectory + "/Assets/Resources";
			
			public static string SPRITESHEETS_DIRECTORY_FULL_PATH = 
				RESOURCES_DIRECTORY_FULL_PATH + "/" + MZ.Sprites.SPRITESHEETS_DIRECTORY_IN_RESOURCES;
				
			public static string ANIMATIONS_DIRECTORY_FULL_PATH = 
				RESOURCES_DIRECTORY_FULL_PATH + "/" + MZ.Sprites.ANIMATIONS_DIRECTORY_IN_RESOURCES;
			
			public static string PLUGIN_TEMP_FULL_PATH = Environment.CurrentDirectory + "/Assets/Temp/mzsktemp";
		}
	}
}
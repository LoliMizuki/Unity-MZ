// TODO: apply new MZProjecter struct

using System;
using System.Collections;

static public partial class MZ {
	
	static public partial class Sprites {

		static public class Editor {
		
			static public string RESOURCES_DIRECTORY_FULL_PATH = Environment.CurrentDirectory + "/Assets/Resources";
			
			static public string SPRITESHEETS_DIRECTORY_FULL_PATH = 
				RESOURCES_DIRECTORY_FULL_PATH + "/" + MZ.Sprites.SPRITESHEETS_DIRECTORY_IN_RESOURCES;
				
			static public string ANIMATIONS_DIRECTORY_FULL_PATH = 
				RESOURCES_DIRECTORY_FULL_PATH + "/" + MZ.Sprites.ANIMATIONS_DIRECTORY_IN_RESOURCES;
			
			static public string PLUGIN_TEMP_FULL_PATH = Environment.CurrentDirectory + "/Assets/Temp/mzsktemp";
		}
	}
}
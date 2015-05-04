using UnityEngine;
using System;
using System.Collections;

public static partial class MZ {

	public static class Paths {
	
		#if UNITY_EDITOR
        public static string FULL_ASSETS_FOLDER_PATH = Environment.CurrentDirectory + "/Assets";
        
		public static string FULL_RESOURCES_FOLDER_PATH = Environment.CurrentDirectory + "/Assets/Resources";
		#endif
	}
}
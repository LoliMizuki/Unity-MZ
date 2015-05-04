using UnityEngine;
using System.IO;
using System.Collections;

public static partial class MZ {

	public static class Files {
		
		public static string ReadTextsWithPath(string path) {
			if (!File.Exists(path)) { return null;}
			return File.ReadAllText(path);
		}
		
		public static void WriteTextsToPath(string path, string contents) {
			if (!File.Exists(path)) File.Create(path).Close();
			File.WriteAllText(path, contents);
		}
	}
}
using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using MiniJSON;

public class MZAnimationsFileContent {

	public List<string> spritesheetNames {
		get {
			return _spritesheetNames;
		}
	}
	public List<Dictionary<string, object>> animationSets {
		get {
			return _animationSets;
		}
	}

	static public MZAnimationsFileContent ProjectContentFromPath(string path) {
		Func<string, Dictionary<string, object>> loadProjectDictFromPath = (string p) => {
			if(IsPathValid(p) == false) {
				return null;
			}

			string jsonContent = File.ReadAllText(p);
			if(jsonContent == null || jsonContent.Length == 0){
				return null;
			}
			
			Dictionary<string, object> pd = (Dictionary<string, object>)Json.Deserialize(jsonContent);
			MZ.Debugs.AssertIfNullWithMessage(pd, "can not load project at path: " + p);
			
			return pd;
		};
		
		MZAnimationsFileContent projectContent = new MZAnimationsFileContent();
		Dictionary<string, object> projectDict = loadProjectDictFromPath(path);
		if(projectDict != null) {
			projectContent._spritesheetNames = SpriteNamesListFromObject(projectDict["spritesheets"]);
			projectContent._animationSets = AnimationSetsDictionaryFromObject(projectDict["animations"]);
		}

		return projectContent;
	}
	
	#region - private

	List<string> _spritesheetNames;
	List<Dictionary<string, object>> _animationSets;

	static bool IsPathValid(string path) {
		return (path != null && path.Length > 0);
	}

	static List<string> SpriteNamesListFromObject(object obj) {
		List<object> spritesheetNames = obj as List<object>;
		
		List<string> names = new List<string>();
		
		for(int i = 0; i < spritesheetNames.Count; i++) {
			names.Add(spritesheetNames[i].ToString());
		}
		
		return names;
	}
	
	static List<Dictionary<string, object>> AnimationSetsDictionaryFromObject(object obj) {
		List<object> animationSets = obj as List<object>;
		
		List<Dictionary<string, object>> setsList = new List<Dictionary<string, object>>();
		
		foreach(object setObject in  animationSets) {
			Dictionary<string, object> setDict = setObject as Dictionary<string, object>;
			setsList.Add(setDict);
		}
		
		return setsList;
	}

	private MZAnimationsFileContent() {
	}


	#endregion
}

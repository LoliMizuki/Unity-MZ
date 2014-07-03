#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections.Generic;
using MiniJSON;

[ExecuteInEditMode]
public class MZAnimationProjectManager : MonoBehaviour {

	public string animationsFilePath;

	public string animationsFileName {
		get {
			if(IsPathValid(animationsFilePath) == false) {
				return null;
			}

			return Path.GetFileNameWithoutExtension(animationsFilePath);
		}
	}

	public void CreateProjectFromPath(string path) {
		if(path == null || path.Length == 0) {
			return;
		}

		Unload();
		
		MZAnimationsFileContent projectContent = MZAnimationsFileContent.ProjectContentFromPath(path);
		animationsFilePath = path;

		if(projectContent.spritesheetNames == null) {
			return;
		}
		foreach(string spritesheetName in projectContent.spritesheetNames) {
			gameObject.GetComponent<MZSpritesheetsManager>().LoadSpritesheetWithName(spritesheetName);
		}

		if(projectContent.animationSets == null) {
			return;
		}
		foreach(Dictionary<string, object> aniSetDict in projectContent.animationSets) {
			gameObject.GetComponent<MZAnimationsManager>().AddAnimationSet(aniSetDict["name"].ToString(),
			                                                               MZ.Sprites.AnimationSet.AnimationSetFromDictionary(aniSetDict));
		}

	}

	public void NewProject() {
		animationsFilePath = EditorUtility.SaveFilePanel("Create Animations File", MZ.Sprites.Editor.ANIMATIONS_DIRECTORY_FULL_PATH, 
		                                          "<new project>", MZ.Sprites.ANIMATION_EXT_NAME);

		if(IsPathValid(animationsFilePath) == false) {
			return;
		}

		CreateFileAtPathIfNotExist(animationsFilePath);
		SetTempRecord("last_project_path", animationsFilePath);

		AssetDatabase.Refresh();
	}

	public void OpenProject() {
		animationsFilePath = EditorUtility.OpenFilePanel(
			"Open Animations File",
			MZ.Sprites.Editor.ANIMATIONS_DIRECTORY_FULL_PATH,
			MZ.Sprites.ANIMATION_EXT_NAME
		 );
		CreateProjectFromPath(animationsFilePath);
		SetTempRecord("last_project_path", animationsFilePath);
	}

	public void SaveProject() {
		if(IsPathValid(animationsFilePath) == false) {
			NewProject();
		}

		SaveAnimationsFileToPath(animationsFilePath);
	}

	public void Unload() {
		// save current state

		GetComponent<MZAnimationsManager>().Unload();
		GetComponent<MZSpritesheetsManager>().Unload();
		animationsFilePath = null;
	}

	#region - private

	MZSpritesheetsManager _spritesheetsManager {
		get {
			return GetComponent<MZSpritesheetsManager>();
			}
	}

	Dictionary<string, string> _tempRecord; // not complex now, use dictionary.
 
	void Awake() {
		if(Application.isPlaying) {
			return;
		}

		CheckAndCreateNecessaryDirectories();
		LoadTempRecord();
		AssetDatabase.Refresh();

		AddManagerComponentsIfNotExist();

		LoadPreProjectIfNeed();
	}

	void CheckAndCreateNecessaryDirectories() {
		Action<string> createPathIfNotExist = (string path) => {
			if(Directory.Exists(path) == false) {
				Directory.CreateDirectory(path);
			}
		};

		createPathIfNotExist(MZ.Sprites.Editor.SPRITESHEETS_DIRECTORY_FULL_PATH);
		createPathIfNotExist(MZ.Sprites.Editor.ANIMATIONS_DIRECTORY_FULL_PATH);
		createPathIfNotExist(Path.GetDirectoryName(MZ.Sprites.Editor.PLUGIN_TEMP_FULL_PATH));
	}

	void AddManagerComponentsIfNotExist() {
		if(GetComponent<MZSpritesheetsManager>() == null) {
			gameObject.AddComponent<MZSpritesheetsManager>();
		}

		if(GetComponent<MZAnimationsManager>() == null) {
			gameObject.AddComponent<MZAnimationsManager>();
		}
	}

	void LoadPreProjectIfNeed() {
		if(_tempRecord == null || _tempRecord.ContainsKey("last_project_path") == false || 
		   _tempRecord["last_project_path"] == null || _tempRecord["last_project_path"].Length == 0) {
			return;
		}

		string lastProjectPath = _tempRecord["last_project_path"];
		if(File.Exists(lastProjectPath) == false) {
			return;
		}

		CreateProjectFromPath(lastProjectPath);
	}

	void LoadTempRecord() {
		CreateFileAtPathIfNotExist(MZ.Sprites.Editor.PLUGIN_TEMP_FULL_PATH);
		string jsonString = File.ReadAllText(MZ.Sprites.Editor.PLUGIN_TEMP_FULL_PATH);
		if(jsonString == null || jsonString.Length == 0) {
			return;
		}

		Dictionary<string, object> tempRecordObject = Json.Deserialize(jsonString) as Dictionary<string, object>;

		if(_tempRecord == null) {
			_tempRecord = new Dictionary<string, string>();
		}

		foreach(string key in tempRecordObject.Keys) {
			if(_tempRecord.ContainsKey(key)) {
				_tempRecord[key] = tempRecordObject[key].ToString();
			} else {
				_tempRecord.Add(key, tempRecordObject[key].ToString());
			}
		}
	}

	void CreateFileAtPathIfNotExist(string path) {
		if(IsPathValid(path) == false) {
			return;
		}

		if(File.Exists(path) == false) {
			File.Create(path).Close();
		}
	}

	bool IsPathValid(string path) {
		return (path != null && path.Length > 0);
	}

	void SetTempRecord(string key, string value) {
		if(_tempRecord == null) {
			_tempRecord = new Dictionary<string, string>();
		}

		if(_tempRecord.ContainsKey(key) == false) {
			_tempRecord.Add(key, value);
		} else {
			_tempRecord[key] = value;
		}

		string jsonString = Json.Serialize(_tempRecord);
		File.WriteAllText(MZ.Sprites.Editor.PLUGIN_TEMP_FULL_PATH, jsonString);
	}

	void SaveAnimationsFileToPath(string path) {
		if(IsPathValid(path) == false) {
			return;
		}

		// spritesheet info
		MZSpritesheetsManager spritesheetsManager = GetComponent<MZSpritesheetsManager>();
		List<string> spritesheetNames = new List<string>();
		foreach(string name in spritesheetsManager.framesSetByTextureName.Keys) {
			spritesheetNames.Add(name);
		}

		// animations info
		MZAnimationsManager animationsManager = GetComponent<MZAnimationsManager>();
		List<Dictionary<string, object>> animationSetsDict = new List<Dictionary<string, object>>();
		foreach(string animationName in animationsManager.animationSetsByName.Keys) {
			MZ.Sprites.AnimationSet animationSet = animationsManager.animationSetsByName[animationName];

			Dictionary<string, object> aniDict = MZ.Sprites.AnimationSet.DictionaryFromAnimationSet(animationSet);
			animationSetsDict.Add(aniDict);
		}

		// meta info
		Dictionary<string, string> meta = new Dictionary<string, string>();
		meta.Add("category", "mzsprites animations");
		meta.Add("created", "mzsprites animations editor");

		Dictionary<string, object> projectDict = new Dictionary<string, object>();
		projectDict.Add("spritesheets", spritesheetNames);
		projectDict.Add("animations", animationSetsDict);
		projectDict.Add("meta", meta);

		string jsonString = Json.Serialize(projectDict);
		File.WriteAllText(path, jsonString);
	}

	#endregion
}

#endif
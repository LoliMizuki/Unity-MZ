#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections.Generic;

[ExecuteInEditMode]
public class MZSpritesheetsManager : MonoBehaviour {

	public Transform SpriteNodesParent {
		get {
			return GameObject.Find("__Sprites").transform;
		}
	}
    
    public Dictionary<string, MZ.Sprites.FramesSet> framesSetByTextureName {
        get {
            return MZ.Sprites.FramesCollection.instance.framesSetByTextureName;
        }
    }

    public Dictionary<string, bool> expandsByName {
        get {
            return _expandsByName;
        }
    }

	public void Refresh() {
		MZAnimationProjectManager projectManager = GetComponent<MZAnimationProjectManager>();
		GetComponent<MZAnimationProjectManager>().CreateProjectFromPath(projectManager.animationsFilePath);
	}

	public void Unload() {
		ClearTestNodes();
		MZ.Sprites.FramesCollection.instance.Clear();
	}

	public void OpenSpritesheet() {
		string spritesheetPath = EditorUtility.OpenFilePanel("Open Spritesheet(Atlas)", 
		                                                     MZ.Sprites.Editor.SPRITESHEETS_DIRECTORY_FULL_PATH, 
		                                                     MZ.Sprites.SPRITESHEET_EXT_NAME);
		if(spritesheetPath != null && spritesheetPath.Length > 0) {
			string spritesheetName = Path.GetFileNameWithoutExtension(spritesheetPath);
			LoadSpritesheetWithName(spritesheetName);
			SetExpandByName(spritesheetName, false);
		}
	}
    
	public void LoadSpritesheetWithName(string name) {
		MZ.Sprites.FramesCollection.instance.AddFramesWithShoeBoxName(name);
	
		foreach(string textureName in MZ.Sprites.FramesCollection.instance.framesSetByTextureName.Keys) {
			SetExpandByName(textureName, false);
		}
    }

	public void ClearTestNodes() {
		while(SpriteNodesParent.childCount > 0) {
			GameObject.DestroyImmediate(SpriteNodesParent.GetChild(0).gameObject);
		}
	}
 
    #region private

    Dictionary<string, bool> _expandsByName;

    void Awake() {
		if(Application.isPlaying) {
			return;
		}
	}

	void SetExpandByName(string name, bool expand) {
		if(_expandsByName == null) {
			_expandsByName = new Dictionary<string, bool>();
		}

		if(_expandsByName.ContainsKey(name) == false) {
			_expandsByName.Add(name, expand);
			return;
		}
	
		_expandsByName[name] = expand;
	}
	
    #endregion
}

#endif
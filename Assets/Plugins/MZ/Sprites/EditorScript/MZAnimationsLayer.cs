#if UNITY_EDITOR

using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using MiniJSON;

public class MZAnimationsLayer : MonoBehaviour {

	void Awake() {
		MZAnimationsFileContent projectContent = ProjectContentFromTemp();
		if(projectContent == null) {
			return;
		}

		LoadSpritesheetsFromNames(projectContent.spritesheetNames);
		LoadAnimations(projectContent.animationSets);
	}

	MZAnimationsFileContent ProjectContentFromTemp() {
		string tempRecordPath = MZ.Sprites.Editor.PLUGIN_TEMP_FULL_PATH;
	
		if(File.Exists(tempRecordPath) == false) {
			return null;
		}

		string jsonString = File.ReadAllText(tempRecordPath);
		if(jsonString == null || jsonString.Length == 0) {
			return null;
		}

		Dictionary<string, object> tempDict = Json.Deserialize(jsonString) as Dictionary<string, object>;

		string lastProjectPath = tempDict["last_project_path"].ToString();
		if(lastProjectPath == null || lastProjectPath.Length == 0) {
			return null;
		}

		return MZAnimationsFileContent.ProjectContentFromPath(lastProjectPath);
	} 

	void LoadSpritesheetsFromNames(List<string> spritesheetNamesList) {
		foreach(string spritesheetName in spritesheetNamesList) {
			MZ.Sprites.FramesCollection.instance.AddFramesWithShoeBoxName(spritesheetName);
		}
	}

	void LoadAnimations(List<Dictionary<string, object>> animationsSetsList) {
		foreach(Dictionary<string, object> animationsSetDict in animationsSetsList) {
			string name = animationsSetDict["name"].ToString();
			MZ.Sprites.AnimationSetsCollection.instance.AddAnimationSet(name, 
			                                                   MZ.Sprites.AnimationSet.AnimationSetFromDictionary(animationsSetDict));
		}
	}  
    
    void Start() {
         ChildrenPlayAnimation();
    }

    void ChildrenPlayAnimation() {
        foreach(Transform child in transform) {
            RebuildAnimationNodeAndPlay(child.gameObject);
        }
    }

    void RebuildAnimationNodeAndPlay(GameObject animationObject) {
        MZAnimation animation = animationObject.GetComponent<MZAnimation>();
        if(animation == null) {
            return;
        }

        MZTestAnimationNodeInfo testAnimationNodeInfo = animationObject.GetComponent<MZTestAnimationNodeInfo>();
        if(testAnimationNodeInfo == null) {
            return;
        }

        foreach(string animationSetName in testAnimationNodeInfo.AnimationSetNames) {
            animation.AddAnimationSet(animationSetName, MZ.Sprites.AnimationSetsCollection.instance.animationSetByName[animationSetName]);
        }

        if(animation.animationsSetsByName != null && animation.animationsSetsByName.Count > 0) {
            animation.Play();
        }
    }
}

#endif

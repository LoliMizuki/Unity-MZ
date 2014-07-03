#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using MiniJSON;

[ExecuteInEditMode]
public class MZAnimationsManager : MonoBehaviour {

    public Transform AnimationNodesParent {
        get {
            return GameObject.Find("__Animations").transform;
        }
    }

    public Dictionary<string, MZ.Sprites.AnimationSet> animationSetsByName {
        get {
            return MZ.Sprites.AnimationSetsCollection.instance.animationSetByName;
        }
	}

    public void AddAnimationSet(string name, MZ.Sprites.AnimationSet animationSet) {
        MZ.Sprites.AnimationSetsCollection.instance.AddAnimationSet(name, animationSet);
    }

	public void Unload() {
		ClearTestNodes();
		MZ.Sprites.AnimationSetsCollection.instance.Clear();
	}

	public void Apply() {
		MZAnimationProjectManager projectManager = GetComponent<MZAnimationProjectManager>();
			projectManager.SaveProject();
	}

    public void AutoGenerateAnimaiton() {
        foreach(string spritesheetName in MZ.Sprites.FramesCollection.instance.framesSetByTextureName.Keys) {
            GenerateAnimationsWithSpritesheetName(spritesheetName);
        }
    }

	public void ClearTestNodes() {
		while(AnimationNodesParent.childCount > 0) {
			GameObject.DestroyImmediate(AnimationNodesParent.GetChild(0).gameObject);
		}
	}
	
	public void PutAnimationNodeToSceneWithSetName(string animationSetName) {
		GameObject newAnimationObject = new GameObject();
        newAnimationObject.name = animationSetName;
        newAnimationObject.transform.parent = AnimationNodesParent;

        MZAnimation animation = newAnimationObject.AddComponent<MZAnimation>();
        animation.AddAnimationSet(animationSetName, MZ.Sprites.AnimationSetsCollection.instance.animationSetByName[animationSetName]);

        newAnimationObject.transform.localScale = new Vector3(1, 1, 0);

        newAnimationObject.AddComponent<MZTestAnimationNodeInfo>().SetInfoWithAnimation(animation);

        animation.SetFrameButNotPlay();
    }

    #region private
	
    void GenerateAnimationsWithSpritesheetName(string spritesheetName) {
        MZ.Debugs.Assert(MZ.Sprites.FramesCollection.instance.framesSetByTextureName.ContainsKey(spritesheetName) == true, spritesheetName + " is not spritesheet");

        MZ.Sprites.FramesSet framesSet = MZ.Sprites.FramesCollection.instance.framesSetByTextureName[spritesheetName];

        Dictionary<string, List<string>> animationNamesGroup = AnimationNamesGroupFromFramesSet(framesSet);
        AddNewAnimationsByNameGroup(animationNamesGroup, spritesheetName);
    }

    Dictionary<string, List<string>> AnimationNamesGroupFromFramesSet(MZ.Sprites.FramesSet framesSet) {
        Dictionary<string, List<string>> animationNamesGroup = new Dictionary<string, List<string>>();
        string matchPattern = "[0-9]+$";

        for(int i = 0; i < framesSet.frameInfoNames.Length; i++) {
            string frameName = framesSet.frameInfoNames[i];
            Match m = Regex.Match(frameName, matchPattern);
            
            if(!m.Success) {
                continue;
            }
           
            string animationNameKey = FormatedAnimationNameKey(frameName.Substring(0, frameName.LastIndexOf(m.Value)));

			if(animationNameKey.Length == 0) {
				continue;
			}

            if(animationNamesGroup.ContainsKey(animationNameKey) == false) {
                animationNamesGroup.Add(animationNameKey, new List<string>());
            }
            
            animationNamesGroup[animationNameKey].Add(frameName);
        }

        return animationNamesGroup;
    }

    string FormatedAnimationNameKey(string originName) {
		if(originName == null || originName.Length == 0) {
			return originName;
		}

        char lastChar = originName[originName.Length - 1];
        List<char> removeSymbols = new List<char>();
        removeSymbols.Add('_'); 
        removeSymbols.Add('-');

        return (removeSymbols.Contains(lastChar))? originName.Substring(0, originName.Length - 1) : originName;
    }

    void AddNewAnimationsByNameGroup(Dictionary<string, List<string>> animationNamesGroup, string spritesheetName) {
        foreach(string animationKey in animationNamesGroup.Keys) {
            if(CheckAnimationNameDuplicate(animationKey)) {
                EditorUtility.DisplayDialog("Oops, this name is duplicated", animationKey + " in " + spritesheetName + 
                                            " is duplicate with others, please rename it", "Ok");
                continue;
            }

            if(animationNamesGroup[animationKey].Count <= 1) {
                continue;
            }

            animationNamesGroup[animationKey].Sort();
            MZ.Sprites.AnimationSet animationSet = MZ.Sprites.AnimationSet.AnimationSetWithParameters(animationKey, 
			                                                                        1,
			                                                                        spritesheetName,
			                                                                        animationNamesGroup[animationKey].ToArray());
            AddAnimationSet(animationKey, animationSet);
        }
    }

    bool CheckAnimationNameDuplicate(string name) {
        return MZ.Sprites.AnimationSetsCollection.instance.animationSetByName.ContainsKey(name);
    }
 
    #endregion
}

#endif
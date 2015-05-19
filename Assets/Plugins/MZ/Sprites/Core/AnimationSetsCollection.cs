using UnityEngine;
using System.IO;
using System.Collections.Generic;
using MiniJSON;

public static partial class MZ {
	
	public static partial class Sprites {

	    public class AnimationSetsCollection {
	
	        public static MZ.Sprites.AnimationSetsCollection instance {
	            get {
	                if(_instance == null) {
	                    _instance = new MZ.Sprites.AnimationSetsCollection();
	                }
	
	                return _instance;
	            }
	        }
	
			public Dictionary<string, MZ.Sprites.AnimationSet> animationSetByName {
				get {
					if(_animationSetByName == null) {
						_animationSetByName = new Dictionary<string, MZ.Sprites.AnimationSet>();
					}
	
					return _animationSetByName;
				}
			}
	
	        public void AddAnimationSet(string name, MZ.Sprites.AnimationSet animationSet) {
				if(animationSetByName.ContainsKey(name)) {
					return;
				}
	
	            animationSetByName.Add(name, animationSet);
	        }
	
	        public MZ.Sprites.AnimationSet AnimationSetWithName(string name) {
	            if(!MZ.Verifiers.Dictionary(animationSetByName)) {
	                MZ.Debugs.Log("animation set is null");
	                return null;
	            }
	
				if(name == null || !animationSetByName.ContainsKey(name)) {
	                MZ.Debugs.Log(
						"can not found animation set with name({0}) in {1}",
						name,
						animationSetByName.KeyNamesString()
					);
					
	                return null;
	            }
	
				return animationSetByName[name];
	        }
	
			public void AddAnimationSetsFromPath(string path) {
				MZ.Debugs.Assert(File.Exists(path), path + " is not exist");
				string jsonContent = File.ReadAllText(path);
				AddAnimationSetsFromDataJson(jsonContent);
			}
	
			public void AddAnimationSetsWithNameInResources(string name) {
				string path = MZ.Sprites.ANIMATIONS_DIRECTORY_IN_RESOURCES + "/" + name;
				AddAnimationSetsWithPathInResources(path);
			}
	
			public void AddAnimationSetsWithPathInResources(string path) {
				TextAsset textAsset = Resources.Load<TextAsset>(path);
				
				MZ.Debugs.AssertIfNullWithMessage(textAsset, "can not load animation at resources path: " + path);
				AddAnimationSetsFromDataJson(textAsset.ToString());
			}
	
			public void AddAnimationSetsFromDataJson(string json) {
				Dictionary<string, object> animData = (Dictionary<string, object>)Json.Deserialize(json);
	
	            if(animData.ContainsKey("spritesheets")) {
	                List<object> spritesheetNames = (List<object>)animData["spritesheets"];
	                foreach(object name in spritesheetNames) {
	                    AddSpritesheetIfNeedWithName(name.ToString());
	                }
	            }
	
				List<object> animSetsList = (List<object>)animData["animations"];
				
				foreach(object animSetObj in animSetsList) {
					Dictionary<string, object> animDict = (Dictionary<string, object>)animSetObj;
					MZ.Sprites.AnimationSet animationSet = MZ.Sprites.AnimationSet.AnimationSetFromDictionary(animDict);
					MZ.Debugs.AssertIfNullWithMessage(animationSet, "create animationSet fail");
					
					AddAnimationSet(animationSet.name, animationSet);
				}
			}
	
			public string JsonFromCurrentAniamtionSets() {
				if(animationSetByName.Count == 0) {
					return null;
				}
	
				Dictionary<string, object> dictionaryOfAniamtionSets = new Dictionary<string, object>();
				foreach(string name in animationSetByName.Keys) {
					dictionaryOfAniamtionSets.Add(name, MZ.Sprites.AnimationSet.DictionaryFromAnimationSet(animationSetByName[name]));
				}
				
				return MiniJSON.Json.Serialize(dictionaryOfAniamtionSets);
			}
	
			public void Clear() {
				if(animationSetByName == null) {
					return;
				}
	
				animationSetByName.Clear();
			}
	
	        // load from file
	        // save to file
	
	        #region private
	
	        static MZ.Sprites.AnimationSetsCollection _instance;
	        Dictionary<string, MZ.Sprites.AnimationSet> _animationSetByName; 
	
	        void AddSpritesheetIfNeedWithName(string name) {
	            if(!MZ.Sprites.FramesCollection.instance.framesSetByTextureName.ContainsKey(name)) {
					MZ.Sprites.FramesCollection.instance.AddFramesWithShoeBoxName(name);
	            }
	        }
	
	        #endregion
	    }
	}
}